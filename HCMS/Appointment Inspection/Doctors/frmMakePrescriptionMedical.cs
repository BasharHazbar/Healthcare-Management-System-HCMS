using HCMS.Classes;
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

namespace HCMS.Appointment_Inspection.Doctors
{
    public partial class frmMakePrescriptionMedical : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode = enMode.AddNew;

        private clsPrescription _Prescription;

        private int _PrescriptionID;

        private clsMedicalRecord _MedicalRecord;

        private int _MedicalRecordID;

        public frmMakePrescriptionMedical(int MedicalRecordID)
        {
            InitializeComponent();
            _MedicalRecordID = MedicalRecordID;
        }

        void _ResetDefultValue()
        {
            if (_Mode == enMode.AddNew)
            {
                _Prescription = new clsPrescription();
                lbTitle.Text = "Add Prescription Medical";
                this.Text = "Add Prescription Medical";
            }
            else
            {
                lbTitle.Text = "Update Prescription Medical";
                this.Text = "Update Prescription Medical";
            }

            _MedicalRecord = clsMedicalRecord.Find(_MedicalRecordID);

            if (_MedicalRecord == null)
            {
                MessageBox.Show("There is No MedicalRecord", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlPersonCard.LoadPersonInfo(_MedicalRecord.AppointmentInfo.PatientInfo.PersonID);

            this.Height = 775;
            lblPrescriptionID.Text = "[????]";
            txtSpecialInstructions.Text = "";
            txtMedicationDetails.Text = "";
            txtDosage.Text = "";
            txtSpecialInstructions.Text = "";
        }

        void _LoadData()
        {
            _Prescription = clsPrescription.Find(_PrescriptionID);

            if (_Prescription == null)
            {
                MessageBox.Show("No Prescription Medical with ID = " + _PrescriptionID, "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            _PrescriptionID = _Prescription.PrescriptionID;
            lblPrescriptionID.Text = _PrescriptionID.ToString();
            txtSpecialInstructions.Text = _Prescription.SpecialInstructions;
            txtMedicationDetails.Text = _Prescription.MedicationDetails;
            txtDosage.Text = _Prescription.Dosage;
        }

        private void frmMakePrescriptionMedical_Load(object sender, EventArgs e)
        {
            _ResetDefultValue();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _Prescription.MedicationDetails = txtMedicationDetails.Text;
            _Prescription.Dosage  = txtDosage.Text;
            _Prescription.SpecialInstructions = txtSpecialInstructions.Text;
            _Prescription.MedicalRecordID = _MedicalRecord.MedicalRecordID;

            if (_Prescription.Save())
            {
                _Mode = enMode.Update;
                lbTitle.Text = "Update Prescription Medical";
                lblPrescriptionID.Text = _PrescriptionID.ToString();

                MessageBox.Show("Data Save Successflly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Not Save Successflly", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
