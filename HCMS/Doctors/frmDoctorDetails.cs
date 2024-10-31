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
    public partial class frmDoctorDetails : Form
    {

        private int _DoctorID;

        public frmDoctorDetails(int DoctorID)
        {
            InitializeComponent();
            _DoctorID = DoctorID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDoctorDetails_Load(object sender, EventArgs e)
        {
            ctrlDoctorDetails1.LoadDoctorInfo(_DoctorID);
        }
    }
}
