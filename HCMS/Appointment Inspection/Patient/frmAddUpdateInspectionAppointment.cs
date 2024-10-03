using HCMS.Classes;
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

namespace HCMS.Appointment_Inspection
{
    public partial class frmAddUpdateInspectionAppointment : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode = enMode.AddNew;

        private int _AppointmentID;

        private clsAppointment _Appointment;

        public frmAddUpdateInspectionAppointment()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateInspectionAppointment(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _Mode = enMode.Update;
        }



        private void _ResetDefultValue()
        {
           
            if (_Mode == enMode.AddNew)
            {
                _Appointment = new clsAppointment();
                lbTitle.Text = "Add New Inspection Appointment";
            }
            else
            {
                lbTitle.Text = "Update Inspection Appointment";
            }

            dtpAppointmentDate.Enabled = false;
            txtNotes.Enabled = false;
            btnSave.Enabled = false;

            dtpAppointmentDate.MinDate = DateTime.Now.AddDays(1);
            lblAppointmentID.Text = "[????]";
            txtNotes.Text = "";

        }

        private void _LoadData()
        {
             _Appointment = clsAppointment.Find(_AppointmentID);

            if (_Appointment == null)
            {
                MessageBox.Show("No Inspection Appointment with ID = " + _AppointmentID , "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            _AppointmentID = _Appointment.AppointmentID;
            lblAppointmentID.Text = _AppointmentID.ToString();
            dtpAppointmentDate.Value = _Appointment.AppointmentDate;
            txtNotes.Text = _Appointment.Notes;
        }


        private void frmAddUpdateInspectionAppointment_Load(object sender, EventArgs e)
        {
            _ResetDefultValue();

            if (_Mode == enMode.Update)
                _LoadData();
                
        }

        private void ctrlPersonCardWithFilter_OnSelectedPerson(int obj)
        {

            if (clsAppointment.IsTherActiveInspectionAppointment(clsGlobal.CurrentUser.PersonID))
            {
                MessageBox.Show("You Aready Have an Active Inspection Appointment!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (obj == -1)
            {
                dtpAppointmentDate.Enabled = false;
                txtNotes.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                dtpAppointmentDate.Enabled = true;
                txtNotes.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!clsDoctor.IsDoctor(ctrlPersonCardWithFilter.PersonID))
            {
                MessageBox.Show("Please Select A Doctor!",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsAppointment.IsTherActiveInspectionAppointment(clsGlobal.CurrentUser.PersonID))
            {
                MessageBox.Show("You Aready Have an Active Inspection Appointment!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // if (_Mode == enMode.AddNew && clsPatient.IsPatientExist(clsGlobal.CurrentUser.PersonInfo.))


            _Appointment.PatientID = clsPatient.FindByPersonID(clsGlobal.CurrentUser.PersonID).PatientID;
            _Appointment.DoctorID = clsDoctor.FindByPersonID(ctrlPersonCardWithFilter.PersonID).DoctorID; 
            _Appointment.AppointmentDate = dtpAppointmentDate.Value;
            _Appointment.Status = clsAppointment.enStatus.Scheduled;
            _Appointment.CreatedBy = clsGlobal.CurrentUser.UserID;

            _Appointment.Notes = txtNotes.Text.Trim();

            if (_Appointment.Save())
            {
                _AppointmentID = _Appointment.AppointmentID;
                lblAppointmentID.Text = _AppointmentID.ToString();
                _Mode = enMode.Update;

                lbTitle.Text = "Update Inspection Appointment";

                MessageBox.Show("Data Save Successflly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Not Save Successflly", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
