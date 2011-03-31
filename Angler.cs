using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;

namespace commercial_angler
{
    static class Angler
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm());
        }
    }
}