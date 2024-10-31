using HCMS.Classes;
using HCMS.Patients;
using HCMS.People;
using HCMS_Buisness;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HCMS.Appointment_Inspection.Doctors
{
    public partial class frmPatientsInspection : Form
    {

        private DataTable _dtPatientsInspection;
        public frmPatientsInspection()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPatientsInspection_Load(object sender, EventArgs e)
        {
            _dtPatientsInspection = clsAppointment.GetAppointmentsPerDoctorID(clsGlobal.CurrentUser.PersonID);
            dgvListPatientInspection.DataSource = _dtPatientsInspection;

            dgvListPatientInspection.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgvListPatientInspection.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            cbFindBy.SelectedIndex = 0;

            this.Width = 1200;

            if (dgvListPatientInspection.Rows.Count > 0)
            {
                dgvListPatientInspection.Columns[0].HeaderText = "Appointment ID";
                dgvListPatientInspection.Columns[0].Width = 150;

                dgvListPatientInspection.Columns[1].HeaderText = "Patient ID";
                dgvListPatientInspection.Columns[1].Width = 130;

                dgvListPatientInspection.Columns[2].HeaderText = "Patient Name";
                dgvListPatientInspection.Columns[2].Width = 270;

                dgvListPatientInspection.Columns[3].HeaderText = "Appointment Date";
                dgvListPatientInspection.Columns[3].Width = 170;

                dgvListPatientInspection.Columns[4].HeaderText = "End Time";
                dgvListPatientInspection.Columns[4].Width = 160;

                dgvListPatientInspection.Columns[5].HeaderText = "Notes";
                dgvListPatientInspection.Columns[5].Width = 170;

                dgvListPatientInspection.Columns[6].HeaderText = "Created Date";
                dgvListPatientInspection.Columns[6].Width = 170;

                dgvListPatientInspection.Columns[7].HeaderText = "Status";
                dgvListPatientInspection.Columns[7].Width = 130;

                lblRecordsCount.Text = dgvListPatientInspection.Rows.Count.ToString();
            }
        }

        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFindBy.Text)
            {
                case "Appointment ID":
                    FilterColumn = "AppointmentID";
                    break;

                case "Patient ID":
                    FilterColumn = "PatientID";
                    break;

                case "Patient Name":
                    FilterColumn = "FullName";
                    break;

                case "Appointment Status":
                    FilterColumn = "AppointmentStatus";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (txtFindBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPatientsInspection.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtPatientsInspection.Rows.Count.ToString();

                return;
            }

            if (FilterColumn == "AppointmentID" || FilterColumn == "PatientID")
                _dtPatientsInspection.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFindBy.Text.Trim());
            else
                _dtPatientsInspection.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFindBy.Text.Trim());

            lblRecordsCount.Text = _dtPatientsInspection.Rows.Count.ToString();
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFindBy.Visible = (cbFindBy.Text != "None");

            if (txtFindBy.Visible)
            {
                txtFindBy.Focus();
                txtFindBy.Text = "";
            }
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFindBy.Text == "Appointment ID" || cbFindBy.Text == "Patient ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDoctorDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsMedicalRecord MedicalRecord = clsMedicalRecord.FindByAppointmentID((int)dgvListPatientInspection.CurrentRow.Cells[0].Value);

            if (MedicalRecord == null)
            {
                MessageBox.Show("There is No MedicalRecord", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmMedicalRecoedDetails form = new frmMedicalRecoedDetails(MedicalRecord.MedicalRecordID);
            form.ShowDialog();
        }

        private void dgvListPatientInspection_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            clsMedicalRecord MedicalRecord = clsMedicalRecord.FindByAppointmentID((int)dgvListPatientInspection.CurrentRow.Cells[0].Value);

/*            if (MedicalRecord == null)
            {
                MessageBox.Show("There is No MedicalRecord", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
*/
            frmMedicalRecoedDetails form = new frmMedicalRecoedDetails();
            form.ShowDialog();
        }

        private void CancelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this Appointment ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsAppointment Appointment = clsAppointment.Find((int)dgvListPatientInspection.CurrentRow.Cells[0].Value);

            if (Appointment != null)
            {
                if (Appointment.Cancele())
                {
                    MessageBox.Show("Appointment Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    FrmPatientsInspection_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel Appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void makeMedicalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsAppointment Appointment = clsAppointment.Find((int)dgvListPatientInspection.CurrentRow.Cells[0].Value);

            if (Appointment == null)
            {
                MessageBox.Show("There is no appointment with ID = " + (int)dgvListPatientInspection.CurrentRow.Cells[0].Value, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmMakeMedicalRecord form = new frmMakeMedicalRecord(Appointment.AppointmentID);
            form.ShowDialog();
        }

        private void makePrescriptionMedicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsMedicalRecord MedicalRecord = clsMedicalRecord.FindByAppointmentID((int)dgvListPatientInspection.CurrentRow.Cells[0].Value);

            if (MedicalRecord == null)
            {
                MessageBox.Show("There is No MedicalRecord", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmMakePrescriptionMedical form = new frmMakePrescriptionMedical(MedicalRecord.MedicalRecordID);
            form.ShowDialog();
        }

        private void cmsPatientInspection_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

            bool IsMedicalExsit = !clsMedicalRecord.
                IsMedicalRecordExisByAppointmentID((int)dgvListPatientInspection.CurrentRow.Cells[0].Value);

            makeMedicalRecordToolStripMenuItem.Enabled = !IsMedicalExsit;

            makePrescriptionMedicalToolStripMenuItem.Enabled = IsMedicalExsit;

        }
    }
}
