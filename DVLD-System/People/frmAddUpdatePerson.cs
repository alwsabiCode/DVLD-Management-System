using Business_DVLD;
using DVLD_System.Global_Classes;
using DVLD_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModuleDTO_DVLD;

namespace DVLD_System.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
        public enum enMode { AddMode = 0, UpdateMode = 1 };
        public enum enGender { Male = 0, Female = 1 };
        private enMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;


        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddMode;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.UpdateMode;
            _PersonID = PersonID;
        }
        private void _ResetDefaultValues()
        {
            _FillCountriesInComoboBox();
            if (_Mode == enMode.AddMode)
            {
                lblTitle.Text = "Add New Person";
                this.Text = "Add New Person";

                _Person = new clsPerson(new clsPersonDTO(), clsPerson.enMode.AddNew);
            }
            else
            {
                lblTitle.Text = "Update Person Info";
                this.Text = "Update Person Info";
            }
            llRemoveImage.Visible = (pbImagePerson.Image != null);
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Yemen");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";

        }
        private void _FillCountriesInComoboBox()
        {
            cbCountry.Items.Clear();
            List<clsCountryDTO> countries = clsCountry.GetAllCountries();
            foreach (var country in countries)
            {
                cbCountry.Items.Add(country.CountryName);
            }
        }
        private void _LoadData()
        {

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //the following code will not be executed if the person was not found
            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.PeopleDTO.FirstName;
            txtSecondName.Text = _Person.PeopleDTO.SecondName;
            txtThirdName.Text = _Person.PeopleDTO.ThirdName;
            txtLastName.Text = _Person.PeopleDTO.LastName;
            txtNationalNo.Text = _Person.PeopleDTO.NationalNo;
            dtpDateOfBirth.Value = _Person.PeopleDTO.DateOfBirth;

            if (_Person.PeopleDTO.Gendor == 0)
                rbMale.Checked = true;


            else
                rbFemale.Checked = true;

            txtAddress.Text = _Person.PeopleDTO.Address;
            txtPhone.Text = _Person.PeopleDTO.Phone;
            txtEmail.Text = _Person.PeopleDTO.Email;
            
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.PeopleDTO.CountryName);
            

            //load person image incase it was set.
            if (_Person.PeopleDTO.ImagePath != "")
            {
                pbImagePerson.ImageLocation = _Person.PeopleDTO.ImagePath;

            }

            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (_Person.PeopleDTO.ImagePath != "");

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.UpdateMode)
            {
                _LoadData();
            }

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked)
            {
                pbImagePerson.Image = Resources.Male_512;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked)
            {
                pbImagePerson.Image = Resources.Female_512;
            }
        }
        private bool _HandlePersonImage()
        {
            if (_Person.PeopleDTO.ImagePath != pbImagePerson.ImageLocation)
            {
                if (_Person.PeopleDTO.ImagePath !="")
                {
                    try
                    {
                        File.Delete(_Person.PeopleDTO.ImagePath);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (pbImagePerson.ImageLocation !=null)
                {
                    // copy the new image to the images folder
                    string imagesFolder = pbImagePerson.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref imagesFolder))
                    {
                        pbImagePerson.ImageLocation = imagesFolder;
                        _Person.PeopleDTO.ImagePath = imagesFolder;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;

        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.Title = "Select Person Image";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectFilePath = openFileDialog1.FileName;
                pbImagePerson.Load(SelectFilePath);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImagePerson.ImageLocation = null;
            _Person.PeopleDTO.ImagePath = null;

            if (rbMale.Checked)
            {
                pbImagePerson.Image = Resources.Male_512;
            }
            else
            {
                pbImagePerson.Image = Resources.Female_512;
            }
            llRemoveImage.Visible = false;
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (pbImagePerson.ImageLocation == null)
            {
                pbImagePerson.Image = Resources.Male_512;
            }
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (pbImagePerson.ImageLocation == null)
            {
                pbImagePerson.Image = Resources.Female_512;
            }
        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox txtBox = ((TextBox)sender);
            if (string.IsNullOrEmpty(txtBox.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtBox, "This field is required.");
            }
            else
            {
              
                errorProvider1.SetError(txtBox, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;

            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Format.");
            }
            else
            {

                errorProvider1.SetError(txtEmail, null);

            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }


            if (txtNationalNo.Text.Trim() != _Person.PeopleDTO.NationalNo && clsPerson.isPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "Person with this National No. already exists.");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandlePersonImage())
                return;


            int NationalityCountryID = clsCountry.Find(cbCountry.Text).CountryID;
            _Person.PeopleDTO.NationalNo = txtNationalNo.Text.Trim();
            _Person.PeopleDTO.FirstName = txtFirstName.Text.Trim();
            _Person.PeopleDTO.SecondName = txtSecondName.Text.Trim();
            _Person.PeopleDTO.ThirdName = txtThirdName.Text.Trim();
            _Person.PeopleDTO.LastName = txtLastName.Text.Trim();
            _Person.PeopleDTO.DateOfBirth = dtpDateOfBirth.Value;
            _Person.PeopleDTO.Gendor = rbMale.Checked ? (short)enGender.Male : (short)enGender.Female;
            _Person.PeopleDTO.Address = txtAddress.Text.Trim();
            _Person.PeopleDTO.Phone = txtPhone.Text.Trim();
            _Person.PeopleDTO.Email = txtEmail.Text.Trim();
            _Person.PeopleDTO.NationalityCountryID = NationalityCountryID;

            if (pbImagePerson.ImageLocation != null)
            {

                _Person.PeopleDTO.ImagePath = pbImagePerson.ImageLocation;
            }
            else
            {
                _Person.PeopleDTO.ImagePath = "";
            }
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the errors.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Person.Save())
            {
                lblPersonID.Text= _Person.PersonID.ToString();
                _Mode = enMode.UpdateMode;
                lblTitle.Text = "Update Person Info";
                this.Text = "Update Person Info";

                MessageBox.Show("Person Info Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                MessageBox.Show("Error Saving Person Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
    