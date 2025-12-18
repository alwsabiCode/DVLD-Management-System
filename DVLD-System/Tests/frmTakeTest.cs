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

namespace DVLD_System.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _AppointmentID;
        private clsTestTypes.enTestType _testTypeID;
        private int _TestID=-1;
        private clsTest _Test;

        public frmTakeTest(int AppointmentID,clsTestTypes.enTestType testType)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _testTypeID = testType;
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest1.TestTypeID = _testTypeID;

            ctrlSecheduledTest1.LoadInfo(_AppointmentID);


            if (ctrlSecheduledTest1.TestAppointmentID == -1)
            {
                btnSave.Enabled = false;

            }
            else
            {
                btnSave.Enabled = true;

            }
            int testID = ctrlSecheduledTest1.TestID;
            if (testID != -1)
            {
                _Test = clsTest.Find(testID);
                if (_Test.testDTO.TestResult)
                {
                    rbPass.Checked = true;

                }
                else
                {
                    rbFail.Checked = true;
                    txtNotes.Text = _Test.testDTO.Note;
                }
                lblUserMessage.Visible = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;

            }
          
            _Test = new clsTest(new clsTestDTO(), clsTest.enMode.AddNew);
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            _Test.testDTO.TestAppointmentID = _AppointmentID;
            _Test.testDTO.TestResult = rbPass.Checked;
            _Test.testDTO.Note = txtNotes.Text.Trim();
            _Test.testDTO.CreatedByUerID = clsGlobal.CurrentUser.UserDTO.UserID;

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

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
