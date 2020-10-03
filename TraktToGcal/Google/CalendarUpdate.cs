using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using TraktToGcal.Trakt;

namespace TraktToGcal.Google {
    class CalendarUpdate {
        public static async Task UpdateCalendarAsync(DefaultProperties Properties, string CalendarName, List<Entry> Entries, List<string> Excludes, bool IncludeSpecials, bool AllDay) {
            // Create the service.
            CalendarService service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = await GoogleAuthorization.GetCredentialAsync(Properties),
                ApplicationName = "TraktToGcal",
            });

            // Load list of calendars.
            CalendarList calList = await service.CalendarList.List().ExecuteAsync();
            string id = null;
            // Scan list of calendars looking for the one requested.
            foreach (CalendarListEntry e in calList.Items) {
                if (e.Summary.ToLowerInvariant() == CalendarName) {
                    id = e.Id;
                    break;
                }
            }

            // If no calendar matched, exit with warning to the user.
            if (id == null) {
                if (!Program.Silent)
                    MessageBox.Show("The calendar specified was not found!", "Calendar not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    Console.WriteLine("The calendar specified was not found!");
                return;
            }

            // Prepare request for events.
            EventsResource.ListRequest req = service.Events.List(id);

            foreach (Entry entry in Entries) {
                // If episode is from show that I want to keep and is not a special (o specials are to be included), copy over to return list.
                if (!Excludes.Contains(entry.Show.Title.ToLowerInvariant()) &&
                    (IncludeSpecials || entry.Episode.Season != 0)) {

                    // Query google calendar for same episode, if present.
                    req.Q = entry.Show.Title + " " + entry.Episode.Season + "x" + TraktAccess.GetProperEpisodeNumber(entry.Episode.Number);

                    // Create start date on show date from trakt.
                    DateTime start = entry.Episode.Aired;
                    // Create end date adding episode duration.
                    DateTime end = start.AddMinutes(45);
                    if (entry.Show.Runtime.HasValue)
                        end = start.AddMinutes(entry.Show.Runtime.Value);

                    // Delete such events.
                    IList<Event> list = req.Execute().Items;
                    bool updated = false;
                    foreach (Event ev in req.Execute().Items) {
                        // Update all events that have the same title.
                        if (ev.Summary == TraktAccess.CleanSeriesTitle(entry.Show.Title) + " " + entry.Episode.Season + "x" + TraktAccess.GetProperEpisodeNumber(entry.Episode.Number)) {
                            // Reset start/end dates.
                            ev.Start = new EventDateTime();
                            ev.End = new EventDateTime();

                            if (AllDay) {
                                // Set start and end date.
                                ev.Start.Date = start.ToString("yyyy-MM-dd");
                                // End date should be start date + 1 day.
                                ev.End.Date = start.AddDays(1).ToString("yyyy-MM-dd");
                            }
                            else {
                                // Set start and end date and time.
                                ev.Start.DateTime = start;
                                ev.End.DateTime = end;
                            }
                            // Set description.
                            ev.Description = "--- " + entry.Episode.Title + Environment.NewLine + entry.Episode.Overview + Environment.NewLine + Environment.NewLine +
                                "https://trakt.tv/episodes/" + entry.Episode.Ids.TraktId.Replace(":", "");

                            // Retrieve the event reminders.
                            //foreach (EventReminder reminder in ev.Reminders) {
                            //  String reminderMethod = reminder.Method;
                            //  int minutes = reminder.Minutes;
                            //}

                            // Needed in case an event changes date (otherwise reminders are lost). Custom reminders may be lost anyway...
                            //ev.Reminders.UseDefault = true;

                            // Update event.
                            service.Events.Update(ev, id, ev.Id).Execute();
                            // Set updated flag to true.
                            updated = true;
                        }
                    }
                    // If updated flag is false, no event has been updated, so we need to create one.
                    if (!updated) {
                        // Create new event with start/end dates.
                        Event evnt = new Event();
                        evnt.Start = new EventDateTime();
                        evnt.End = new EventDateTime();

                        if (AllDay) {
                            // Set start and end date.
                            evnt.Start.Date = start.ToString("yyyy-MM-dd");
                            // End date should be start date + 1 day.
                            evnt.End.Date = start.AddDays(1).ToString("yyyy-MM-dd");
                        }
                        else {
                            // Set start and end date and time.
                            evnt.Start.DateTime = start;
                            evnt.End.DateTime = end;
                        }

                        // Add title and descrition (with episode tite, overview and link).                            
                        evnt.Summary = TraktAccess.CleanSeriesTitle(entry.Show.Title) + " " + entry.Episode.Season + "x" + TraktAccess.GetProperEpisodeNumber(entry.Episode.Number);
                        evnt.Description = "--- " + entry.Episode.Title + Environment.NewLine + entry.Episode.Overview + Environment.NewLine + Environment.NewLine +
                            "https://trakt.tv/episodes/" + entry.Episode.Ids.TraktId.Replace(":", "");

                        // Save event to google calendar.
                        service.Events.Insert(evnt, id).Execute();
                    }
                }
            }
        }

        public static async Task<List<string>> GetListOfCalendarsAsync(DefaultProperties Properties) {
            // Create the service.
            CalendarService service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = await GoogleAuthorization.GetCredentialAsync(Properties),
                ApplicationName = "TraktToGcal",
            });

            // Load list of calendars.
            CalendarList calList = await service.CalendarList.List().ExecuteAsync();
            List<string> ret = new List<string>();
            // Prepare list of strings.
            foreach (CalendarListEntry e in calList.Items)
                ret.Add(e.Summary);

            return ret;
        }
    }
}
