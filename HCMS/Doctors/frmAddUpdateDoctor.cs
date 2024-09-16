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
using static HCMS_Buisness.clsUser;

namespace HCMS.Doctors
{
    public partial class frmAddUpdateDoctor : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode = enMode.AddNew;

        private int _DoctorID = -1;

        private clsDoctor _Doctor;

        public frmAddUpdateDoctor()
        {
            _Mode = enMode.AddNew;
            InitializeComponent();
        }

        public frmAddUpdateDoctor(int Doctor)
        {
            _Mode = enMode.Update;
            _DoctorID = Doctor;
            InitializeComponent();
        }


        private void _ResetDefultValue()
        {
            if (_Mode == enMode.AddNew)
            {
                lbTitle.Text = "Add New Doctor";
                _Doctor = new clsDoctor();
                tpDoctorInfo.Enabled = false;
            }
            else
            {
                lbTitle.Text = "Update Doctor";
            }

            lblDoctorID.Text = "[????]";
            txtSpecialization.Text = "";
            txtClinicAddress.Text = "";
        }


        private void _LoadData()
        {
            _Doctor = clsDoctor.Find(_DoctorID);

            if (_Doctor == null)
            {
                MessageBox.Show("No Doctor with ID = " + _DoctorID, "Doctor Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            _DoctorID = _Doctor.DoctorID;
            lblDoctorID.Text = _Doctor.DoctorID.ToString();
            txtSpecialization.Text = _Doctor.Specialization;
            txtClinicAddress.Text = _Doctor.ClinicAddress;

            ctrlPersonCardWithFilter.LoadPersonData(_Doctor.PersonID);
        }

        private void frmAddUpdateDoctor_Load(object sender, EventArgs e)
        {
            _ResetDefultValue();

            if (_Mode == enMode.Update)
                _LoadData();    
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpDoctorInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpDoctorInfo"];
                return;
            }

            if (ctrlPersonCardWithFilter.PersonID != -1)
            {
                if (clsDoctor.IsDoctor(ctrlPersonCardWithFilter.PersonID))
                {

                    MessageBox.Show("Selected Person already a Doctor, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter.TextFilterFocus();

                }
                else if (clsDoctor.IsPatient(ctrlPersonCardWithFilter.PersonID))
                {

                    MessageBox.Show("Selected Person already a Patient, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter.TextFilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpDoctorInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpDoctorInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter.TextFilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSpecialization_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSpecialization.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSpecialization, "The Field is Required!");
            }
            else
            {
                errorProvider1.SetError(txtSpecialization, null);
            }
        }

        private void txtClinicAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtClinicAddress.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtClinicAddress, "The Field is Required!");
            }
            else
            {
                errorProvider1.SetError(txtClinicAddress, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields Not Valid, Put the Mouse over the Icon(s) to know the Reason!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Doctor.PersonID = ctrlPersonCardWithFilter.PersonID;
            _Doctor.Specialization = txtSpecialization.Text;
            _Doctor.ClinicAddress = txtClinicAddress.Text;

            if (_Doctor.Save())
            {
                _DoctorID = _Doctor.DoctorID;
                lblDoctorID.Text = _DoctorID.ToString();
                _Mode = enMode.Update;

                lbTitle.Text = "Update Doctor";

                MessageBox.Show("Data Save Successflly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Not Save Successflly", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
