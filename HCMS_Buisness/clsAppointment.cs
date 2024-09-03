using HCMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_Buisness
{
    public class clsAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }

        public enMode Mode = enMode.AddNew;
        public enum enStatus { Scheduled = 0, Completed = 1, Canceled = 2 }
        public int AppointmentID { get; set; }

        public int PatientID { get; set; }

        public clsPatient PatientInfo { get; set; }

        public int DoctorID { get; set; }

        public clsDoctor DoctorInfo {  get; set; }
        public DateTime AppointmentDate { get; set; }

        public TimeSpan EndTime { get; set; }

        public enStatus Status { get; set; }
        public string Notes { get; set; }

        public int CreatedBy { get; set; }

        public string StatusText { get {
                switch (this.Status)
                {
                    case enStatus.Scheduled:
                        return "Scheduled";
                    case enStatus.Completed:
                        return "Completed";
                    case enStatus.Canceled:
                        return "Canceled";
                    default:
                        return "Unknown";   
                }
            } 
        }

        public clsAppointment()
        {
            this.AppointmentID = -1;
            this.PatientID = -1;
            this.DoctorID = -1;
            this.AppointmentDate = DateTime.Now;
            this.EndTime = TimeSpan.Zero;
            this.Status = enStatus.Scheduled;
            this.Notes = "";
            this.CreatedBy = -1;
            this.Mode = enMode.AddNew;
        }

        public clsAppointment(int AppointmentID, int PatientID, int DoctorID, DateTime AppointmentDate, 
            TimeSpan EndTime, enStatus Status, string Notes, int CreatedBy)
        {
            this.AppointmentID = AppointmentID;
            this.PatientID=PatientID;
            this.PatientInfo = clsPatient.Find(PatientID);
            this.DoctorID = DoctorID;
            this.DoctorInfo = clsDoctor.Find(DoctorID);
            this.AppointmentDate = AppointmentDate;
            this.EndTime = EndTime;
            this.Status = Status;
            this.Notes = Notes;
            this.CreatedBy = CreatedBy;
            this.Mode = enMode.Update;
        }

        private bool _AddNewAppointment()
        {
            this.AppointmentID = clsAppointmentData.AddNewAppointment(this.PatientID,this.DoctorID,(byte)this.Status,this.Notes,this.CreatedBy);
            return this.AppointmentID != -1;
        }

        private bool _UpdateAppointment()
        {
            return clsAppointmentData.UpdateAppointment(this.AppointmentID,this.PatientID,this.DoctorID,
                this.AppointmentDate,this.EndTime,(byte)this.Status,this.Notes,this.CreatedBy);
        }
        public static clsAppointment Find(int AppointmentID)
        {

            int PatientID = -1;
            int DoctorID = -1;
            DateTime AppointmentDate = DateTime.Now;
            TimeSpan EndTime = TimeSpan.Zero;
            byte Status = 3;
            string Notes = "";
            int CreatedBy = -1;

            if (clsAppointmentData.GetAppointmentInfoByID(AppointmentID,ref PatientID,ref DoctorID,ref AppointmentDate,
                ref EndTime,ref Status,ref Notes,ref CreatedBy))

                return new clsAppointment(AppointmentID,PatientID,DoctorID,AppointmentDate,EndTime,(enStatus)Status,Notes,CreatedBy);

            else
            return null;
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (this._AddNewAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return this._UpdateAppointment();

                default:
                    return false;
            }
        }

        public static DataTable GetAllAppointments()
        {
            return clsAppointmentData.GetAllAppointments();
        }

        public static bool DeleteAppointment(int AppointmentID)
        {
            return clsAppointmentData.DeleteAppointment(AppointmentID);
        }

        public  bool DeleteAppointment()
        {
            return clsAppointmentData.DeleteAppointment(this.AppointmentID);
        }

        public static bool IsAppointmentExist(int AppointmentID)
        {
            return clsAppointmentData.IsAppointmentExist(AppointmentID);
        }
    }
}
