using HCMS_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCMS.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public event Action<int> OnSelectedPerson;

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private bool _ShowAddPerson = true;

        public bool ShowAddPerson

        { get { return _ShowAddPerson; } set { _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson; } }

        private bool _FilterEnabled = true;

        public bool FilterEnabled { get { return _FilterEnabled; } set { _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled; } }

        public int PersonID {
            get { return ctrlPersonCard.PersonID; }
        }

        public void FilterFocus()
        {
            txtFind.Focus();
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard.SelectedPersonInfo; }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Failds are not valid!, put the mouse over the red icon to see the error",
                    "Validating Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FindNow();
        }

        private void FindNow()
        {
            ctrlPersonCard.LoadPersonInfo(int.Parse(txtFind.Text));

            if (OnSelectedPerson != null && FilterEnabled)
                OnSelectedPerson?.Invoke(PersonID);
        }



        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            txtFind.Focus();
        }

        private void txtFind_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFind, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFind, null);
            }
        }

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {

        }
    }
}
