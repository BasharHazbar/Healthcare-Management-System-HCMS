using HCMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_Buisness
{
    public class clsUser
    {

        public enum enMode { AddNew = 0, Update = 1 }

        private enMode Mode = enMode.AddNew;

        public enum enRole { Admin = 0, Doctor = 1, Patient = 2}

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }   
        public string UserName { get; set; }
        public string Password { get; set; }
        public enRole Role { get; set; }
        public bool IsActive {  get; set; } 
        public DateTime CreatedDate { get; set; }


        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.Role = enRole.Admin;
            this.IsActive = true;

            this.Mode = enMode.AddNew;
        }

        public clsUser(int UserID, int PersonID, string UserName,string Password,enRole Role, bool IsActive, DateTime CreatedDate)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.Role = Role;
            this.IsActive = IsActive;
            this.CreatedDate = CreatedDate;
            this.Mode = enMode.Update;
        }


        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID,this.UserName,this.Password,(byte)this.Role);
            return this.UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID,this.PersonID,this.UserName,this.Password,(byte)this.Role, this.IsActive);
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            byte Role = 0;
            DateTime CreatedDate = DateTime.Now;
            bool IsActive = true;


            if (clsUserData.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref Role, ref IsActive, ref CreatedDate))
                return new clsUser(UserID,PersonID,UserName,Password,(enRole)Role,IsActive,CreatedDate);

            else
            return null;
        }

        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1;    
            int PersonID = -1;
            byte Role = 0;
            DateTime CreatedDate = DateTime.Now;
            bool IsActive = true;


            if (clsUserData.GetUserInfoByUserNameAndPassword(UserName,Password,ref UserID, ref PersonID, ref Role, ref IsActive, ref CreatedDate))
                return new clsUser(UserID, PersonID, UserName, Password, (enRole)Role, IsActive, CreatedDate);

            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);  
        }

        public bool DeleteUser()
        {
            return clsUserData.DeleteUser(this.UserID);
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }


        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUserData.IsUserExistByPersonID(PersonID);
        }
    }
}
