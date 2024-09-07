using HCMS.Classes;
using HCMS.Properties;
using HCMS_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCMS.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enum enGender { Male = 0, Female = 1 };

        private enMode _Mode = enMode.AddNew;

        private clsPerson _Person;

        private int _PersonID = -1;
        public frmAddUpdatePerson()
        {
            this._Mode = enMode.AddNew;
            InitializeComponent();
        }

        public frmAddUpdatePerson(int PersonID)
        {
            this._Mode = enMode.Update;
            this._PersonID = PersonID;
            InitializeComponent();
        }

        private void _ResetDefultValue()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                txtFirstName.Focus();
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }
            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible =  pbPersonImage.ImageLocation != null;

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtAddress.Text = "";
        }

       void _LoadData()
       {
            _Person = clsPerson.Find(_PersonID);

            if ( _Person == null )
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.PhoneNumber;
            txtAddress.Text = _Person.Address;

            if (_Person.Gender == (byte)enGender.Male)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }

            llRemoveImage.Visible = (_Person.ImagePath != "");
        }

        private bool _HandlPersonImage()
        {
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch(IOException) 
                    {

                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceImage = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToFolderProjectImages(ref SourceImage))
                    {
                        pbPersonImage.ImageLocation = SourceImage;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefultValue();
            
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void ValidationEmptyText(object sender, CancelEventArgs e)
        {
            TextBox Temp = sender as TextBox;

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "The Field is Required!");
            }
            else
            {
                errorProvider1.SetError(Temp,null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
                return;

            if (!clsValidatoin.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail,"Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Feilds Not Valid, Put the Mouse over the Icon(s) to know the Reason!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlPersonImage())
                return;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.PhoneNumber = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();

            if (rbMale.Checked)
            {
                _Person.Gender = (byte)enGender.Male;
            }
            else
            {
                _Person.Gender = (byte)enGender.Female;
                
            }

            if (pbPersonImage.ImageLocation != null)
            {
                _Person.ImagePath = pbPersonImage.ImageLocation;
            }
            else
            {
                _Person.ImagePath = "";
            }

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;

                lblTitle.Text = "Update Person";

                MessageBox.Show("Data Save Successflly", "Saved", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Not Save Successflly", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
           if (pbPersonImage.ImageLocation != null)
                pbPersonImage.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation != null)
                pbPersonImage.Image = Resources.Female_512;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.Load(openFileDialog1.FileName);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;

        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
