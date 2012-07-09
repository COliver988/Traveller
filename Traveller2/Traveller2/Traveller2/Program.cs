using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Traveller2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                SubMain();
            }
            catch (Exception e)
            {
                HandleUnhandledException(e);
            }
        }
           

        public static void SubMain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Setup unhandled exception handlers
            AppDomain.CurrentDomain.UnhandledException += // CLR
               new UnhandledExceptionEventHandler(OnUnhandledException);


            // Start application logic
            Application.Run(new Form1());
        }

        // CLR unhandled exception
        private static void OnUnhandledException(Object sender,
           UnhandledExceptionEventArgs e)
        {
            HandleUnhandledException(e.ExceptionObject);
        }

        static void HandleUnhandledException(Object o)
        {
            Exception e = o as Exception;

            string error = "";
            if (e != null)
            { // Report System.Exception info
                error = "Exception = " + e.GetType() + Environment.NewLine +
                    "Message = " + e.Message + Environment.NewLine + 
                    "FullText = " + e.ToString();
            }
            else
            { // Report exception Object info
                error = "Exception = " + o.GetType() + Environment.NewLine +
                    "FullText = " + o.ToString();
            }

            MessageBox.Show("An unhandled exception occurred " +
               "and the application is shutting down.");
            Environment.Exit(1); // Shutting down
        }
    }
}
