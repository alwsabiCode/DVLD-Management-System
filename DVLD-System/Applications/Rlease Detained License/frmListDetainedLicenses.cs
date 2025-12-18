using Business_DVLD;
using Business_DVLD;
using DVLD_System.Licenses;
using DVLD_System.Licenses.Detain_License;
using DVLD_System.Licenses.Local_Licenses;
using DVLD_System.People;
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

namespace DVLD_System.Applications.Rlease_Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        private static List<clsDetainedLicenseViewDTO> _DetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
        private List<clsDetainedLicenseViewDTO> _filter = new List<clsDetainedLicenseViewDTO>(_DetainedLicenses);
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

            _DetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            _filter= new List<clsDetainedLicenseViewDTO>(_DetainedLicenses);

            dgvDetainedLicenses.DataSource = _filter.Select(d => new
            {
                d.DetainID,
                d.LicenseID,
                d.DetainDate,
                IsReleased= d.IsReleased? "Yes":"No",
                d.FineFees,
                d.ReleaseDate,
                d.NationalNo,
                d.FullName,
                 d.ReleaseApplicationID
            }).ToList();
            lblRecordCount.Text = dgvDetainedLicenses.Rows.Count.ToString();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;

            }
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
            //refresh
            frmListDetainedLicenses_Load(null, null);
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {

            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            //refresh
            frmListDetainedLicenses_Load(null, null);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    {
                        FilterColumn = "IsReleased";
                        break;
                    }
                    ;

                case "National No":

                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":

                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                frmListDetainedLicenses_Load(null, null);

            }
            var filtered = _DetainedLicenses;
            if (FilterColumn== "DetainID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(),out int value))
                {
                    filtered = _DetainedLicenses.Where(x => x.DetainID == value).ToList();
                }
                else
                {
                    filtered = _DetainedLicenses;
                }
            }
           
            else if (FilterColumn == "ReleaseApplicationID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int value))
                {
                    filtered = _DetainedLicenses.Where(x => x.ReleaseApplicationID == value).ToList();
                }
                else
                {
                    filtered = _DetainedLicenses;
                }
            }
            else if (FilterColumn== "NationalNo")
            {
                string value = txtFilterValue.Text.Trim().ToLower();
                filtered= _DetainedLicenses.Where(x => x.NationalNo.ToLower().StartsWith(value)).ToList();

            }
            else if (FilterColumn == "FullName")
            {
                string value = txtFilterValue.Text.Trim().ToLower();
                filtered = _DetainedLicenses.Where(x => x.FullName.ToLower().StartsWith(value)).ToList();
            }

            var custmFilter=filtered.Select(x => new
            {
                x.DetainID,
                x.LicenseID,
                x.DetainDate,
                x.IsReleased,
                x.FineFees,
                x.ReleaseDate,
                x.NationalNo,
                x.FullName,
                x.ReleaseApplicationID
            })
                .ToList();
            dgvDetainedLicenses.DataSource = custmFilter;
            lblRecordCount.Text = custmFilter.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else

            {

                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                    

                }
                else
                    txtFilterValue.Enabled = true;


                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterValue = cbIsReleased.Text.Trim();
            if (filterValue == "All")
            {
                frmListDetainedLicenses_Load(null, null);
            }
            bool filterIsReleased = filterValue == "Yes";
            var filtered = _DetainedLicenses.Where(x => 
            x.IsReleased == filterIsReleased).Select(x=> new
            {
                x.DetainID,
                x.LicenseID,
                x.DetainDate,
                x.IsReleased,
                x.FineFees,
                x.ReleaseDate,
                x.NationalNo,
                x.FullName,
                x.ReleaseApplicationID
            })

                .ToList();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.FindByLicenseID(LicenseID).DriverInfo.DriverDTO.PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.FindByLicenseID(LicenseID).DriverInfo.DriverDTO.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;

            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.ShowDialog();
            //refresh
            frmListDetainedLicenses_Load(null, null);
        }
    }
}
