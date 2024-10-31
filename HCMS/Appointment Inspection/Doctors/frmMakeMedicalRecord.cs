using HCMS.Classes;
using HCMS_Buisness;
using System;
using System.Windows.Forms;

namespace HCMS.Appointment_Inspection.Doctors
{
    public partial class frmMakeMedicalRecord : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode = enMode.AddNew;

        private clsMedicalRecord _MedicalRecord;

        private int _MedicalRecordID;

        private clsAppointment _Appointment;

        private int _AppointmentID;
        public frmMakeMedicalRecord(int AppointmentID)
        {
            InitializeComponent();
            this._AppointmentID = AppointmentID;
        }

        public frmMakeMedicalRecord(int AppointmentID, int MedicalRecordID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            this._AppointmentID = AppointmentID;
            this._MedicalRecordID = MedicalRecordID;
        }

        void _ResetDefultValue()
        {
            if (_Mode == enMode.AddNew)
            {
                _MedicalRecord = new clsMedicalRecord();
                lbTitle.Text = "Add Medical Record";
            }
            else
            {
                lbTitle.Text = "Update Medical Record";
            }

            _Appointment = clsAppointment.Find(_AppointmentID);

            if (_Appointment == null)
            {
                MessageBox.Show("No Appointment with Appointment ID = " + _AppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlPersonCard.LoadPersonInfo(_Appointment.PatientInfo.PersonID);

            lblMedicalRecordID.Text = "[????]";
            txtVisitDescription.Text = "";
        }

        void _LoadData()
        {
            _MedicalRecord = clsMedicalRecord.Find(_MedicalRecordID);

            if (_MedicalRecord == null )
            {
                MessageBox.Show("No Medical Record with ID = " + _MedicalRecordID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
           _MedicalRecordID = _MedicalRecord.MedicalRecordID;
           lblMedicalRecordID.Text = _MedicalRecordID.ToString();
           txtVisitDescription.Text = _MedicalRecord.VisitDescription;

        }

        private void frmMakeMedicalRecord_Load(object sender, EventArgs e)
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

            _MedicalRecord.AppointmentID = _AppointmentID;
            _MedicalRecord.Diagnosis = txtDiagnosis.Text.Trim();
            _MedicalRecord.VisitDescription = txtVisitDescription.Text.Trim();

            if (_MedicalRecord.Save())
            {
                _MedicalRecordID = _MedicalRecord.MedicalRecordID;
                lblMedicalRecordID.Text = _MedicalRecordID.ToString();
                _Mode = enMode.Update;

                _Appointment.SetCompleted();

                lbTitle.Text = "Update Medical Record";

                MessageBox.Show("Data Save Successflly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Not Save Successflly", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  
    }
}
