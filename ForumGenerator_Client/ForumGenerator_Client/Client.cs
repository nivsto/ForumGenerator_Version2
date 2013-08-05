using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ForumGenerator_Client.Dialogs;

namespace ForumGenerator_Client
{
    static class Client
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainMethods mainMethods = new MainMethods();
        }
    }
}
