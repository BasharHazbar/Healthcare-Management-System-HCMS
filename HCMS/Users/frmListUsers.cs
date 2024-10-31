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

namespace HCMS.Users
{
    public partial class frmListUsers : Form
    {
        private DataTable _dtUsers;
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtUsers = clsUser.GetAllUsers();

            dgvListUsers.DataSource = _dtUsers;

            dgvListUsers.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgvListUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            cbFindBy.SelectedIndex = 0;

            if (dgvListUsers.Rows.Count > 0)
            {
                dgvListUsers.Columns[0].HeaderText = "User ID";
                dgvListUsers.Columns[0].Width = 120;

                dgvListUsers.Columns[1].HeaderText = "Person ID";
                dgvListUsers.Columns[1].Width = 120;

                dgvListUsers.Columns[2].HeaderText = "Full Name";
                dgvListUsers.Columns[2].Width = 300;

                dgvListUsers.Columns[3].HeaderText = "User Name";
                dgvListUsers.Columns[3].Width = 140;

                dgvListUsers.Columns[4].HeaderText = "User Role";
                dgvListUsers.Columns[4].Width = 130;

                dgvListUsers.Columns[5].HeaderText = "Is Active";
                //dgvListUsers.Columns[4].Width = 130;

                lblRecordsCount.Text = dgvListUsers.Rows.Count.ToString();
            }
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFindBy.Visible = (cbFindBy.Text != "None") && (cbFindBy.Text != "Is Active") 
                && (cbFindBy.Text != "User Role");

            cbIsActive.Visible = cbFindBy.Text == "Is Active";

            cbRole.Visible = cbFindBy.Text == "User Role";

            if (cbFindBy.Text == "Is Active")
            {
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();

            }
            else if (cbFindBy.Text == "User Role")
            {

                cbRole.SelectedIndex = 0;
                cbRole.Focus();
            }
            else
            {
                if (txtFindBy.Visible)
                {
                    txtFindBy.Focus();
                    txtFindBy.Text = "";
                }
            }
        }

        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFindBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "User Name":
                    FilterColumn = "UserName";
                    break;

                case "User Role":
                    FilterColumn = "Role";
                    break;

                case "Is Active":
                    FilterColumn = "LastName";
                    break;
                default:
                    FilterColumn = "All";
                    break;

            }

            if (txtFindBy.Text.Trim() == "" || FilterColumn == "All")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtUsers.Rows.Count.ToString();

                return;
            }
            
            if (_dtUsers.Rows.Count > 0)
            {

                if (FilterColumn == "PersonID" || FilterColumn == "UserID")
                    _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFindBy.Text.Trim());
                else
                    _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFindBy.Text.Trim());

                lblRecordsCount.Text = _dtUsers.Rows.Count.ToString();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "None" || FilterColumn == "All")
                _dtUsers.DefaultView.RowFilter = "";
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = _dtUsers.Rows.Count.ToString();
        }


        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Role";
            string FilterValue = cbRole.Text;

            switch (FilterValue)
            {
                case "Admin":
                    FilterValue = "Admin";
                    break;
                case "Patient":
                    FilterValue = "Patient";
                    break;
                case "Doctor":
                    FilterValue = "Doctor";
                    break;
                default:
                    FilterValue = "All";
                    break;
            }

            if (FilterValue == "None" || FilterColumn == "All")
            {
                _dtUsers.DefaultView.RowFilter = "";
            }
            else
            {

                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, FilterValue);
            }

            lblRecordsCount.Text = _dtUsers.Rows.Count.ToString();
        }


        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFindBy.Text == "Person ID" || cbFindBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dgvListPeople_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUserDetails form = new frmUserDetails((int)dgvListUsers.CurrentRow.Cells[0].Value);
            form.ShowDialog();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser form = new frmAddUpdateUser();
            form.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails form = new frmUserDetails((int)dgvListUsers.CurrentRow.Cells[0].Value);
            form.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser form = new frmAddUpdateUser((int)dgvListUsers.CurrentRow.Cells[0].Value);
            form.ShowDialog();

            frmListUsers_Load(null,null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete User [" + dgvListUsers.CurrentRow.Cells[0].Value + "].",
               "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;



            if (clsPerson.DeletePerson((int)dgvListUsers.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("User Deleted Successfuly.", "Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListUsers_Load(null, null);
            }
            else
            {
                MessageBox.Show("User was not deleted because it has data linked to it.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmChangePassword form = new frmChangePassword((int)dgvListUsers.CurrentRow.Cells[0].Value);
            form.ShowDialog();
        }
    }
}
