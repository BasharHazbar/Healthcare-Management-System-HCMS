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
        
        public enum enAnyPerson { Person = 0, Doctor = 1, Patient = 2};

        private enAnyPerson _AnyPerson = enAnyPerson.Doctor;

        private DataTable _dtPeople = new DataTable();

        public event Action<int> OnSelectedPerson;

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }


        public enAnyPerson AnyPerson
        {
            get { return _AnyPerson; }

            set { 
                _AnyPerson = value;

                switch (_AnyPerson)
                {
                    case enAnyPerson.Doctor:
                        _dtPeople = clsDoctor.GetAllDoctors();
                        break;
                    case enAnyPerson.Patient:
                        _dtPeople = clsPatient.GetAllPatients();
                        break;
                    case enAnyPerson.Person:
                        _dtPeople = clsPerson.GetAllFullNamePeople();
                        break;
                }

            }
        }


        private bool _ShowAddPerson = true;

        public bool ShowAddPerson

        { 
            get { return _ShowAddPerson; } set { _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson; } 
        }

        private bool _EditPersonInfoEnabled;
        public bool EditPersonInfoEnabled
        {
            get { return _EditPersonInfoEnabled; }
            set { _EditPersonInfoEnabled = value; ctrlPersonCard.EditPersonInfoEnabled = _EditPersonInfoEnabled; }
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled { get { return _FilterEnabled; } set { _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled; } }

        public int PersonID {
            get { return ctrlPersonCard.PersonID; }
        }

        public void TextFilterFocus()
        {
            txtFind.Focus();
        }

        void _FillPeopleComboBox()
        {
           
            foreach (DataRow row in _dtPeople.Rows)
            {
                if (_AnyPerson == enAnyPerson.Doctor)
                {
                    cbPeopleList.Items.Add("Dr. " + row["FullName"]);
                }
                else
                {
                    cbPeopleList.Items.Add(row["FullName"]);
                }
            }

            cbPeopleList.SelectedIndex = 0;
        }

        public void LoadPersonData(int PersonID)
        {
            gbFilter.Enabled = false;
            txtFind.Text = PersonID.ToString();
            FindNow();
        }



        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard.SelectedPersonInfo; }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
/*            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Failds are not valid!, put the mouse over the red icon to see the error",
                    "Validating Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/
            FindNow();
        }

        private void FindNow()
        {
            if (!string.IsNullOrEmpty(txtFind.Text) && _AnyPerson != enAnyPerson.Doctor)
            {
                ctrlPersonCard.LoadPersonInfo(int.Parse(txtFind.Text));
            }
            else if (!string.IsNullOrEmpty(txtFind.Text) && _AnyPerson == enAnyPerson.Doctor)
            {
                if (clsDoctor.IsDoctor(int.Parse(txtFind.Text)))
                {
                    ctrlPersonCard.LoadPersonInfo(int.Parse(txtFind.Text));
                }
                else
                {
                    MessageBox.Show("Wrong ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCard.ResetPersonInfo();
                }

            }

            if (cbFindBy.Text == "Name" && cbPeopleList.Text != "None")
           {
                string Name = "";

                if (_AnyPerson == enAnyPerson.Doctor)
                {
                    Name = cbPeopleList.Text.Replace("Dr. ","");
                }
                else
                {
                    Name = cbPeopleList.Text;
                }

                ctrlPersonCard.LoadPersonInfo(Name);
           }

            if (OnSelectedPerson != null && FilterEnabled)
                OnSelectedPerson?.Invoke(PersonID);
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            TextFilterFocus();
            cbFindBy.SelectedIndex = 0;
            _FillPeopleComboBox();
            cbPeopleList.SelectedIndex = 0;

        }

     /*   private void txtFind_Validating(object sender, CancelEventArgs e)
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
        }*/

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }

        private void cbPeopleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFind.PerformClick();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson();
            form.ShowDialog();
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFind.Visible = (cbFindBy.Text == "Person ID");

            cbPeopleList.Visible = (cbFindBy.Text == "Name");

            txtFind.Text = "";
            cbPeopleList.Text = "None";

            if (txtFind.Visible)
            {
                txtFind.Focus();
            }
        }

       
    }
}
