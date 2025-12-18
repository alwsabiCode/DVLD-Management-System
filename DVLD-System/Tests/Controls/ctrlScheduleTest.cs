using Business_DVLD;
using DVLD_System.Global_Classes;
using DVLD_System.Properties;
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
using static Business_DVLD.clsTestTypes;

namespace DVLD_System.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        public enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private int LocalDrivingLicenseApplicationID = -1;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        public clsTestTypes.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestTypes.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }

                }

            }
        }
        public void LoadInfo(int localDrivingLicenseApplicationID, int AppointmentID = -1)
        {
            if (AppointmentID == -1)
            {
                _Mode = enMode.AddNew;

            }
            else
            {
                _Mode = enMode.Update;
            }
             LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDriving(LocalDrivingLicenseApplicationID);
            if (_localDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + LocalDrivingLicenseApplicationID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }
            if (_localDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;
            }
            else
            {
                _CreationMode = enCreationMode.FirstTimeSchedule;

            }
            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationType.FindApplicationTypeByID((int)clsApplication.enApplicationType.RetakeTest).ApplicationTypeDTO.ApplicationTypeFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";

            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";

            }
            lblLocalDrivingLicenseAppID.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _localDrivingLicenseApplication.LicenseClassInfo.LicenseClassDTO.ClassName;
            lblFullName.Text = _localDrivingLicenseApplication.PersonFullName;

            lblTrial.Text = _localDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestTypes.FindTestTypeByID(_TestTypeID).testTypeDTO.Fees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";
                _TestAppointment = new clsTestAppointment(new clsTestAppointmentDTO(),clsTestAppointment.enMode.AddNew);
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }
            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();


            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePrviousTestConstraint())
                return;


        }
        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;

        }
        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.FindTestAppointmentByID(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblFees.Text = _TestAppointment.TestAppointmentDTO.PaidFees.ToString();
            if (DateTime.Compare(DateTime.Now, _TestAppointment.TestAppointmentDTO.AppointmentDate)<0)
            {
                dtpTestDate.MinDate = DateTime.Now;
            }
            else
            {
                dtpTestDate.MinDate = _TestAppointment.TestAppointmentDTO.AppointmentDate;
            }
            dtpTestDate.Value = _TestAppointment.TestAppointmentDTO.AppointmentDate;
            if (_TestAppointment.TestAppointmentDTO.RetakeTestApplicationID==-1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";

            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.ApplicatDTO.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.TestAppointmentDTO.RetakeTestApplicationID.ToString();
            }
            return true;
        }
        private bool _HandleAppointmentLockedConstraint()
        {
            //if appointment is locked that means the person already sat for this test
            //we cannot update locked appointment
            if (_TestAppointment.TestAppointmentDTO.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;

            }
            else
                lblUserMessage.Visible = false;

            return true;
        }
        private bool _HandlePrviousTestConstraint()
        {
            //we need to make sure that this person passed the prvious required test before apply to the new test.
            //person cannno apply for written test unless s/he passes the vision test.
            //person cannot apply for street test unless s/he passes the written test.

            switch (TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    lblUserMessage.Visible = false;

                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.
                    if (!_localDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

                case clsTestTypes.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    if (!_localDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

            }
            return true;

        }
        private bool _HandleRetakeApplication()
        {
            //this will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will linkit to the appoinment.
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                //incase the mode is add new and creation mode is retake test we should create a seperate application for it.
                //then we linke it with the appointment.

                //First Create Applicaiton 
                clsApplication Application = new clsApplication(new clsApplicationDTO(),clsApplication.enMode.AddNew);

                Application.ApplicatDTO.ApplicationPersonID = _localDrivingLicenseApplication.ApplicatDTO.ApplicationPersonID;
                Application.ApplicatDTO.ApplicationDate = DateTime.Now;
                Application.ApplicatDTO.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicatDTO.ApplicationStatus = (byte)clsApplication.enApplicationStatus.Completed;
                Application.ApplicatDTO.LastStatusDate = DateTime.Now;
                Application.ApplicatDTO.PaidFees = clsApplicationType.FindApplicationTypeByID((int)clsApplication.enApplicationType.RetakeTest).ApplicationTypeDTO.ApplicationTypeFees;
                Application.ApplicatDTO.CreatedByUserID = clsGlobal.CurrentUser.UserDTO.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.TestAppointmentDTO.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.TestAppointmentDTO.RetakeTestApplicationID = Application.ApplicationID;

            }
            return true;
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment. TestAppointmentDTO.TestTypeID = (int)_TestTypeID;
            _TestAppointment.TestAppointmentDTO.LocalDrivingLicenseApplicationID = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.TestAppointmentDTO.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.TestAppointmentDTO.PaidFees = Convert.ToDecimal(lblFees.Text);
            _TestAppointment.TestAppointmentDTO.CreatedByUserID = clsGlobal.CurrentUser.UserDTO.UserID;

            if (_TestAppointment.SaveAppointment())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void ctrlScheduleTest_Load(object sender, EventArgs e)
        {

        }
    }
}
