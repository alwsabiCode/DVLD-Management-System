using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModuleDTO_DVLD;
using Business_DVLD;
using DVLD_System.People;

namespace DVLD_System
{
    public partial class frmListPeople : Form
    {
        private static List<clsPersonDTO> _PeopleList = clsPerson.GetAllPeople();

        private static List<clsPersonDTO> _FilteredPeopleList = new List<clsPersonDTO>(_PeopleList);



        public frmListPeople()
        {
            InitializeComponent();
        }
        private void _RefreshPeopleList()
        {
            _PeopleList = clsPerson.GetAllPeople();
            _FilteredPeopleList = new List<clsPersonDTO>(_PeopleList);
            var CustomList = _FilteredPeopleList.
                Select(P => new
                {
                    P.PersonID,
                    P.NationalNo,
                    P.FirstName,
                    P.SecondName,
                    P.ThirdName,
                    P.LastName,
                    P.DateOfBirth,
                    Gendor = (P.Gendor == 0) ? "Male" : "Female",
                    P.CountryName,
                    P.Phone,
                    P.Email, 


                }).ToList();
            DGVPeople.DataSource = CustomList;
            lblRecordCount.Text = DGVPeople.Rows.Count.ToString();
        }
        private void frmPeople_Load(object sender, EventArgs e)
        {
            DGVPeople.DataSource = _FilteredPeopleList.
                Select(P => new
                {
                    P.PersonID,
                    P.NationalNo,
                    P.FirstName,
                    P.SecondName,
                    P.ThirdName,
                    P.LastName,
                    P.DateOfBirth,
                    Gendor = (P.Gendor == 0) ? "Male" : "Female",
                    P.CountryName,
                    P.Phone,
                    P.Email,

                }).ToList(); ;
            cbFilter.SelectedIndex = 0;
            lblRecordCount.Text = DGVPeople.Rows.Count.ToString();
            if (DGVPeople.Rows.Count > 0)
            {
                DGVPeople.Columns[0].HeaderText = "Person ID";
                DGVPeople.Columns[0].Width = 80;
                DGVPeople.Columns[1].HeaderText = "National No";
                DGVPeople.Columns[1].Width = 100;
                DGVPeople.Columns[2].HeaderText = "First Name";
                DGVPeople.Columns[2].Width = 100;
                DGVPeople.Columns[3].HeaderText = "Second Name";
                DGVPeople.Columns[3].Width = 100;
                DGVPeople.Columns[4].HeaderText = "Third Name";
                DGVPeople.Columns[4].Width = 100;
                DGVPeople.Columns[5].HeaderText = "Last Name";
                DGVPeople.Columns[5].Width = 100;
                DGVPeople.Columns[6].HeaderText = "Date Of Birth";
                DGVPeople.Columns[6].Width = 120;
                DGVPeople.Columns[7].HeaderText = "Gendor";
                DGVPeople.Columns[7].Width = 80;
                DGVPeople.Columns[8].HeaderText = "Nationality";
                DGVPeople.Columns[8].Width = 100;
                DGVPeople.Columns[9].HeaderText = "Phone";
                DGVPeople.Columns[9].Width = 100;
                DGVPeople.Columns[10].HeaderText = "Email";
                DGVPeople.Columns[10].Width = 150;


            }

        }

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "Gendor";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }
            List<clsPersonDTO> ListPerson = new List<clsPersonDTO>();
            if (txtfilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _RefreshPeopleList();
                return;
            }

            if (FilterColumn == "PersonID")
            {
                if (int.TryParse(txtfilter.Text.Trim(), out int Value))
                {
                    ListPerson = _FilteredPeopleList.Where(P => P.PersonID == Value)
                        .ToList();
                }
                else
                {
                    ListPerson = _FilteredPeopleList;
                }
            }
            else if (FilterColumn == "NationalNo")
            {
                string FilterValue = txtfilter.Text.Trim().ToLower();
                ListPerson = _FilteredPeopleList
                    .Where(P => P.NationalNo.ToLower().StartsWith(FilterValue))
                    .ToList();
            }
            else if (FilterColumn == "FirstName")
            {
                string FilterValue = txtfilter.Text.Trim().ToLower();
                ListPerson = _FilteredPeopleList.Where(
                    P => P.FirstName.ToLower().StartsWith(FilterValue)).ToList();

            }
            else if (FilterColumn == "SecondName")
            {
                string FilterValue = txtfilter.Text.Trim().ToLower();
                ListPerson = _FilteredPeopleList.Where(
                    P => P.SecondName.ToLower().StartsWith(FilterValue)).ToList();

            }
            else if (FilterColumn == "ThirdName")
            {
                string FilterValue = txtfilter.Text.Trim().ToLower();
                ListPerson = _FilteredPeopleList.Where(
                    P => P.ThirdName.ToLower().StartsWith(FilterValue)).ToList();

            }
            else if (FilterColumn == "LastName")
            {
                string FilterValue = txtfilter.Text.Trim().ToLower();
                ListPerson = _FilteredPeopleList.Where(
                    P => P.LastName.ToLower().StartsWith(FilterValue)).ToList();

            }
            else if (FilterColumn == "CountryName")
            {
                string FilterValue=txtfilter.Text.Trim().ToLower();
                ListPerson=_FilteredPeopleList.Where(
                    P=>P.CountryName.ToLower().StartsWith(FilterValue)).ToList();

            }
            else if (FilterColumn == "Gendor")
            {
                string filterValue = txtfilter.Text.Trim().ToLower();

                ListPerson = _FilteredPeopleList
                    .Where(P =>
                        (P.Gendor == 0 && "male".StartsWith(filterValue)) ||
                        (P.Gendor == 1 && "female".StartsWith(filterValue))
                    )
                    .ToList();
            }

            else if (FilterColumn == "Phone")
            {
                string FilterValue = txtfilter.Text.Trim().ToLower();
                ListPerson = _FilteredPeopleList.Where(
                    P => P.Phone.ToLower().StartsWith(FilterValue)).ToList();

            }
            else if (FilterColumn == "Email")
            {
                string FilterValue = txtfilter.Text.Trim().ToLower();
                ListPerson = _FilteredPeopleList.Where(
                    P => P.Email.ToLower().StartsWith(FilterValue)).ToList();

            }
            var CustomList = ListPerson.
                Select(P => new
                {
                    P.PersonID,
                    P.NationalNo,
                    P.FirstName,
                    P.SecondName,
                    P.ThirdName,
                    P.LastName,
                    P.DateOfBirth,
                    Gendor = (P.Gendor == 0) ? "Male" : "Female",
                    P.CountryName,
                    P.Phone,
                    P.Email,

                }).ToList();
            DGVPeople.DataSource = CustomList;
            lblRecordCount.Text = CustomList.Count.ToString();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtfilter.Visible = (cbFilter.Text != "None");

            if (txtfilter.Visible)
            {
                txtfilter.Text = "";
                txtfilter.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSMSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void TSMOhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void TSMShowDetail_Click(object sender, EventArgs e)
        {
            int PersonID = (int)DGVPeople.CurrentRow.Cells[0].Value;
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void TSMAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void TSMEdit_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson((int)DGVPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void DGVPeople_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmPersonDetails((int)DGVPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void txtfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TSMDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + DGVPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.DeletePerson((int)DGVPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    _RefreshPeopleList();
                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}



