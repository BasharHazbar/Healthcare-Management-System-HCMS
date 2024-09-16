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
    public partial class frmListPatients : Form
    {
        private DataTable _dtPatients;
        public frmListPatients()
        {
            InitializeComponent();
        }

        private void frmListPatients_Load(object sender, EventArgs e)
        {
            _dtPatients = clsPatient.GetAllPatients();

            dgvListPatients.DataSource = _dtPatients;

            dgvListPatients.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgvListPatients.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            cbFindBy.SelectedIndex = 0;

            if (dgvListPatients.Rows.Count > 0)
            {
                dgvListPatients.Columns[0].HeaderText = "Patient ID";
                dgvListPatients.Columns[0].Width = 120;

                dgvListPatients.Columns[1].HeaderText = "Person ID";
                dgvListPatients.Columns[1].Width = 120;

                dgvListPatients.Columns[2].HeaderText = "Full Name";
                dgvListPatients.Columns[2].Width = 420;

                dgvListPatients.Columns[3].HeaderText = "Address";
                dgvListPatients.Columns[3].Width = 230;

                lblRecordsCount.Text = dgvListPatients.Rows.Count.ToString();
            }
        }

        private void btnAddNewPatient_Click(object sender, EventArgs e)
        {
            frmAddUpdatePatient form = new frmAddUpdatePatient();
            form.ShowDialog();
            frmListPatients_Load(null,null);

        }

        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFindBy.Text)
            {
                case "Doctor ID":
                    FilterColumn = "DoctorID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Address":
                    FilterColumn = "Address";
                    break;
                default:
                    FilterColumn = "All";
                    break;

            }

            if (txtFindBy.Text.Trim() == "" || FilterColumn == "All")
            {
                _dtPatients.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtPatients.Rows.Count.ToString();

                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "PatientID")
                _dtPatients.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFindBy.Text.Trim());
            else
                _dtPatients.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFindBy.Text.Trim());

            lblRecordsCount.Text = _dtPatients.Rows.Count.ToString();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFindBy.Text == "Person ID" || cbFindBy.Text == "Patient ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void addNewDoctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePatient form = new frmAddUpdatePatient();
            form.ShowDialog();
            frmListPatients_Load(null,null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePatient form = new frmAddUpdatePatient((int)dgvListPatients.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            frmListPatients_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPatientDetails form = new frmPatientDetails((int)dgvListPatients.CurrentRow.Cells[1].Value);
            form.ShowDialog();
        }

        private void dgvListPatients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmPatientDetails form = new frmPatientDetails((int)dgvListPatients.CurrentRow.Cells[1].Value);
            form.ShowDialog();
        }
    }
}
