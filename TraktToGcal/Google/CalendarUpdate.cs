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
        public static async Task UpdateCalendarAsync(string CalendarName, List<Entry> Entries, List<string> Excludes) {
            // Create the service.
            CalendarService service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = await Authorization.GetCredentialAsynch(),
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
                MessageBox.Show("The calendar specified was not found!", "Calendar not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prepare request for events.
            EventsResource.ListRequest req = service.Events.List(id);

            foreach (Entry entry in Entries) {
                foreach (EpisodeContainer container in entry.Episodes) {
                    // If episode is from show that I want to keep, copy over to return list.
                    if (!Excludes.Contains(container.Show.Title.ToLowerInvariant())) {

                        // Query google calendar for same episode, if present.
                        req.Q = container.Show.Title + " " + container.Episode.Season + "x" + TraktAccess.GetProperEpisodeNumber(container.Episode.Number);

                        // Create start date on show date from trakt.
                        DateTime start = DateTime.Parse(entry.Date);
                        // Creat end date adding 1 day to start (google considers this an all-day event).
                        DateTime end = start.AddDays(1);

                        // Delete such events.
                        IList<Event> list = req.Execute().Items;
                        bool updated = false;
                        foreach (Event ev in req.Execute().Items) {
                            // Update all events that have the same title.
                            if (ev.Summary == TraktAccess.CleanSeriesTitle(container.Show.Title) + " " + container.Episode.Season + "x" + TraktAccess.GetProperEpisodeNumber(container.Episode.Number)) {
                                // Set start and end date.
                                ev.Start.Date = start.ToString("yyyy-MM-dd");
                                ev.End.Date = end.ToString("yyyy-MM-dd");
                                // Set description.
                                ev.Description = "--- " + container.Episode.Title + Environment.NewLine + container.Episode.Overview + Environment.NewLine + Environment.NewLine + container.Episode.Url;

                                // Needed in case an event changes date (otherwise reminders are lost). Custom reminders may be lost anyway...
                                ev.Reminders.UseDefault = true;

                                // Update event.
                                service.Events.Update(ev, id, ev.Id).Execute();
                                // Set updated flag to true.
                                updated = true;
                            }
                        }
                        // If updated flag is false, no event has been updated, so we need to create one.
                        if (!updated) {
                            // Create new event with start date.
                            Event evnt = new Event();
                            evnt.Start = new EventDateTime();
                            evnt.Start.Date = start.ToString("yyyy-MM-dd");

                            // Add end date, title and descrition (with episode tite, overview and link).
                            evnt.End = new EventDateTime();
                            evnt.End.Date = end.ToString("yyyy-MM-dd");
                            evnt.Summary = TraktAccess.CleanSeriesTitle(container.Show.Title) + " " + container.Episode.Season + "x" + TraktAccess.GetProperEpisodeNumber(container.Episode.Number);
                            evnt.Description = "--- " + container.Episode.Title + Environment.NewLine + container.Episode.Overview + Environment.NewLine + Environment.NewLine + container.Episode.Url;

                            // Save event to google calendar.
                            service.Events.Insert(evnt, id).Execute();
                        }
                    }
                }
            }
        }

        public static async Task<List<string>> GetListOfCalendarsAsync() {
            // Create the service.
            CalendarService service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = await Authorization.GetCredentialAsynch(),
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
