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

namespace HCMS.Users
{
    public partial class frmAddUpdateUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode = enMode.AddNew;

        private int _UserID = -1;

        private clsUser _User;

        public frmAddUpdateUser()
        {
            this._Mode = enMode.AddNew;
            InitializeComponent();
        }

        public frmAddUpdateUser(int UserID)
        {
            this._Mode = enMode.Update;
            this._UserID = UserID;
            InitializeComponent();
        }

        void _ResetDefultValue()
        {
            if (_Mode == enMode.AddNew)
            {
                lbTitle.Text = "Add New User";
                _User = new clsUser();
                tpUserInfo.Enabled = false;
            }
            else
            {
                lbTitle.Text = "Update User";
            }

            lblUserID.Text = "[????]";
            txtUserName.Text = "";
            txtPassword.Text = "";
            cbRole.SelectedIndex = 0;
            txtConfirmPassword.Text = "";
        }

        void _LoadData()
        {
            _User = clsUser.Find(_UserID);
            ctrlPersonCardWithFilter.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            tpUserInfo.Enabled = true;

            _UserID = _User.UserID;
            lblUserID.Text = _UserID.ToString();
            txtUserName.Text = _User.UserName;
/*            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;*/
            chkIsActive.Checked = _User.IsActive; 

            switch (_User.Role)
            {
                case clsUser.enRole.Admin:
                    cbRole.Text = "Admin";
                    break;
                case clsUser.enRole.Doctor:
                    cbRole.Text = "Doctor";
                    break;
                case clsUser.enRole.Patient:
                    cbRole.Text = "Patient";
                    break;
                default:
                    break;
            }

            ctrlPersonCardWithFilter.LoadPersonData(_User.PersonID);
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefultValue();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "The User Name Shoud not be Empty!");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }

            if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "The User Name Is Used By another User!");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
            }
            else
            {
                if (_User.UserName != txtUserName.Text.Trim())
                {

                    if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                    {
                        
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "The User Name Is Used By another User!");
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_Mode == enMode.Update && string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "The User Password Shoud Not be Empty!");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_Mode == enMode.Update && string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                return;
            }
              
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some Fields Not Valid, Put the Mouse over the Icon(s) to know the Reason!", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.PersonID = ctrlPersonCardWithFilter.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = clsGlobal.HashPassword(txtPassword.Text.Trim());
            _User.IsActive = chkIsActive.Checked;

            switch (cbRole.Text.Trim())
            {
                case "Admin":
                    _User.Role = clsUser.enRole.Admin;
                    break;
                case "Doctro":
                    _User.Role = clsUser.enRole.Doctor;
                    break;
                case "Patient":
                    _User.Role = clsUser.enRole.Patient;
                    break;
                default:
                    break;
            }

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;

                lbTitle.Text = "Update Person";

                MessageBox.Show("Data Save Successflly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Not Save Successflly", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter.TextFilterFocus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpUserInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpUserInfo"];
                return;
            }

            if (ctrlPersonCardWithFilter.PersonID != -1)
            {
                if (clsUser.IsUserExistByPersonID(ctrlPersonCardWithFilter.PersonID))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter.TextFilterFocus();

                }
                else
                {
                    btnSave.Enabled = true;
                    tpUserInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpUserInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter.TextFilterFocus();
            }
        }
    }
}
