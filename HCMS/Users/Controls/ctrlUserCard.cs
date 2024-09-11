using HCMS.Properties;
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
using static HCMS_Buisness.clsUser;

namespace HCMS.Users
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _User;

        private int _UserID;

        public ctrlUserCard()
        {
            InitializeComponent();
        }

/*        public int UserID { get { return _UserID; } }

        public clsUser User { get { return _User; } }*/

        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.Find(UserID);

            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }


        private void _FillPersonInfo()
        {
            _UserID = _User.UserID;
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName;

            switch (_User.Role)
            {
                case clsUser.enRole.Admin:
                    lblRole.Text = "Admin";
                    break;
                case clsUser.enRole.Doctor:
                    lblRole.Text = "Doctor";
                    break;
                case clsUser.enRole.Patient:
                    lblRole.Text = "Patient";
                    break;
                default:
                    break;
            }
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";

            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
        }


        public void _ResetPersonInfo()
        {
            lblUserID.Text = "[????]";
            lblUserName.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblRole.Text = "[????]";
        }

    }
}
