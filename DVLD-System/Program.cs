using DVLD_System.Applications.Application_Types;
using DVLD_System.Applications.International_License;
using DVLD_System.Applications.Local_Driving_License;
using DVLD_System.Applications.Renew_Local_License;
using DVLD_System.Applications.ReplaceLostOrDamagedLicense;
using DVLD_System.Applications.Rlease_Detained_License;
using DVLD_System.Drivers;
using DVLD_System.Licenses;
using DVLD_System.Licenses.Detain_License;
using DVLD_System.Licenses.International_Licenses;
using DVLD_System.Licenses.Local_Licenses;
using DVLD_System.Login;
using DVLD_System.People;
using DVLD_System.Tests;
using DVLD_System.Tests.Test_Types;
using DVLD_System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
