using System;
using System.Threading;
using System.Windows.Forms;

namespace TraktToGcal {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            try {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += new ThreadExceptionEventHandler(GUIException);
                // Set the unhandled exception mode to force all Windows Forms errors to go through
                // our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(NonGUIException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception e) {
                try {
                    ErrorDialog.Show("There's been an error:" + Environment.NewLine, "Error", e);
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
                ErrorDialog.Show("There's been an error:" + Environment.NewLine, "Error", t.Exception);
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
                ErrorDialog.Show("There's been an error:" + Environment.NewLine, "Error", e);
            }
            catch {
                Console.WriteLine("There's been a problem: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            Environment.Exit(1);
        }
    }
}
