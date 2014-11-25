using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;

namespace TraktToGcal.Google {
    class Authorization {
        public static async Task<UserCredential> GetCredentialAsynch(Credentials Creds) {
            UserCredential credential;
            using (var stream = new FileStream("settings/googleauth.json", FileMode.Open, FileAccess.Read)) {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    Creds.GoogleUser,
                    CancellationToken.None,
                    new FileDataStore("settings", true)
                );
            }

            return credential;
        }

        public static async Task<UserCredential> GetCredentialAsynch(Credentials Creds, string ClientId, string ClientSecret) {
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets {
                        ClientId = ClientId,
                        ClientSecret = ClientSecret
                    },
                new[] { CalendarService.Scope.Calendar },
                Creds.GoogleUser, CancellationToken.None, new FileDataStore("settings", true)
            );

            return credential;
        }
    }
}
