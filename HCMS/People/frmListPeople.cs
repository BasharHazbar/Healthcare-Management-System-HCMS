using HCMS_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HCMS.People
{
    public partial class frmListPeople : Form
    {
        private DataTable _dtAllPeople;

        private DataTable _dtPeople;
        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            _dtAllPeople = clsPerson.GetAllPeople();
  
            if (_dtAllPeople.Rows.Count > 0)
            {
                _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID",
                                             "FirstName", "SecondName", "ThirdName", "LastName",
                                             "GendorCaption", "DateOfBirth", "Email", "PhoneNumber");
            }
            dgvListPeople.DataSource = _dtPeople;

            dgvListPeople.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgvListPeople.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            cbFindBy.SelectedIndex = 0;

            this.Width = 1200;

            if (dgvListPeople.Rows.Count > 0)
            {
                dgvListPeople.Columns[0].HeaderText = "Person ID";
                dgvListPeople.Columns[0].Width = 120;

                dgvListPeople.Columns[1].HeaderText = "First Name";
                dgvListPeople.Columns[1].Width = 130;

                dgvListPeople.Columns[2].HeaderText = "Second Name";
                dgvListPeople.Columns[2].Width = 140;

                dgvListPeople.Columns[3].HeaderText = "Third Name";
                dgvListPeople.Columns[3].Width = 130;

                dgvListPeople.Columns[4].HeaderText = "Last Name";
                dgvListPeople.Columns[4].Width = 130;

                dgvListPeople.Columns[5].HeaderText = "Gender";
                dgvListPeople.Columns[5].Width = 130;

                dgvListPeople.Columns[6].HeaderText = "Date Of Birth";
                dgvListPeople.Columns[6].Width = 130;

                dgvListPeople.Columns[7].HeaderText = "Email";
                dgvListPeople.Columns[7].Width = 200;

                dgvListPeople.Columns[8].HeaderText = "Phone";
                dgvListPeople.Columns[8].Width = 120;

                lblRecordsCount.Text = dgvListPeople.Rows.Count.ToString();
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

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Gender":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "PhoneNumber";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn = "All";
                    break;

            }

            if (txtFindBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtPeople.Rows.Count.ToString();

                return;
            }

            if (_dtAllPeople.Rows.Count > 0)
            {
                if (FilterColumn == "PersonID")
                    _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFindBy.Text.Trim());
                else
                    _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFindBy.Text.Trim());

                lblRecordsCount.Text = _dtPeople.Rows.Count.ToString();
            }
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson();
            form.ShowDialog();
            frmListPeople_Load(null,null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (cbFindBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails form = new frmPersonDetails((int)dgvListPeople.CurrentRow.Cells[0].Value);
            form.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson();
            form.ShowDialog();
            frmListPeople_Load(null,null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson((int)dgvListPeople.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            frmListPeople_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvListPeople.CurrentRow.Cells[0].Value + "].",
                "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
        


            if (clsPerson.DeletePerson((int)dgvListPeople.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Person Deleted Successfuly.", "Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListPeople_Load(null, null);
            }
            else
            {
                MessageBox.Show("Person was not deleted because it has data linked to it.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void dgvListPeople_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmPersonDetails form = new frmPersonDetails((int)dgvListPeople.CurrentRow.Cells[0].Value);
            form.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }
}
