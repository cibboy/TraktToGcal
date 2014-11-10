-- DESCRIPTION --
This small application takes a user's TV shows from trakt.tv and adds events for such shows on a specific calendar in Google Calendar.

-- PREREQUISITES --
In order to work, the application requires the following:
1) a trakt.tv account with public access to information (if you want to keep your data private on trakt, you need to specify you account password or hash)
2) a Google account
3) a Google Developer Key with access grant to Google Calendar
4) [Optional] a dedicated calendar on Google Calendar, possibly with default notifications

-- INSTALLATION --
1) Download your client secrets json file from the Google Developer Console, copy it over the credentials.cred folder and rename it into client.secrets.json
   Alternatively you can specify cliend id and client secret as parameters in each call of method Authorization.GetCredentialAsynch()
2) Edit credentials.cred/custom.creds.json specifying your Google username, trakt username and trakt api key
3) If your account data on trakt is private, either specify you password in credentials.cred/custom.creds.json (parameter traktpassword - CLEAR TEXT!!), or your basic authentication hash (base64 conversion of <username>:<password> - check https://apigee.com/trakt/console/morbo?apig_cc=1 to show your hash)
4) EditTraktToGcal.exe.config with your personal settings if you with (these can be set at runtime, but they're not persisted at the moment)
5) Run and enjoy

-- KNOWN BUGS --
1) No complete error handling

-- TODO --
The following is work still to be completed, in no specific order:
1) Handling of all exceptions/error
2) Better implementation of parallelism (currently the GUI is mostly frozen when working)
3) Application icon
4) Settings dialog with persistence
5) More options/flexibility