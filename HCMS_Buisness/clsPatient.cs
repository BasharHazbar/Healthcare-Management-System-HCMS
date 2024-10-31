using HCMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HCMS_Buisness
{
    public class clsPatient
    {
        private enum enMode { AddNew = 0, Update = 1 }  
        
        private enMode Mode = enMode.AddNew;

        public int PatientID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsPatient()
        {
            this.PatientID = -1;
            this.PersonID = -1;
            Mode = enMode.AddNew;
        }

        public clsPatient(int PatientID, int PersonID)
        {
            this.PatientID = PatientID;
            this.PersonID = PersonID;
            PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.Update;
        }

        private bool _AddNewPatient()
        {
            this.PatientID = clsPatientData.AddNewPatient(this.PersonID);
            return this.PatientID != -1;
        }

        private bool _UpdatePatient()
        {
            return clsPatientData.UpdatePatient(this.PatientID,this.PersonID);
        }
        public static clsPatient Find(int PatientID)
        {

            int PersonID = -1;

            if (clsPatientData.GetPatientInfoByID(PatientID, ref PersonID))
                return new clsPatient(PatientID,PersonID);
            else
                return null;
        }

        public static clsPatient FindByPersonID(int PersonID)
        {

            int PatientID = -1;

            if (clsPatientData.GetPatientInfoByPersonID(PersonID, ref PatientID))
                return new clsPatient(PatientID, PersonID);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPatient())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePatient();

            }

            return false;
        }

        public static DataTable GetAllPatients()
        {
            return clsPatientData.GetAllPatients();
        }


        public  bool DeletePatient()
        {
            return clsPatientData.DeletePatient(this.PatientID);
        }

        public static bool DeletePatient(int PatientID)
        {
            return clsPatientData.DeletePatient(PatientID);
        }

        public bool Delete()
        {
            return clsPatientData.DeletePatient(PatientID);
        }

        public static bool IsPatientExist(int PatientID)
        {
            return clsPatientData.IsPatientExist(PatientID);
        }

        public static bool IsPatient(int PersonID)
        {
            return clsPatientData.IsPatientExistByPersonId(PersonID);
        }
        public static bool IsDoctor(int PersonID)
        {
            return clsDoctor.IsDoctor(PersonID);
        }
    }
}
