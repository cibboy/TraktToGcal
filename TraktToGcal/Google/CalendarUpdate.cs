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
        public static async Task UpdateCalendarAsync(DefaultProperties Properties, string CalendarName, List<EpisodeEntry> Entries, List<string> Excludes, bool IncludeSpecials, bool AllDay) {
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

            foreach (EpisodeEntry entry in Entries) {
                // If episode is from show that I want to keep and is not a special (or specials are to be included), copy over to return list.
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
                    foreach (Event ev in list) {
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

                            // Update event.
                            service.Events.Update(ev, id, ev.Id).Execute();
                            // Set updated flag to true.
                            updated = true;
                        }
                    }
                    // If updated flag is false, no event has been updated, so we need to create one.
                    if (!updated) {
                        // Create new event with start/end dates.
                        Event evnt = new Event {
                            Start = new EventDateTime(),
                            End = new EventDateTime()
                        };

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

                        // Add title and description (with episode title, overview and link).                            
                        evnt.Summary = TraktAccess.CleanSeriesTitle(entry.Show.Title) + " " + entry.Episode.Season + "x" + TraktAccess.GetProperEpisodeNumber(entry.Episode.Number);
                        evnt.Description = "--- " + entry.Episode.Title + Environment.NewLine + entry.Episode.Overview + Environment.NewLine + Environment.NewLine +
                            "https://trakt.tv/episodes/" + entry.Episode.Ids.TraktId.Replace(":", "");

                        // Save event to google calendar.
                        service.Events.Insert(evnt, id).Execute();
                    }
                }
            }
        }

        public static async Task UpdateCalendarAsync(DefaultProperties Properties, string CalendarName, List<MovieEntry> Entries) {
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

            foreach (MovieEntry entry in Entries) {
                string title = entry.Movie.Title;
                if (entry.Movie.Year.HasValue)
                    title += " (" + entry.Movie.Year.Value + ")";

                // Query google calendar for same movie, if present.
                req.Q = title;

                // Get release date from trakt.
                DateTime start = entry.Released;
                // All-day events, end date should be date + 1 day..
                DateTime end = start.AddDays(1);

                // Delete such events.
                IList<Event> list = req.Execute().Items;
                bool updated = false;
                foreach (Event ev in list) {
                    // Update all events that have the same title.
                    if (ev.Summary == title) {
                        // Reset start/end dates.
                        ev.Start = new EventDateTime();
                        ev.End = new EventDateTime();
                        // Set start and end date.
                        ev.Start.Date = start.ToString("yyyy-MM-dd");
                        ev.End.Date = end.ToString("yyyy-MM-dd");

                        // Set description.
                        ev.Description = "--- " + title +
                            Environment.NewLine + entry.Movie.Tagline +
                            Environment.NewLine + entry.Movie.Runtime + "m - " + entry.Movie.Certification +
                            Environment.NewLine + Environment.NewLine + entry.Movie.Overview +
                            Environment.NewLine + Environment.NewLine + "https://trakt.tv/movies/" + entry.Movie.Ids.TraktId.Replace(":", "");

                        // Update event.
                        service.Events.Update(ev, id, ev.Id).Execute();
                        // Set updated flag to true.
                        updated = true;
                    }
                }
                // If updated flag is false, no event has been updated, so we need to create one.
                if (!updated) {
                    // Create new event with start/end dates.
                    Event evnt = new Event {
                        Start = new EventDateTime(),
                        End = new EventDateTime()
                    };
                    evnt.Start.Date = start.ToString("yyyy-MM-dd");
                    evnt.End.Date = end.ToString("yyyy-MM-dd");

                    // Add title and description.                            
                    evnt.Summary = title;
                    evnt.Description = "--- " + title +
                        Environment.NewLine + entry.Movie.Tagline +
                        Environment.NewLine + entry.Movie.Runtime + "m - " + entry.Movie.Certification +
                        Environment.NewLine + Environment.NewLine + entry.Movie.Overview +
                        Environment.NewLine + Environment.NewLine + "https://trakt.tv/movies/" + entry.Movie.Ids.TraktId.Replace(":", "");

                    // Save event to google calendar.
                    service.Events.Insert(evnt, id).Execute();
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
