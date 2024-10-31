using HCMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_Buisness
{
    public class clsPayment
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode Mode = enMode.AddNew;
        public enum enPaymentStatus { Paid = 0, Pending = 1, Overdue = 2 }
        public int PaymentID { get; set; } 
        public int AppointmentID { get; set; }

        public int PatientID { get; set;}

        public float TotalAmount {  get; set; } 

        public enPaymentStatus PaymentStatus { get; set; }

        public DateTime PaymentDate {  get; set; }

        public clsPayment() {

            this.PaymentID = -1;
            this.AppointmentID = -1;
            this.PatientID = -1;
            this.TotalAmount = -1;
            this.PaymentStatus = enPaymentStatus.Pending;
            this.PaymentDate = DateTime.Now;

            this.Mode = enMode.AddNew;
            
        }

        public clsPayment(int PaymentID, int AppointmentID, int PatientID, float TotalAmount, 
            enPaymentStatus PaymentStatus, DateTime PaymentDate)
        {
            this.PaymentID = PaymentID;
            this.AppointmentID = AppointmentID;
            this.PatientID = PatientID;
            this.TotalAmount = TotalAmount;
            this.PaymentStatus = PaymentStatus;
            this.PaymentDate = PaymentDate;

            this.Mode = enMode.Update;
        }

       public clsPayment Find(int PaymentID)
       {
            int AppointmentID = -1;
            int PatientID = -1;
            float TotalAmount = -1;
            byte PaymentStatus = 3;
            DateTime PaymentDate = DateTime.Now;

            if (clsPaymentData.GetPaymentInfoByID(PaymentID,ref AppointmentID,ref PatientID,ref TotalAmount,
                ref PaymentStatus,ref PaymentDate))
            {
                return new clsPayment(PaymentID,AppointmentID,PatientID,TotalAmount, (enPaymentStatus)PaymentStatus,PaymentDate);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewPayment()
        {
            this.PaymentID = clsPaymentData.AddNewPayment(this.AppointmentID,this.PatientID,this.TotalAmount,(byte)this.PaymentStatus,this.PaymentDate);

            return PaymentID != -1;
        }

        private bool _UpdatePayment()
        {
            return clsPaymentData.UpdatePayment(this.PaymentID,this.AppointmentID,this.PatientID,this.TotalAmount,(byte)this.PaymentStatus,this.PaymentDate);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (this._AddNewPayment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return this._UpdatePayment();

                default:
                    return false;
            }
        }

        public bool Delete()
        {
            return clsPaymentData.DeletePayment(PaymentID);
        }

        public static bool IsPaymentExist(int PaymentID)
        {
            return clsPaymentData.IsPaymentExist(PaymentID);
        }

    }
}
