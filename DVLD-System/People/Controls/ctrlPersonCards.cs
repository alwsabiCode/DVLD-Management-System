using Business_DVLD;
using DVLD_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.People.Controls
{
    public partial class ctrlPersonCards : UserControl
    {
        private clsPerson _Person;
        private int _PersonID=-1;

        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }
        public ctrlPersonCards()
        {
            InitializeComponent();
        }
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if (_Person== null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person With Person ID = "+PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();

        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo.Trim());

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person With National No = " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();

        }
        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.PDTO.NationalNo;
            lblFullName.Text = _Person.PDTO.FullName;
            lblGendor.Text = (_Person.PDTO.Gendor == 0) ? "Male" : "Female";
            lblEmail.Text = _Person.PDTO.Email;
            lblPhone.Text = _Person.PDTO.Phone;
            lblDateOfBirth.Text = _Person.PDTO.DateOfBirth.ToShortDateString();
            lblCountry.Text = _Person.PDTO.CountryName;
            lblAddress.Text = _Person.PDTO.Address;
            
            _LoadPersonImage();
        }
        private void _LoadPersonImage()
        {
            if (_Person.PDTO.Gendor == 0)
            {
                pbGendor.Image = Resources.Man_32;
                pbPersonImage.Image = Resources.Male_512;

            }
            else
            {
                pbGendor.Image = Resources.Woman_32;
                pbPersonImage.Image = Resources.Female_512;

            }
            string imgPath = _Person.PDTO.ImagePath;
            if (imgPath!="")      
            {
                if (File.Exists(imgPath))
                {
                    pbPersonImage.ImageLocation = imgPath;
                }
                else 
                {
                    MessageBox.Show("Could not find this image: = " + imgPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;
        }

        private void ctrlPersonCards_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        private void lblDateOfBirth_Click(object sender, EventArgs e)
        {

        }
    }
}
