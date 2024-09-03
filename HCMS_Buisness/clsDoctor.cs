using HCMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_Buisness
{
    public class clsDoctor
    {

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode Mode = enMode.AddNew;
        public int DoctorID { set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo { set; get; }
        public string Specialization { set; get; }
        public string ClinicAddress { set; get; }
        public clsDoctor() {

            this.DoctorID = -1;
            this.PersonID = -1;
            this.Specialization = "";
            this.ClinicAddress = "";
            Mode = enMode.AddNew;
        }

        public clsDoctor(int DoctorID, int PersonID, string Specialization, string ClinicAddress)
        {
            this.DoctorID = DoctorID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.Specialization = Specialization;
            this.ClinicAddress = ClinicAddress;
            Mode = enMode.Update;
        }

        private bool _AddNewDoctor()
        {
            this.DoctorID = clsDoctorData.AddNewDoctor(this.PersonID, this.Specialization, this.ClinicAddress);
            return this.DoctorID != -1;
        }

        private bool _UpdateDoctor()
        {
            return clsDoctorData.UpdateDoctor(this.DoctorID, this.PersonID, this.Specialization, this.ClinicAddress);
        }

        public static clsDoctor Find(int DoctorID)
        {
            int PersonID = -1;
            string Specialization = "";
            string ClinicAddress = "";

            if (clsDoctorData.GetDoctorInfoByID(DoctorID, ref PersonID, ref Specialization, ref ClinicAddress))
                return new clsDoctor(DoctorID,PersonID,Specialization,ClinicAddress);
            else
                return null;
        }

      public bool Save()
      {
            switch (Mode)
            {
                case enMode.AddNew:

                  if (this._AddNewDoctor())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                  else
                    {
                        return false;
                    }

                case enMode.Update:
                    return this._UpdateDoctor();

                default:
                    return false;
            }
      }
        public static DataTable GetAllDoctors()
        {
            return clsDoctorData.GetAllDoctors();
        }

        public  bool DeleteDoctor()
        {
            return clsDoctorData.DeleteDoctor(this.DoctorID);
        }

        public static bool DeleteDoctor(int DoctorID)
        {
            return clsDoctorData.DeleteDoctor(DoctorID);
        }

        public static bool IsDoctorExist(int DoctorID)
        {
            return clsDoctorData.IsDoctorExist(DoctorID);
        }
    }
}
