using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace TraktToGcal.Google {
    class Authorization {
        private async Task RunAsynch() {
            UserCredential credential;
            using (var stream = new FileStream("credentials.cred/client_secrets.json", FileMode.Open, FileAccess.Read)) {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    Properties.Settings.Default.GoogleUsername,
                    CancellationToken.None,
                    new FileDataStore("credentials.cred", true)
                );

                /*credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets {
                          ClientId = "PUT_CLIENT_ID_HERE",
                          ClientSecret = "PUT_CLIENT_SECRETS_HERE"
                      },
                    new[] { CalendarService.Scope.Calendar },
                    Properties.Settings.Default.GoogleUsername, CancellationToken.None, new FileDataStore("credentials.cred", true));*/
            }

            // Create the service.
            var service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = "TraktToGcal",
            });

            CalendarList list = await service.CalendarList.List().ExecuteAsync();
            //MessageBox.Show("ciao");
            //Calendar c = null;
            string id = null;
            foreach (CalendarListEntry e in list.Items) {
                //c = await service.Calendars.Get(e.Id).ExecuteAsync();
                if (e.Summary == "TV Series") {
                    id = e.Id;
                    break;
                }
            }

            //var bookshelves = await service.Mylibrary.Bookshelves.List().ExecuteAsync();
        }
    }
}
