using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UrlAutoTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Framework.Components.DataAccess.SetupConfiguration.SetConnectionList(10);
            Application.Run(new frmUrlTester());
        }
    }
}
