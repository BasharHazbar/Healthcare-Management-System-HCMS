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

namespace HCMS.Doctors.Controls
{
    public partial class ctrlDoctorDetails : UserControl
    {
        private int _DoctorID;

        private clsDoctor _Doctor;
        public ctrlDoctorDetails()
        {
            InitializeComponent();
        }

    


        public int DoctorID
        {
            get { return _DoctorID; }
        }

        public clsDoctor Doctor
        {
            get { return _Doctor; }
        }

        public void LoadDoctorInfo(int DoctorID)
        {
            _Doctor = clsDoctor.Find(DoctorID); 

            if (_Doctor == null)
            {
                MessageBox.Show("No Doctor with DoctorID = " + DoctorID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _RestDoctorInfo();
                return;
            }
            _FillDoctorInfo();
        }


        void _FillDoctorInfo()
        {
            _DoctorID = _Doctor.DoctorID;
            lblDoctorID.Text = _DoctorID.ToString();
            lblSpecialization.Text = _Doctor.Specialization;
            lblClinicAddress.Text = _Doctor.ClinicAddress;
            ctrlPersonCard1.LoadPersonInfo(_Doctor.PersonID);
        }

        void _RestDoctorInfo()
        {
            lblDoctorID.Text = "[????]";
            lblSpecialization.Text = "[????]";
            lblClinicAddress.Text = "[????]";
        }

    }
}
