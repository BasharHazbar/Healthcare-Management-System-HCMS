using HCMS.Appointment_Inspection;
using HCMS.Appointment_Inspection.Doctors;
using HCMS.Classes;
using HCMS.Doctors;
using HCMS.Patients;
using HCMS.People;
using HCMS.Users;
using HCMS_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCMS
{
    public partial class frmMain : Form
    {
        private frmLogIn _LogIn;

        private Timer _ImageTimer;
        private int _ImageIndex = 0;
        private readonly string[] _ImageFiles = {
            @"D:\HCMS Project\Background Images\Image_1.jpg",
            @"D:\HCMS Project\Background Images\Image_2.jpg",
            @"D:\HCMS Project\Background Images\Image_3.jpg",
            @"D:\HCMS Project\Background Images\Image_4.jpg"
        };
        
        public frmMain(frmLogIn logIn)
        {
            InitializeComponent();
            _LogIn = logIn;

        }

        void _ManageAccessUser()
        {
           usersToolStripMenuItem.Enabled = clsGlobal.CurrentUser.Role == clsUser.enRole.Admin;
           patientToolStripMenuItem.Enabled = clsGlobal.CurrentUser.Role == clsUser.enRole.Admin;
           doctotsToolStripMenuItem.Enabled = clsGlobal.CurrentUser.Role == clsUser.enRole.Admin;
           peopleToolStripMenuItem.Enabled = clsGlobal.CurrentUser.Role == clsUser.enRole.Admin;
           AppointmentInspectionDoctortoolStripMenuItem2.Enabled = clsGlobal.CurrentUser.Role == clsUser.enRole.Patient;
           patientInspectionToolStripMenuItem.Enabled = clsGlobal.CurrentUser.Role == clsUser.enRole.Doctor;

        }


        private void frmMain_Load(object sender, EventArgs e)
        {

            _ManageAccessUser();
            //CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            _ImageTimer = new Timer
            {
                Interval = 5000
            };
            _ImageTimer.Tick += OnImageChange;
            _ImageTimer.Start();
        }

        private void OnImageChange(object sender, EventArgs e)
        {

           

            try
            {
                _ImageIndex = (_ImageIndex + 1) % _ImageFiles.Length;
                pbBackground.Image = Image.FromFile(_ImageFiles[_ImageIndex]);
            }
            catch(IOException ex) {

                MessageBox.Show("Exseption Error " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
        }
        private void doctotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDoctor form = new frmListDoctor();
            form.ShowDialog();
        }

        private void patientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPatients form = new frmListPatients();
            form.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople form = new frmListPeople();
            form.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers form = new frmListUsers();
            form.ShowDialog();
        }

        private void currentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails form = new frmUserDetails(clsGlobal.CurrentUser.UserID);
            form.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword form = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            form.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            this.Close();
            _LogIn.Show();
        }

        private void AppointmentInspectionDoctortoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //frmAddUpdateInspectionAppointment form = new frmAddUpdateInspectionAppointment();

            FrmAppointmentInspection form = new FrmAppointmentInspection();
            form.ShowDialog();
        }

        private void patientInspectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPatientsInspection Form = new frmPatientsInspection();
            Form.ShowDialog();  
        }
    }
}
