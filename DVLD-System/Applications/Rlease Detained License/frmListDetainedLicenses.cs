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
        private static List<clsDetainedLicenseViewDTO> _DetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

            _DetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

            dgvDetainedLicenses.DataSource = _DetainedLicenses.Select(d => new
            {
                d.DetainID,
                d.LicenseID,
                d.DetainDate,

                IsReleased = d.IsReleased,                   // للمنطق فقط
                ReleasedStatus = d.IsReleased ? "Yes" : "No", // للعرض

                d.FineFees,
                d.ReleaseDate,
                d.NationalNo,
                d.FullName,
                d.ReleaseApplicationID
            }).ToList();

            lblRecordCount.Text = dgvDetainedLicenses.Rows.Count.ToString();

            SetupDetainedLicensesGrid();
        }
        private void SetupDetainedLicensesGrid()
        {
            if (dgvDetainedLicenses.Rows.Count == 0)
                return;

            dgvDetainedLicenses.Columns["IsReleased"].Visible = false;

            dgvDetainedLicenses.Columns["DetainID"].HeaderText = "D.ID";
            dgvDetainedLicenses.Columns["DetainID"].Width = 80;

            dgvDetainedLicenses.Columns["LicenseID"].HeaderText = "L.ID";
            dgvDetainedLicenses.Columns["LicenseID"].Width = 80;

            dgvDetainedLicenses.Columns["DetainDate"].HeaderText = "D.Date";
            dgvDetainedLicenses.Columns["DetainDate"].Width = 130;

            dgvDetainedLicenses.Columns["ReleasedStatus"].HeaderText = "Released";
            dgvDetainedLicenses.Columns["ReleasedStatus"].Width = 90;

            dgvDetainedLicenses.Columns["FineFees"].HeaderText = "Fine Fees";
            dgvDetainedLicenses.Columns["FineFees"].Width = 80;

            dgvDetainedLicenses.Columns["ReleaseDate"].HeaderText = "Release Date";
            dgvDetainedLicenses.Columns["ReleaseDate"].Width = 130;

            dgvDetainedLicenses.Columns["NationalNo"].HeaderText = "N.No.";
            dgvDetainedLicenses.Columns["NationalNo"].Width = 80;

            dgvDetainedLicenses.Columns["FullName"].HeaderText = "Full Name";
            dgvDetainedLicenses.Columns["FullName"].Width = 330;

            dgvDetainedLicenses.Columns["ReleaseApplicationID"].HeaderText = "Release App.ID";
            dgvDetainedLicenses.Columns["ReleaseApplicationID"].Width = 100;
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

            var custmFilter=filtered.Select(d => new
            {
                d.DetainID,
                d.LicenseID,
                d.DetainDate,

                IsReleased = d.IsReleased,                   // للمنطق فقط
                ReleasedStatus = d.IsReleased ? "Yes" : "No", // للعرض

                d.FineFees,
                d.ReleaseDate,
                d.NationalNo,
                d.FullName,
                d.ReleaseApplicationID
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

            // All → إعادة تحميل البيانات كاملة
            if (filterValue == "All")
            {
                frmListDetainedLicenses_Load(null,null);
                return; 
            }

            var filtered = _DetainedLicenses
                .Where(d =>
                    (filterValue == "Yes" && d.IsReleased) ||
                    (filterValue == "No" && !d.IsReleased)
                )
                .Select(d => new
                {
                    d.DetainID,
                    d.LicenseID,
                    d.DetainDate,
                    d.IsReleased, // bool للمنطق
                    ReleasedStatus = d.IsReleased ? "Yes" : "No", // للعرض
                    d.FineFees,
                    d.ReleaseDate,
                    d.NationalNo,
                    d.FullName,
                    d.ReleaseApplicationID
                })
                .ToList();

            dgvDetainedLicenses.DataSource = filtered;
            lblRecordCount.Text = dgvDetainedLicenses.Rows.Count.ToString();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;

        }
    }
}
