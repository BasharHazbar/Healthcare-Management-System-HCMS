using HCMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_Buisness
{
    public class clsPrescription
    {
        private enum enMode { AddNew = 0, Update = 1 }

        private enMode Mode = enMode.AddNew;

        public int PrescriptionID {  get; set; }
        public int MedicalRecordID { get; set; }
        clsMedicalRecord MedicalRecordInfo { get; set; }
        public string MedicationDetails { get; set; }
        public string Dosage { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string SpecialInstructions { get; set;}

        public clsPrescription()
        {
            this.PrescriptionID = -1;
            this.MedicalRecordID = -1;
            this.MedicationDetails = "";
            this.Dosage = "";
            this.SpecialInstructions = "";
            this.Mode = enMode.AddNew;
        }

        public clsPrescription(int PrescriptionID, int MedicalRecordID, string MedicationDetails, 
            string Dosage, DateTime PrescriptionDate, string SpecialInstructions)
        {
            this.PrescriptionID = PrescriptionID;
            this.MedicalRecordID = MedicalRecordID;
            this.MedicalRecordInfo = clsMedicalRecord.Find(MedicalRecordID);
            this.MedicationDetails = MedicationDetails;
            this.Dosage = Dosage;
            this.SpecialInstructions = SpecialInstructions;
            this.Mode = enMode.Update;
        }

        private bool _AddNewPrescription()
        {
            this.PrescriptionID = clsPrescriptionData.AddNewPrescription(this.MedicalRecordID,this.MedicationDetails,this.Dosage,
                this.SpecialInstructions);
            return this.PrescriptionID != -1;
        }

        private bool _UpdatePrescription()
        {
            return clsPrescriptionData.UpdatePrescription(this.PrescriptionID,this.MedicalRecordID,
                this.MedicationDetails,this.Dosage,this.PrescriptionDate,this.SpecialInstructions);
        }
        public static clsPrescription Find(int PrescriptionID)
        {
            int MedicalRecordID = -1;
            string MedicationDetails = "";
            string Dosage = "";
            DateTime PrescriptionDate = DateTime.Now;
            string SpecialInstructions = "";

            bool IsFound = clsPrescriptionData.GetPrescriptionInfoByID(PrescriptionID,ref MedicalRecordID,ref MedicationDetails
                ,ref Dosage,ref PrescriptionDate,ref SpecialInstructions);

            if (IsFound)
            {
                return new clsPrescription(PrescriptionID,MedicalRecordID,MedicationDetails,Dosage,PrescriptionDate,SpecialInstructions);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPrescription())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePrescription();

            }

            return false;
        }

        public static DataTable GetAllPrescriptionsPerMedicalRecord(int MedicalRecordID)
        {
            return clsPrescriptionData.GetAllPrescriptionsPerMedicalRecord(MedicalRecordID);
        }

        public static bool DeletePrescription(int PrescriptionID)
        {
            return clsPrescriptionData.DeletePrescription(PrescriptionID);
        }

        public bool Delete()
        {
            return clsPrescriptionData.DeletePrescription(this.PrescriptionID);
        }

        public static bool IsPrescriptionExist(int PrescriptionID)
        {
            return clsPrescriptionData.IsPrescriptionExist(PrescriptionID);
        }
    }
}
