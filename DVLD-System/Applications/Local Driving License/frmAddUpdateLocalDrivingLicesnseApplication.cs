using Business_DVLD;
using DVLD_System.Global_Classes;
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

namespace DVLD_System.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        public enum enMode {AddNew=0,Update=1 };
        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID = -1;

        clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _Mode=enMode.AddNew;
        }
        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID=LocalDrivingLicenseApplicationID;
        }
        private void _FillLicenseClassesInComoboBox()
        {
            var ListLicenseClasses = clsLicenseClass.GetAllLicenseClass();
            foreach (var Class in ListLicenseClasses)
            {
                cbLicenseClass.Items.Add(Class.ClassName);
            }
        }
        private void _ResetDefualtValues()
        {
            _FillLicenseClassesInComoboBox();
            if (_Mode== enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                ctrlPersonCardWithFilter1.FilterFocus();
                _localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication(new clsLocalDrivingLicenseApplicationDTO(),new clsApplicationDTO (),clsLocalDrivingLicenseApplication.enMode.AddNew);
                tpApplication.Enabled = false;
                cbLicenseClass.SelectedIndex = 2;
                lblFees.Text=clsApplicationType.FindApplicationTypeByID((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationTypeDTO.ApplicationTypeFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserDTO.Username;


            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplication.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.ShowFilter = false;
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
             FindLocalDriving(_LocalDrivingLicenseApplicationID);
            if( _localDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_localDrivingLicenseApplication.ApplicatDTO.ApplicationPersonID);
            lblLocalDrivingLicebseApplicationID.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_localDrivingLicenseApplication.ApplicatDTO.ApplicationDate);
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find(_localDrivingLicenseApplication.LocalDTO.LicenseClassID).LicenseClassDTO.ClassName);
            lblFees.Text = _localDrivingLicenseApplication.ApplicatDTO.PaidFees.ToString();
            lblCreatedByUser.Text = clsUser.FindUserByUserID(_localDrivingLicenseApplication.ApplicatDTO.CreatedByUserID).UserDTO.Username;

        }

       

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

      

      
        private void frmAddUpdateLocalDrivingLicesnseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplication.Enabled = true;
                tcApplication.SelectedTab = tcApplication.TabPages["tpApplication"];
                return;
            }
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled = true;
                tpApplication.Enabled = true;
                tcApplication.SelectedTab = tcApplication.TabPages["tpApplication"];

            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;
            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewInternationalLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }
            if (clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _localDrivingLicenseApplication.ApplicatDTO.ApplicationPersonID = ctrlPersonCardWithFilter1.PersonID;
            _localDrivingLicenseApplication.ApplicatDTO.ApplicationDate = DateTime.Now;
            _localDrivingLicenseApplication.ApplicatDTO.ApplicationTypeID = 1;
            _localDrivingLicenseApplication.ApplicatDTO.ApplicationStatus = (byte)clsApplication.enApplicationStatus.New;
            _localDrivingLicenseApplication.ApplicatDTO.LastStatusDate = DateTime.Now;
            _localDrivingLicenseApplication.ApplicatDTO.PaidFees = Convert.ToDecimal(lblFees.Text);
            _localDrivingLicenseApplication.ApplicatDTO.CreatedByUserID = clsGlobal.CurrentUser.UserDTO.UserID;
            _localDrivingLicenseApplication.LocalDTO.LicenseClassID = LicenseClassID;

            if (_localDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enMode.Update;
                this.Text = "Update Local Driving License Application";
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
