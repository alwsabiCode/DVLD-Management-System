using Business_DVLD;
using DVLD_System.Licenses;
using DVLD_System.Licenses.International_Licenses;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_System.Applications.International_License
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        private static List<clsInternationalLicenseDTO>_ListInternationalLicenses=clsInternationalLicense.GetAllInternationalLicense();
        private static List<clsInternationalLicenseDTO>_ListFilter=new List<clsInternationalLicenseDTO>(_ListInternationalLicenses);
        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            _ListInternationalLicenses= clsInternationalLicense.GetAllInternationalLicense();
            _ListFilter = new List<clsInternationalLicenseDTO>(_ListInternationalLicenses);
            dgvInternationalLicenses.DataSource = _ListFilter
                         .Select(u => new
                           {
                               u.InternationalLicenseID,
                               u.ApplicationID,
                               u.DriverID,
                               u.IssuedUsingLocalLicenseID,
                               u.IssueDate,
                               u.ExpirationDate,

                               IsActive = u.IsActive ? "Yes" : "No"
                               
                           })
                           .ToList();
            lblRecordCount.Text = dgvInternationalLicenses.Rows.Count.ToString();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 150;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 130;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 180;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;
            }
            cbFilter.SelectedIndex = 0;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            //refresh
            frmListInternationalLicesnseApplications_Load(null, null);

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).DriverDTO.PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

    
        private void txtfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Active")
            {
                txtfilter.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }
            else
            {
                txtfilter.Visible = (cbFilter.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilter.Text == "None")
                {
                    txtfilter.Enabled = false;

                }
                else
                    txtfilter.Enabled = true;

                txtfilter.Text = "";
                txtfilter.Focus();
            }
        }

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }

            var ListFilter = new List<clsInternationalLicenseDTO>();
            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtfilter.Text.Trim() == "" || FilterColumn == "None")
            {
                frmListInternationalLicesnseApplications_Load(null, null);
            }
            if (FilterColumn== "InternationalLicenseID")
            {
                if(int.TryParse(txtfilter.Text.Trim(),out int Value))
                {
                    ListFilter = _ListFilter.Where(P => P.InternationalLicenseID == Value).ToList();

                }
                else
                {
                    ListFilter = _ListFilter;
                }

            }
            else if (FilterColumn == "ApplicationID")
            {
                if (int.TryParse(txtfilter.Text.Trim(), out int Value))
                {
                    ListFilter = _ListFilter.Where(P => P.ApplicationID == Value).ToList();

                }
                else
                {
                    ListFilter = _ListFilter;
                }

            }
            else if (FilterColumn == "DriverID")
            {
                if (int.TryParse(txtfilter.Text.Trim(), out int Value))
                {
                    ListFilter = _ListFilter.Where(P => P.DriverID == Value).ToList();

                }
                else
                {
                    ListFilter = _ListFilter;
                }

            }
            else if (FilterColumn == "IssuedUsingLocalLicenseID")
            {
                if (int.TryParse(txtfilter.Text.Trim(), out int Value))
                {
                    ListFilter = _ListFilter.Where(P => P.IssuedUsingLocalLicenseID == Value).ToList();

                }
                else
                {
                    ListFilter = _ListFilter;
                }

            }
            var customFilter = ListFilter
               .Select(u => new
               {
                   u.InternationalLicenseID,
                   u.ApplicationID,
                   u.DriverID,
                   u.IssuedUsingLocalLicenseID,
                   u.IssueDate,
                   u.ExpirationDate,

                   IsActive = u.IsActive ? "Yes" : "No"
                   
               })
               .ToList();

            dgvInternationalLicenses.DataSource = customFilter;
            lblRecordCount.Text = customFilter.Count.ToString();

        }
        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = cbIsReleased.Text.Trim();

            // All → عرض كل المستخدمين
            if (filter == "All")
            {
                frmListInternationalLicesnseApplications_Load(null,null);
                return;
            }

            bool isActiveFilter = filter == "Yes";

            // فلترة المستخدمين حسب IsActive
            var filteredList = _ListFilter
                .Where(u => u.IsActive == isActiveFilter)
                .Select(u => new
                {
                   u.InternationalLicenseID,
                   u.ApplicationID,
                   u.DriverID,
                   u.IssuedUsingLocalLicenseID,
                   u.IssueDate,
                   u.ExpirationDate,

                    IsActive = u.IsActive ? "Yes" : "No"
                })
                .ToList();

            dgvInternationalLicenses.DataSource = filteredList;
            lblRecordCount.Text = filteredList.Count.ToString();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).DriverDTO.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();

        }
    }
}

