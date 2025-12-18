using Business_DVLD;
using DVLD_System.Licenses.Local_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Applications.Local_Driving_License.controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private int localDrivingLicenseApplicationID = -1;
        private int? licenceID = -1;
        public int LocalDrivingLicenseApplicationID
        {
            get
            {
                return localDrivingLicenseApplicationID;
            }
        }
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

       public void LoadApplicationInfoByLocalDrivingAppID(int localDrivingLicenceApplicationID)
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDriving(localDrivingLicenceApplicationID);
            if( _localDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();

        }
        public void LoadApplicationInfoByApplictionID(int localDrivingLicenceApplicationID)
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindApplicationID(localDrivingLicenceApplicationID);
            if (_localDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();

        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            licenceID =_localDrivingLicenseApplication.GetActiveLicenseID();
            llShowLicenceInfo.Enabled = (licenceID != -1);

            lblLocalDrivingLicenseApplicationID.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = clsLicenseClass.Find(_localDrivingLicenseApplication.LocalDTO.LicenseClassID).LicenseClassDTO.ClassName;
            lblPassedTests.Text = _localDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_localDrivingLicenseApplication.ApplicationID);
        }
        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            localDrivingLicenseApplicationID = -1;
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";


        }
        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_localDrivingLicenseApplication.GetActiveLicenseID());
            frm.ShowDialog();
        }
    }
}
