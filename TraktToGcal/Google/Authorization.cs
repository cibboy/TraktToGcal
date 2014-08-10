using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;

namespace TraktToGcal.Google {
    class Authorization {
        public static async Task<UserCredential> GetCredentialAsynch() {
            CustomCreds creds = CustomCreds.GetInstance();

            UserCredential credential;
            using (var stream = new FileStream("credentials.cred/client.secrets.json", FileMode.Open, FileAccess.Read)) {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    creds.GoogleUser,
                    CancellationToken.None,
                    new FileDataStore("credentials.cred", true)
                );
            }

            return credential;
        }

        public static async Task<UserCredential> GetCredentialAsynch(string ClientId, string ClientSecret) {
            CustomCreds creds = CustomCreds.GetInstance();

            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets {
                        ClientId = ClientId,
                        ClientSecret = ClientSecret
                    },
                new[] { CalendarService.Scope.Calendar },
                creds.GoogleUser, CancellationToken.None, new FileDataStore("credentials.cred", true)
            );

            return credential;
        }
    }
}
