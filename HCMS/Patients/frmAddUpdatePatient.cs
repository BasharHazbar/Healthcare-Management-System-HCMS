using HCMS.People.Controls;
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

namespace HCMS.Patients
{
    public partial class frmAddUpdatePatient : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode = enMode.AddNew;

        private int _PatientID;

        private clsPatient _Patient;


        public frmAddUpdatePatient()
        {
            _Mode = enMode.AddNew;
            InitializeComponent();
        }

        public frmAddUpdatePatient(int PatientID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PatientID = PatientID;
        }

        void _ResetDefultValue()
        {
            if (_Mode == enMode.AddNew)
            {
                lbTitle.Text = "Add New Patient";
                _Patient = new clsPatient();
            }
            else
            {
                lbTitle.Text = "Update Patient";
            }
        }

        void _LoadData()
        {
            _Patient = clsPatient.Find(_PatientID);

            if (_Patient == null)
            {
                MessageBox.Show("No Patient with ID = " + _PatientID, "Patient Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            ctrlPersonCardWithFilter1.LoadPersonData(_Patient.PersonID);
        }

        private void frmAddUpdatePatient_Load(object sender, EventArgs e)
        {
            _ResetDefultValue();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
 
            _Patient.PersonID = ctrlPersonCardWithFilter1.PersonID;

            if (clsDoctor.IsDoctor(ctrlPersonCardWithFilter1.PersonID))
            {

                MessageBox.Show("Selected Person already a Doctor, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.TextFilterFocus();
                return;
            }
            else if (clsPatient.IsPatient(ctrlPersonCardWithFilter1.PersonID))
            {

                MessageBox.Show("Selected Person already a Patient, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.TextFilterFocus();
                return;
            }

            if (_Patient.Save())
            {
                _PatientID = _Patient.PatientID;
                _Mode = enMode.Update;

                lbTitle.Text = "Update Patient";

                MessageBox.Show("Data Save Successflly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Not Save Successflly", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
