using Business_DVLD;
using DVLD_System.Global_Classes;
using DVLD_System.Licenses.International_Licenses;
using DVLD_System.Licenses.Local_Licenses;
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

namespace DVLD_System.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID;
        private clsDriver _driver;

        private static List<clsDriverLicensesViewDTO> _ListDriverLocalLicensesHistory;
        private List<clsDriverLicensesViewDTO> _FilterDriverLocalLicensesHistory;
        private static List<clsDriverInternationalLicensesViewDTO> _ListDriverInternationalLicensesHistory;
        private List<clsDriverInternationalLicensesViewDTO> _FilterDriverInternationalLicensesHistory;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        public void _LoadLicenseInfo()
        {
            _ListDriverLocalLicensesHistory = clsDriver.GetLicenses(_DriverID);
            dgvLocalLicensesHistory.DataSource = _ListDriverLocalLicensesHistory.Select(D => new
            {
                D.LicenseID,
                D.ApplicationID,
                D.ClassName,
                IssueDate=clsFormat.DateToShort (D.IssueDate),
                ExpirationDate=clsFormat.DateToShort( D.ExpirationDate),
                IsActive= (D.IsActive==false)?"No": "Yes"
                

            }).ToList();

            lblLocalLicensesRecords.Text=dgvLocalLicensesHistory.Rows.Count.ToString();
            if (dgvLocalLicensesHistory.Rows.Count>0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;

            }
        }
        public void _LoadInternationalLicenseInfo()
        {
            _ListDriverInternationalLicensesHistory = clsDriver.GetInternationalLicenses(_DriverID);

            dgvInternationalLicensesHistory.DataSource = _ListDriverInternationalLicensesHistory.Select(I => new
            {
                I.InternationalLicenseID,
                I.ApplicationID,
                I.IssuedUsingLocalLicenseID,
                IssueDate=clsFormat.DateToShort( I.IssueDate),
                ExpirationDate=clsFormat.DateToShort( I.ExpirationDate),
                IsActive= (I.IsActive==false)? "No":"Yes"


            }).ToList();
            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();
            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 140;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;
            }
        }
        public void  LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _driver = clsDriver.FindByPersonID(_DriverID);
            _LoadLicenseInfo();
            _LoadInternationalLicenseInfo();
            
        }
        public void LoadInfoByPersonID(int PersonID)
        {
            _driver = clsDriver.FindByPersonID(PersonID);
            if (_driver!=null)
            {
                _DriverID = clsDriver.FindByPersonID(PersonID).DriverID;
              
            }
            _LoadLicenseInfo();
            _LoadInternationalLicenseInfo();
        }
        public void Clear()
        {
            _ListDriverLocalLicensesHistory.Clear();

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

      
    }
}
