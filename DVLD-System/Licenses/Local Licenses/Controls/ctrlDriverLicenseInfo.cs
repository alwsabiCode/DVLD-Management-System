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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID;
        private clsLicense _License;

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }
        public int LicenseID
        {
            get { return _LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }
        }
        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.PDTO.Gendor==0)
            {
                pbPersonImage.Image = Resources.Male_512;
                pbGendor.Image = Resources.Man_32;
            }
            else
            {
                pbPersonImage.Image= Resources.Female_512;
                pbGendor.Image= Resources.Woman_32;
            }
            string ImagePath=_License.DriverInfo.PersonInfo.PDTO.ImagePath;
            if(ImagePath!="")
                if(File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
            else
               MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        public void LoadInfo(int LicenseID)
        {
            _LicenseID=LicenseID;
            _License = clsLicense.FindByLicenseID(LicenseID);
            if(_License == null)
            {

                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.licenseDTO.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassIfo.LicenseClassDTO.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.PDTO.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.PDTO.NationalNo;
            lblGendor.Text = _License.DriverInfo.PersonInfo.PDTO.Gendor == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.PDTO.DateOfBirth);

            lblDriverID.Text = _License.licenseDTO.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.licenseDTO.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.licenseDTO.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.licenseDTO.Notes == "" ? "No Notes" : _License.licenseDTO.Notes;
            _LoadPersonImage();
        }
    }
}
