using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_DVLD;
using ModuleDTO_DVLD;



namespace DVLD_System.Applications.Application_Types
{
    public partial class frmListApplicationType : Form
    {
        private List<clsApplicationTypeDTO> _ListApplicationTypes=clsApplicationType.GetAllApplicationTypes();
        public frmListApplicationType()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationType_Load(object sender, EventArgs e)
        {
            _ListApplicationTypes=clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _ListApplicationTypes;
            lblRecordCount.Text = dgvApplicationTypes.Rows.Count.ToString();
            if (dgvApplicationTypes.Rows.Count > 0)
            {

                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 100;
            }

        }

        private void TSMApplicationType_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListApplicationType_Load(null, null);
        }
    }
}
