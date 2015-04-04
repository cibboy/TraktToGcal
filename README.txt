-- DESCRIPTION --
This small application takes a user's TV shows from trakt.tv and adds events for such shows on a specific calendar in Google Calendar.

-- PREREQUISITES --
In order to work, the application requires the following:
1) a trakt.tv account
2) a trakt.tv application which provides a client ID and a client secret
3) a Google account
4) a Google Developer Key with access grant to Google Calendar
5) [Optional] a dedicated calendar on Google Calendar, possibly with default notifications

-- INSTALLATION --
1) Launch the application and let it create the necessary settings file for you
2) Download your client secrets json file from the Google Developer Console, copy it over the settings folder and rename it into googleauth.json
   Alternatively you can specify cliend id and client secret as parameters in each call of method Authorization.GetCredentialAsynch()
3) Fill in the fields in settings window (if you successfully copied googleauth.json you can already query google for the list of your calendars)
4) Save and enjoy

Once properly configured, it can be run with the --silent switch (for example in a scheduled task) in order to execute its job without showing the GUI, using the default parameters and the next Monday as starting day. Although it works out-of-the-box, it's suggested to rebuild the application as a console application, in order to make it wait for the execution to finish.

-- MIGRATION FROM V1 --
I made it so that you can use your existing installation and switch between the 2 versions in case something wrong happens. In technical terms, you can maintain both executables together (with different file names of course) and settings files will be updated without interfering with each other. V1 credentials will be stored in credentials.json, while V2 credentials will be split between settings.json (usernames) and traktauth.json (trakt codes). This requires you to go over to settings and re-set your usernames, as well as client id and client secret from your trakt application.

-- KNOWN BUGS --
1) If the local machine's firewall blocks access to local ports using localhost, google authorization does not work

-- TODO --
The following is work still to be completed, in no specific order:
1) Better implementation of parallelism (currently the GUI is mostly frozen when working)
2) More options/flexibility
3) Authorization of trakt access that does not require manual input of authorization code