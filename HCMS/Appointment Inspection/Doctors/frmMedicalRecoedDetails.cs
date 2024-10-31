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

namespace HCMS.Appointment_Inspection.Doctors
{
    public partial class frmMedicalRecoedDetails : Form
    {
        private int _MedicalRecordID = -1;


        public frmMedicalRecoedDetails()
        {
            InitializeComponent();
        }
        public frmMedicalRecoedDetails(int MedicalRecordID)
        {
            InitializeComponent();
            this._MedicalRecordID = MedicalRecordID;
        }

        private void frmMedicalRecoedDetails_Load(object sender, EventArgs e)
        {
            clsMedicalRecord MedicalRecord = clsMedicalRecord.Find(_MedicalRecordID);

            _MedicalRecordID = -1;

            if (MedicalRecord == null)
            {
                MessageBox.Show("No MedicalRecord with ID " + _MedicalRecordID, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlMedicalRecoredCard1.LoadMedicalRecordInfo(MedicalRecord.MedicalRecordID);
            clsMedicalRecordPrescriptionsCard1.LoadMedicalRecordPrescriptions(MedicalRecord.MedicalRecordID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
