using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraktToGcal {
    static class Program {
        public static bool Silent = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1 && args[1] == "--silent")
                Silent = true;

            try {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += new ThreadExceptionEventHandler(GUIException);
                // Set the unhandled exception mode to force all Windows Forms errors to go through
                // our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(NonGUIException);

                if (!Silent) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                else {
                    Task<bool> t = MainForm.RunSilentAsync();
                    t.Wait();

                    if (!t.Result) {
                        Environment.ExitCode = 1;
                        Application.Exit();
                    }
                }
            }
            catch (Exception e) {
                try {
                    if (!Silent)
                        ErrorDialog.Show("There's been an error:" + Environment.NewLine, "Error", e);
                    else {
                        Console.WriteLine("There's been an error: " + e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }
                catch {
                    Console.WriteLine("There's been a problem: " + e.Message);
                    Console.WriteLine(e.StackTrace);
                }

                Environment.ExitCode = 1;
                Application.Exit();
            }
        }

        /// <summary>
        /// Handles GUI unhandled exceptions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="t"></param>
        private static void GUIException(object sender, ThreadExceptionEventArgs t) {
            try {
                if (!Silent)
                    ErrorDialog.Show("There's been an error:" + Environment.NewLine, "Error", t.Exception);
                else {
                    Console.WriteLine("There's been an error: " + t.Exception.Message);
                    Console.WriteLine(t.Exception.StackTrace);
                }
            }
            catch {
                Console.WriteLine("There's been a problem: " + t.Exception.Message);
                Console.WriteLine(t.Exception.StackTrace);
            }

            Environment.ExitCode = 1;
            Application.Exit();
        }

        /// <summary>
        /// Handles non-GUI unhandled exceptions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="t"></param>
        private static void NonGUIException(object sender, UnhandledExceptionEventArgs t) {
            Exception e = (Exception)t.ExceptionObject;
            try {
                if (!Silent)
                    ErrorDialog.Show("There's been an error:" + Environment.NewLine, "Error", e);
                else {
                    Console.WriteLine("There's been an error: " + e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
            catch {
                Console.WriteLine("There's been a problem: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            Environment.Exit(1);
        }
    }
}
