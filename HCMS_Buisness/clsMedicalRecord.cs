using HCMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_Buisness
{
    public class clsMedicalRecord
    {

        private enum enMode { AddNew = 0, Update = 1 }

        private enMode Mode = enMode.AddNew;

        public int MedicalRecordID { get; set; }

        public int AppointmentID { get; set; }

        public clsAppointment AppointmentInfo { get; }

        public string Diagnosis { get; set; }

        public string VisitDescription { get; set; }


        public clsMedicalRecord() {

            Mode = enMode.AddNew;
            this.MedicalRecordID = -1;
            this.AppointmentID = -1;
            this.Diagnosis = "";
            this.VisitDescription = "";
        }


        public clsMedicalRecord(int MedicalRecordID,int AppointmentID,string Diagnosis, string VisitDescription)
        {
            this.MedicalRecordID= MedicalRecordID;
            this.AppointmentID= AppointmentID;
            this.Diagnosis= Diagnosis;
            this.VisitDescription= VisitDescription;
            this.AppointmentInfo = clsAppointment.Find(AppointmentID);
            this.Mode = enMode.Update;
        }

        private bool _AddNewMedicalRecord()
        {
            this.MedicalRecordID = clsMedicalRecordData.AddNewMedicalRecord(this.AppointmentID,this.Diagnosis,this.VisitDescription);
            return this.MedicalRecordID != -1;
        }

        private bool _UpdateMedicalRecord()
        {
            return clsMedicalRecordData.UpdateMedicalRecord(this.MedicalRecordID,this.AppointmentID,this.Diagnosis,this.VisitDescription);
        }

        public static clsMedicalRecord Find(int MedicalRecordID)
        {

            int AppointmentID = -1;
            string Diagnosis = "";
            string VisitDescription = "";

            if (clsMedicalRecordData.GetMedicalRecordInfoByID(MedicalRecordID,ref AppointmentID,ref Diagnosis,ref VisitDescription))
                return new clsMedicalRecord(MedicalRecordID,AppointmentID, Diagnosis, VisitDescription);
            else
                return null;
        }

        public static clsMedicalRecord FindByAppointmentID(int AppointmentID)
        {

            int MedicalRecordID = -1;
            string Diagnosis = "";
            string VisitDescription = "";

            if (clsMedicalRecordData.GetMedicalRecordInfoByAppointmentID(AppointmentID,ref MedicalRecordID,ref Diagnosis,ref VisitDescription))
            {
                return new clsMedicalRecord(MedicalRecordID,AppointmentID,Diagnosis,VisitDescription);
            }
            else
            {
                return null;
            }

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (this._AddNewMedicalRecord())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return this._UpdateMedicalRecord();

                default:
                    return false;
            }
        }

        public static DataTable GetAllMedicalRecords()
        {
            return clsMedicalRecordData.GetAllMedicalRecords();
        }

        public static bool DeleteMedicalRecord(int MedicalRecordID)
        {
            return clsMedicalRecordData.DeleteMedicalRecord(MedicalRecordID);
        }

        public bool Delete()
        {
            return clsMedicalRecordData.DeleteMedicalRecord(this.MedicalRecordID);
        }

        public static bool IsMedicalRecordExisByAppointmentID(int AppointmentID)
        {
            return clsMedicalRecordData.IsMedicalRecordExisByAppointmentID(AppointmentID);
        }

        public static bool IsMedicalRecordExist(int MedicalRecordID)
        {
            return clsMedicalRecordData.IsMedicalRecordExist(MedicalRecordID);
        }
    }
}
