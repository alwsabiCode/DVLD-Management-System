using Business_DVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID);
            }
        }
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAdd.Visible = value;
            }
        }
        private bool _ShowFilter = true;
        public bool ShowFilter
        {
            get { return _ShowFilter; }
            set
            {
                _ShowFilter = value;
                gbFilter.Enabled = _ShowFilter;
            }
        }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        private int _PersonID = -1;
        public int PersonID
        {
            get { return ctrlPersonCards1.PersonID; }
        }
        public clsPerson selectedPersonInfo
        {
            get { return ctrlPersonCards1.SelectedPersonInfo; }
        }
        public void LoadPersonInfo(int PersonID)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Text = PersonID.ToString();
            _FindNow();

        }
        private void _FindNow()
        {
            switch (cbFilter.Text)
            {
                case "Person ID":

                    ctrlPersonCards1.LoadPersonInfo(int.Parse(txtFilter.Text));
                    break;

                case "National No":

                    ctrlPersonCards1.LoadPersonInfo(txtFilter.Text);
                    break;

                default:
                    break;
            }
            if (OnPersonSelected != null && _ShowFilter)
            {
                OnPersonSelected(ctrlPersonCards1.PersonID);
            }
        }
        private void ctrlPersonCards1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _FindNow();

          
        }

        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "Please enter a value to search for");

            }
            else
            {
                errorProvider1.SetError(txtFilter, null);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();

        }
        private void DataBackEvent(object sender, int PersonID)
        {
            cbFilter.SelectedIndex = 1;
            txtFilter.Text = PersonID.ToString();
            ctrlPersonCards1.LoadPersonInfo(PersonID);
        }
        public void FilterFocus()
        {
            txtFilter.Focus();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }
            if (cbFilter.Text == "Person ID")
            {
                // Allow only digits and control characters (like backspace)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
