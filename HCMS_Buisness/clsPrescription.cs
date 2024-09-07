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
        public enum enMode { AddNew = 0, Update = 1 }

        private enMode Mode = enMode.AddNew;

        public int PrescriptionID {  get; set; }
        public int MedicalRecordID { get; set; }
        clsMedicalRecord MedicalRecordInfo { get; }
        public string Treatment { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set;}
        public string SpecialInstructions { get; set;}

        public clsPrescription()
        {
            this.PrescriptionID = -1;
            this.MedicalRecordID = -1;
            this.Treatment = "";
            this.Dosage = "";
            this.Frequency = "";
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.SpecialInstructions = "";
            this.Mode = enMode.AddNew;
        }

        public clsPrescription(int PrescriptionID, int MedicalRecordID, string Treatment, 
            string Dosage, string Frequency, DateTime StartDate, DateTime EndDate, string SpecialInstructions)
        {
            this.PrescriptionID = PrescriptionID;
            this.MedicalRecordID = MedicalRecordID;
            this.MedicalRecordInfo = clsMedicalRecord.Find(MedicalRecordID);
            this.Treatment = Treatment;
            this.Dosage = Dosage;
            this.Frequency = Frequency;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.SpecialInstructions = SpecialInstructions;
            this.Mode = enMode.Update;
        }

        private bool _AddNewPrescription()
        {
            this.PrescriptionID = clsPrescriptionData.AddNewPrescription(this.MedicalRecordID,this.Treatment,this.Dosage,
                this.Frequency,this.StartDate,this.EndDate,this.SpecialInstructions);
            return this.PrescriptionID != -1;
        }

        private bool _UpdatePrescription()
        {
            return clsPrescriptionData.UpdatePrescription(this.PrescriptionID,this.MedicalRecordID,this.Treatment,
                this.Dosage,this.Frequency,this.StartDate,this.EndDate,this.SpecialInstructions);
        }
        public clsPrescription Find(int PrescriptionID)
        {
            int MedicalRecordID = -1;
            string Treatment = "";
            string Dosage = "";
            string Frequency = "";
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            string SpecialInstructions = "";

            bool IsFound = clsPrescriptionData.GetPrescriptionInfoByID(PrescriptionID,
                ref MedicalRecordID,ref Treatment,ref Dosage,ref Frequency,ref StartDate,
                ref EndDate,ref SpecialInstructions);

            if (IsFound)
            {
                return new clsPrescription(PrescriptionID,MedicalRecordID,Treatment,Dosage,Frequency,
                    StartDate,EndDate,SpecialInstructions);
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

        public static DataTable GetAllPrescriptions()
        {
            return clsPrescriptionData.GetAllPrescriptions();
        }

        public static bool DeletePrescription(int PrescriptionID)
        {
            return clsPrescriptionData.DeletePrescription(PrescriptionID);
        }

        public bool DeletePrescription()
        {
            return clsPrescriptionData.DeletePrescription(this.PrescriptionID);
        }

        public static bool IsPrescriptionExist(int PrescriptionID)
        {
            return clsPrescriptionData.IsPrescriptionExist(PrescriptionID);
        }
    }
}
