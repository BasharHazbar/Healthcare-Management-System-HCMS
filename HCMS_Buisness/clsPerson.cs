using HCMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_Buisness
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;

        public int PersonID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName
        {
            get {
                /* if (this.ThirdName != "")
                    return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;

                 else 
                     return FirstName + " " + SecondName + " " + LastName;*/
                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }

        }
        public DateTime DateOfBirth { set; get; }
        public byte Gender { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }

        public string ImagePath { set; get; }

   

        public clsPerson()

        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.PhoneNumber = "";
            this.Email = "";
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, byte Gender,
            string Address, string Phone, string Email, string ImagePath)

        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.PhoneNumber = Phone;
            this.Email = Email;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
          
             this.PersonID = clsPersonData.AddNewPerson(
                this.FirstName, this.SecondName, this.ThirdName,
                this.LastName,this.DateOfBirth, this.Gender, this.Address, this.PhoneNumber, this.Email,
                this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            
            return clsPersonData.UpdatePerson(
                this.PersonID,this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address, this.PhoneNumber, this.Email,
                this.ImagePath);
        }

        public static clsPerson Find(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", 
            Email = "", PhoneNumber = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;

            bool IsFound = clsPersonData.GetPersonInfoByID
                                (
                                    PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref DateOfBirth,
                                    ref Gender, ref Address, ref PhoneNumber, ref Email, ref ImagePath
                                );

            if (IsFound)
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName
                    ,DateOfBirth, Gender, Address, PhoneNumber, Email, ImagePath);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public bool DeletePerson()
        {
            return clsPersonData.DeletePerson(this.PersonID);
        }

        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }

        

    }
}
