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
    public partial class clsMedicalRecordPrescriptionsCard : UserControl
    {
     
        public clsMedicalRecordPrescriptionsCard()
        {
            InitializeComponent();
        }
        public void LoadMedicalRecordPrescriptions(int MedicalRecordID)
        {

            clsMedicalRecord MedicalRecord = clsMedicalRecord.Find(MedicalRecordID);

            if (MedicalRecord == null )
            {
                MessageBox.Show("No MedicalRecord with ID = " + MedicalRecordID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable _MedicalRecordPrescriptions = clsPrescription.GetAllPrescriptionsPerMedicalRecord(MedicalRecord.MedicalRecordID);


            dgvMedicalRecordPrescriptions.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgvMedicalRecordPrescriptions.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            dgvMedicalRecordPrescriptions.DataSource = _MedicalRecordPrescriptions;
            
            if (_MedicalRecordPrescriptions.Rows.Count  > 0)
            {
                dgvMedicalRecordPrescriptions.Columns[0].HeaderText = "Prescription ID";
                dgvMedicalRecordPrescriptions.Columns[0].Width = 150;

                dgvMedicalRecordPrescriptions.Columns[1].HeaderText = "Medication Details";
                dgvMedicalRecordPrescriptions.Columns[1].Width = 180;

                dgvMedicalRecordPrescriptions.Columns[2].HeaderText = "Dosage";
                dgvMedicalRecordPrescriptions.Columns[2].Width = 270;

                dgvMedicalRecordPrescriptions.Columns[3].HeaderText = "Prescription Date";
                dgvMedicalRecordPrescriptions.Columns[3].Width = 170;

                dgvMedicalRecordPrescriptions.Columns[4].HeaderText = "Special Instructions";
                dgvMedicalRecordPrescriptions.Columns[4].Width = 180;


                lblRecordsCount.Text = _MedicalRecordPrescriptions.Rows.Count.ToString();
            }
        }
    }
}
