using Business_DVLD;
using Business_DVLD;
using DVLD_System.Global_Classes;
using DVLD_System.Licenses;
using DVLD_System.Licenses.International_Licenses;
using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private int _InternationalLicenseID = -1;

        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

       

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));//add one year.
            lblFees.Text = clsApplicationType.FindApplicationTypeByID((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationTypeDTO.ApplicationTypeFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserDTO.Username;
        }


        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsInternationalLicense InternationalLicense = new clsInternationalLicense(new clsInternationalLicenseDTO(),new clsApplicationDTO(),clsInternationalLicense.enMode.AddNew);
            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            InternationalLicense.ApplicatDTO.ApplicationPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.DriverDTO.PersonID;
            InternationalLicense.ApplicatDTO.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicatDTO.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            InternationalLicense.ApplicatDTO.ApplicationStatus = (byte)clsApplication.enApplicationStatus.Completed;
            InternationalLicense.ApplicatDTO.LastStatusDate = DateTime.Now;
            InternationalLicense.ApplicatDTO.PaidFees = clsApplicationType.FindApplicationTypeByID((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationTypeDTO.ApplicationTypeFees;
            InternationalLicense.ApplicatDTO.CreatedByUserID = clsGlobal.CurrentUser.UserDTO.UserID;


            InternationalLicense.InternationalDTO.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.DriverID;
            InternationalLicense.InternationalDTO.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.InternationalDTO.IssueDate = DateTime.Now;
            InternationalLicense.InternationalDTO.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.InternationalDTO.CreatedByUserID=clsGlobal.CurrentUser.UserDTO.UserID;
            


            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
               new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.DriverDTO.DriverID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmShowInternationalLicenseInfo frm =
              new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void frmNewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tpApplicationInfo.Enabled = true; 
            tcInternatinalLicense.SelectedTab = tcInternatinalLicense.TabPages["tpApplicationInfo"];
            return;

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);
            if (SelectedLicenseID == -1)
            {
                return;
            }
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.licenseDTO.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ActiveInternaionalLicenseID = clsInternationalLicense.
                GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.licenseDTO.DriverID);
            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                btnIssueLicense.Enabled = false;
                tpApplicationInfo.Enabled = false;
                return;
            }
            btnIssueLicense.Enabled = true;
            tpApplicationInfo.Enabled = true;
        }
    }
}
