using HCMS_DataAccess;
using System.Data;

namespace HCMS_Buisness
{
    public class clsDoctor
    {

        private enum enMode { AddNew = 0, Update = 1 };

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

        public static clsDoctor FindByPersonID(int PersonID)
        {
            int DoctorID = -1;
            string Specialization = "";
            string ClinicAddress = "";

            if (clsDoctorData.GetDoctorInfoByPersonID(PersonID, ref DoctorID, ref Specialization, ref ClinicAddress))
                return new clsDoctor(DoctorID, PersonID, Specialization, ClinicAddress);
            else
                return null;
        }

        public static clsDoctor FindByName(string FullName)
        {
            int DoctorID = -1;
            int PersonID = -1;  
            string Specialization = "";
            string ClinicAddress = "";

            if (clsDoctorData.GetDoctorInfoByFullName(FullName,ref PersonID,ref DoctorID,ref Specialization,ref ClinicAddress))
                return new clsDoctor(DoctorID, PersonID, Specialization, ClinicAddress);
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

        public static bool IsDoctor(int PersonID)
        {
            return clsDoctorData.IsDoctorExistByPersonID(PersonID);
        }

        public static bool IsPatient(int PersonID)
        {
            return clsPatient.IsPatient(PersonID);
        }
    }
}
