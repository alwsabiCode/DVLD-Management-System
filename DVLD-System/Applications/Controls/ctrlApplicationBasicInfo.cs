using Business_DVLD;
using DVLD_System.Global_Classes;
using DVLD_System.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        clsApplication _Application;
        private int _ApplicationID = -1;
        public int ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
        }
        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application=clsApplication.FindBaseApplication(ApplicationID);
            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                _FillApplicationInfo();
        }
        private void _FillApplicationInfo()
        {
            _ApplicationID=_Application.ApplicationID ;
            lblApplicationID.Text=_Application.ApplicationID.ToString();
            lblStatus.Text = _Application.StatusText;
            lblType.Text = _Application.ApplicationTypeInfo.ApplicationTypeDTO.ApplicationTypeTitle;
            lblFees.Text = _Application.ApplicatDTO.PaidFees.ToString();
            lblApplicant.Text = _Application.PersonInfo.PDTO.FullName;
            lblDate.Text = clsFormat.DateToShort(_Application.ApplicatDTO.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_Application.ApplicatDTO.LastStatusDate);
            lblCreatedByUser.Text = _Application.CreatedByUserInfo.UserDTO.Username;

        }
        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;

            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_Application.ApplicatDTO.ApplicationPersonID);
            frm.ShowDialog();

            //Refresh
            LoadApplicationInfo(_ApplicationID);
        }
    }
}
