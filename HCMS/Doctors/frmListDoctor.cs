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

namespace HCMS.Doctors
{
    public partial class frmListDoctor : Form
    {
        private DataTable _dtDoctors;
        public frmListDoctor()
        {
            InitializeComponent();
        }

        private void frmListDoctor_Load(object sender, EventArgs e)
        {
            _dtDoctors = clsDoctor.GetAllDoctors();

            dgvListDoctor.DataSource = _dtDoctors;

            dgvListDoctor.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgvListDoctor.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            cbFindBy.SelectedIndex = 0;

            if (dgvListDoctor.Rows.Count > 0)
            {
                dgvListDoctor.Columns[0].HeaderText = "Doctor ID";
                dgvListDoctor.Columns[0].Width = 120;

                dgvListDoctor.Columns[1].HeaderText = "Person ID";
                dgvListDoctor.Columns[1].Width = 120;

                dgvListDoctor.Columns[2].HeaderText = "Full Name";
                dgvListDoctor.Columns[2].Width = 300;

                dgvListDoctor.Columns[3].HeaderText = "Specialization";
                dgvListDoctor.Columns[3].Width = 180;

                dgvListDoctor.Columns[4].HeaderText = "Clinic Address";
                dgvListDoctor.Columns[4].Width = 180;

                //dgvListUsers.Columns[4].Width = 130;

                lblRecordsCount.Text = dgvListDoctor.Rows.Count.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

                case "Specialization":
                    FilterColumn = "Specialization";
                    break;

                case "Clinic Address":
                    FilterColumn = "ClinicAddress";
                    break;
                default:
                    FilterColumn = "All";
                    break;

            }

            if (txtFindBy.Text.Trim() == "" || FilterColumn == "All")
            {
                _dtDoctors.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtDoctors.Rows.Count.ToString();

                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "DoctorID")
                _dtDoctors.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFindBy.Text.Trim());
            else
                _dtDoctors.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFindBy.Text.Trim());

            lblRecordsCount.Text = _dtDoctors.Rows.Count.ToString();
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFindBy.Text == "Person ID" || cbFindBy.Text == "Doctor ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoctorDetails form = new frmDoctorDetails((int)dgvListDoctor.CurrentRow.Cells[0].Value);
            form.ShowDialog();
        }

        private void addNewDoctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateDoctor form = new frmAddUpdateDoctor((int)dgvListDoctor.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            frmListDoctor_Load(null,null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateDoctor form = new frmAddUpdateDoctor();
            form.ShowDialog();
            frmListDoctor_Load(null, null);
        }

        private void DeletetoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Doctor [" + dgvListDoctor.CurrentRow.Cells[0].Value + "].",
              "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;



            if (clsPerson.DeletePerson((int)dgvListDoctor.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Doctor Deleted Successfuly.", "Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListDoctor_Load(null, null);
            }
            else
            {
                MessageBox.Show("Doctor was not deleted because it has data linked to it.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddNewDoctor_Click(object sender, EventArgs e)
        {
            frmAddUpdateDoctor form = new frmAddUpdateDoctor();
            form.ShowDialog();
        }

        private void dgvListDoctor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmDoctorDetails form = new frmDoctorDetails((int)dgvListDoctor.CurrentRow.Cells[0].Value);
            form.ShowDialog();
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
    }
}
