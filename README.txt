-- DESCRIPTION --
This small application takes a user's TV shows from trakt.tv and adds events for such shows on a specific calendar in Google Calendar.

-- PREREQUISITES --
In order to work, the application requires the following:
1) a trakt.tv account with public access to information (if you want to keep your data private on trakt, you need to specify you account password - don't worry, it will be stored as a hash)
2) a Google account
3) a Google Developer Key with access grant to Google Calendar
4) [Optional] a dedicated calendar on Google Calendar, possibly with default notifications

-- INSTALLATION --
1) Launch the application and let it create the necessary settings file for you
2) Download your client secrets json file from the Google Developer Console, copy it over the settings folder and rename it into googleauth.json
   Alternatively you can specify cliend id and client secret as parameters in each call of method Authorization.GetCredentialAsynch()
3) Fill in the fields in settings window (if you successfully copied googleauth.json you can already query google for the list of your calendars)
   If your account data on trakt is private, you need to specify your trakt password - it will be stored as a hash
4) Save and enjoy

-- KNOWN BUGS --
1) If the local machine's firewall blocks access to local ports using localhost, google authorization does not work

-- TODO --
The following is work still to be completed, in no specific order:
1) Better implementation of parallelism (currently the GUI is mostly frozen when working)
2) More options/flexibility