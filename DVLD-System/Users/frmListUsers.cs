using Business_DVLD;
using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Users
{
    public partial class frmListUsers : Form
    {
        private static List<clsUserDTO> _ListUser = clsUser.GetAllUsers();
        private static List<clsUserDTO> _FilterUsers = new List<clsUserDTO>(_ListUser);
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void _RefreshUsers()
        {
            _ListUser = clsUser.GetAllUsers();
            _FilterUsers = new List<clsUserDTO>(_ListUser);
            var CustomList = _FilterUsers.Select(P => new
            {
                P.UserID,
                P.PersonID,
                P.FullName,
                P.Username,
                IsActive = (P.IsActive == false) ? "No" : "Yes"
            }).ToList();
            dgvUsers.DataSource = CustomList;
            lblRecordCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
             dgvUsers.DataSource = _FilterUsers.
                           Select(P => new
                           {
                               P.UserID,
                               P.PersonID,
                               P.FullName,
                               P.Username,
                               IsActive = (P.IsActive == false) ? "No" : "Yes"
                           }).ToList(); ;
            cbFilterBy.SelectedIndex = 0;
            lblRecordCount.Text = dgvUsers.Rows.Count.ToString();
            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 100;
                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;
                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 400;
                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 180;
                dgvUsers.Columns[4].HeaderText = "IsActive";
                dgvUsers.Columns[4].Width = 100;


            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUsers frm= new frmAddUpdateUsers();
            frm.ShowDialog();
            _RefreshUsers();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbActive.Visible = true;
                cbActive.Focus();
                cbActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbActive.Visible = false;

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

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            List<clsUserDTO> ListUser = new List<clsUserDTO>();
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _RefreshUsers();
            }
            if (FilterColumn == "UserID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int value))
                {
                    ListUser = _FilterUsers.Where(P => P.UserID == value).ToList();
                }
                else
                {
                    ListUser = _FilterUsers;
                }
            }
            else if (FilterColumn == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int value))
                {
                    ListUser = _FilterUsers.Where(P => P.PersonID == value).ToList();
                }
                else
                {
                    ListUser = _FilterUsers;
                }

            }
            else if (FilterColumn == "FullName")
            {
                string filter = txtFilterValue.Text.Trim().ToLower();

                ListUser = _FilterUsers
                    .Where(p => p.FullName.ToLower().StartsWith(filter))

                    .ToList();
            }

            else if (FilterColumn == "UserName")
            {
                string FilterValue = txtFilterValue.Text.Trim().ToLower();
                ListUser = _FilterUsers.Where(P => P.Username.ToLower().StartsWith(FilterValue)).ToList();

            }
           
            var CustomList = ListUser.
                   Select(P => new
                   {
                       P.UserID,
                       P.PersonID,
                       P.FullName,
                       P.Username,
                       IsActive = (P.IsActive == false) ? "No" : "Yes"
                   }).ToList();
            dgvUsers.DataSource = CustomList;
            lblRecordCount.Text = CustomList.Count.ToString();

        }
        private void cbActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = cbActive.Text.Trim();

            // All → عرض كل المستخدمين
            if (filter == "All")
            {
                _RefreshUsers();
                return;
            }

            bool isActiveFilter = filter == "Yes";

            // فلترة المستخدمين حسب IsActive
            var filteredList = _FilterUsers
                .Where(u => u.IsActive == isActiveFilter)
                .Select(u => new
                {
                    u.UserID,
                    u.PersonID,
                    u.FullName,
                    u.Username,
                    IsActive = u.IsActive ? "Yes" : "No"
                })
                .ToList();

            dgvUsers.DataSource = filteredList;
            lblRecordCount.Text = filteredList.Count.ToString();
        }

        private void TSMAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUsers frm=new frmAddUpdateUsers();
            frm.ShowDialog();
           _RefreshUsers();

        }

        private void TSMShowDetail_Click(object sender, EventArgs e)
        {
            frmShowDetailUser frm = new frmShowDetailUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void TSMEditUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUsers frm = new frmAddUpdateUsers((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshUsers();
        }

        private void TSMSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void TSMOhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
        
        private void TSMChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TSMDeleteUser_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvUsers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (clsUser.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    _RefreshUsers();
                }

                else
                    MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmShowDetailUser frm = new frmShowDetailUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
