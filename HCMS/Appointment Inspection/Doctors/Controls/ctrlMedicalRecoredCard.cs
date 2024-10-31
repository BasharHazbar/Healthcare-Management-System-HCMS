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

namespace HCMS.Appointment_Inspection.Doctors.Controls
{
    public partial class ctrlMedicalRecoredCard : UserControl
    {
        private clsMedicalRecord _MedicalRecord;
        public ctrlMedicalRecoredCard()
        {
            InitializeComponent();
        }

        public void LoadMedicalRecordInfo(int MedicalRecordID)
        {
            _MedicalRecord = clsMedicalRecord.Find(MedicalRecordID);
            if (_MedicalRecord == null)
            {
                _ResetMedicalRecordInfo();
                MessageBox.Show("No MedicalRecord with ID = " + MedicalRecordID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

             _FillMedicalRecordInfo();
        }

        private void _FillMedicalRecordInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_MedicalRecord.AppointmentInfo.PatientInfo.PersonID);
            lblMedicalRecordID.Text = _MedicalRecord.MedicalRecordID.ToString();
            lblDiagnosis.Text = _MedicalRecord.Diagnosis;
            lblVisitDescription.Text = _MedicalRecord.VisitDescription;
        }

        private void _ResetMedicalRecordInfo()
        {
            
            lblMedicalRecordID.Text = "[????]";
            lblDiagnosis.Text = "[????]";
            lblVisitDescription.Text = "[????]";
        }
    }
}
