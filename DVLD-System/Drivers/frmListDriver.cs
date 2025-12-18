using Business_DVLD;
using DVLD_System.Licenses;
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
namespace DVLD_System.Drivers
{
    public partial class frmListDriver : Form
    {
        private  List<clsDriverViewDTO>_ListDrivers=clsDriver.GetAllDrivers();
        public frmListDriver()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDriver_Load(object sender, EventArgs e)
        {
            _ListDrivers=clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _ListDrivers;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
            cbFilter.SelectedIndex=0;
            if(dgvDrivers.Rows.Count>0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 100;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 115;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 320;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 150;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 100;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");


            if (cbFilter.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                dgvDrivers.DataSource = _ListDrivers;
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }
            var ListFilteredDrivers = new List<clsDriverViewDTO>();
            if (FilterColumn== "DriverID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(),out int value))
                {
                    ListFilteredDrivers = _ListDrivers.Where(P=> P.DriverID == value).ToList();
                }
                else
                {
                      ListFilteredDrivers = _ListDrivers;
                }

            }
            else if (FilterColumn == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int value))
                {
                    ListFilteredDrivers = _ListDrivers.Where(P => P.PersonID == value).ToList();
                }
                else
                {
                    ListFilteredDrivers = _ListDrivers;
                }
            }
            else if (FilterColumn == "NationalNo")
            {
                string filterValue = txtFilterValue.Text.Trim().ToLower();
                ListFilteredDrivers = _ListDrivers.Where(P => P.NationalNo.ToLower().StartsWith(filterValue)).ToList();
            }
            else if (FilterColumn == "FullName")
            {
                string filterValue = txtFilterValue.Text.Trim().ToLower();
                ListFilteredDrivers = _ListDrivers.Where(P => P.FullName.ToLower().Contains(filterValue)).ToList();
            }

            var CustomList = ListFilteredDrivers.Select(P=> new
            {
                P.DriverID,
                P.PersonID,
                P.NationalNo,
                P.FullName,
                P.CrearedDate,
                P.NumberOfActiveLicenses
            }).ToList();
            dgvDrivers.DataSource = CustomList;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilter.Text == "Driver ID" || cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
           frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
            //refresh
            frmListDriver_Load(null, null);
        }

        private void issueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.");

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;


            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
