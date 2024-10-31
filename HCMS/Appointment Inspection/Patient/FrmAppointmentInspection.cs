using HCMS.Classes;
using HCMS.Doctors;
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
    public partial class FrmAppointmentInspection : Form
    {
        private  DataTable _dtAppointments;
        public FrmAppointmentInspection()
        {
            InitializeComponent();
        }

        private void FrmAppointmentInspection_Load(object sender, EventArgs e)
        {

            _dtAppointments = clsAppointment.GetAppointmentsPerPatientID(clsGlobal.CurrentUser.PersonID);

            dgvListAppointments.DataSource = _dtAppointments;

            dgvListAppointments.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgvListAppointments.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            cbFindBy.SelectedIndex = 0;

            this.Width = 1200;

            if (dgvListAppointments.Rows.Count > 0)
            {
                dgvListAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvListAppointments.Columns[0].Width = 130;

                dgvListAppointments.Columns[1].HeaderText = "Doctor ID";
                dgvListAppointments.Columns[1].Width = 130;

                dgvListAppointments.Columns[2].HeaderText = "Doctor Name";
                dgvListAppointments.Columns[2].Width = 250;

                dgvListAppointments.Columns[3].HeaderText = "Specialization";
                dgvListAppointments.Columns[3].Width = 120;

                dgvListAppointments.Columns[4].HeaderText = "Clinic Address";
                dgvListAppointments.Columns[4].Width = 160;

                dgvListAppointments.Columns[5].HeaderText = "Appointment Date";
                dgvListAppointments.Columns[5].Width = 200;

                dgvListAppointments.Columns[6].HeaderText = "End Time";
                dgvListAppointments.Columns[6].Width = 130;

                dgvListAppointments.Columns[7].HeaderText = "Created Date";
                dgvListAppointments.Columns[7].Width = 200;

                dgvListAppointments.Columns[8].HeaderText = "Status";
                dgvListAppointments.Columns[8].Width = 130;

                lblRecordsCount.Text = dgvListAppointments.Rows.Count.ToString();
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

                case "Doctor ID":
                    FilterColumn = "DoctorID";
                    break;

                case "Doctor Name":
                    FilterColumn = "FullName";
                    break;

                case "Specialization":
                    FilterColumn = "Specialization";
                    break;

                case "Clinic Address":
                    FilterColumn = "Clinic Address";
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
                _dtAppointments.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAppointments.Rows.Count.ToString();

                return;
            }

            if (FilterColumn == "AppointmentID" || FilterColumn == "DoctorID")
                _dtAppointments.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFindBy.Text.Trim());
            else
                _dtAppointments.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFindBy.Text.Trim());

            lblRecordsCount.Text = _dtAppointments.Rows.Count.ToString();
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFindBy.Text == "Appointment ID" || cbFindBy.Text == "Doctor ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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

        private void btnAddNewInspectionAppoinment_Click(object sender, EventArgs e)
        {

            clsPatient Patient = clsPatient.FindByPersonID(clsGlobal.CurrentUser.PersonID);

            if (Patient == null)
            {
                MessageBox.Show("No Patient!",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           

            if (clsAppointment.IsTherActiveInspectionAppointment(Patient.PatientID))
            {
                MessageBox.Show("You Aready Have an Active Inspection Appointment!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmAddUpdateInspectionAppointment form = new frmAddUpdateInspectionAppointment();
            form.ShowDialog();

            FrmAppointmentInspection_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDoctorDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoctorDetails form = new frmDoctorDetails((int)dgvListAppointments.CurrentRow.Cells[1].Value);
            form.ShowDialog();
        }
        private void dgvListAppointments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmDoctorDetails form = new frmDoctorDetails((int)dgvListAppointments.CurrentRow.Cells[1].Value);
            form.ShowDialog();
        }
        private void DeleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete Doctor [" + dgvListAppointments.CurrentRow.Cells[0].Value + "].",
                     "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;



            if (clsAppointment.DeleteAppointment((int)dgvListAppointments.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Inspection Appointment Deleted Successfuly.", "Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmAppointmentInspection_Load(null, null);
            }
            else
            {
                MessageBox.Show("Inspection Appointment was not deleted.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void CancelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this Appointment ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsAppointment Appointment = clsAppointment.Find((int)dgvListAppointments.CurrentRow.Cells[0].Value);

            if (Appointment != null)
            {
                if (Appointment.Cancele())
                {
                    MessageBox.Show("Appointment Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    FrmAppointmentInspection_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel Appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

       
    }
}
