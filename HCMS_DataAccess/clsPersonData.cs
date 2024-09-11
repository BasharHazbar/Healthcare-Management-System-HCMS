using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace HCMS_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string SecondName,
                  ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
                   ref byte Gender, ref string Address, ref string PhoneNumber, ref string Email, ref string ImagePath)
        {
            bool isFound = false;

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                      
                            if (reader.Read())
                            {
                                isFound = true;

                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];

                                ThirdName = reader["ThirdName"] as string ?? "";

                                LastName = (string)reader["LastName"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Gender = (byte)reader["Gender"];
                                Address = (string)reader["Address"];
                                PhoneNumber = (string)reader["PhoneNumber"];

                                Email = reader["Email"] as string ?? "";

                                ImagePath = reader["ImagePath"] as string ?? "";
                            }
                            else
                            {
                                isFound = false;
                            }
                        }    

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);

                        isFound = false;
                    }
                }
            }


            return isFound;
        }

        public static int AddNewPerson(string FirstName, string SecondName,
           string ThirdName, string LastName, DateTime DateOfBirth,
           byte Gender, string Address, string PhoneNumber, string Email, string ImagePath)
        {
            int PersonID = -1;

            string query = @"INSERT INTO People (FirstName, SecondName, ThirdName, LastName,
                                         DateOfBirth, Gender, Address, PhoneNumber, Email, ImagePath)
                     VALUES (@FirstName, @SecondName, @ThirdName, @LastName,
                             @DateOfBirth, @Gender, @Address, @PhoneNumber, @Email, @ImagePath);
                     SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);

                    command.Parameters.AddWithValue("@ThirdName", 
                        clsDataAccessSettings.CheckIsNullOrEmpty(ThirdName) ? (object)DBNull.Value : ThirdName);

                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

                    command.Parameters.AddWithValue("@Email",
                        clsDataAccessSettings.CheckIsNullOrEmpty(Email) ? (object)DBNull.Value : Email);

                    command.Parameters.AddWithValue("@ImagePath",
                        clsDataAccessSettings.CheckIsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertID))
                        {
                            PersonID = insertID;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName,
          string ThirdName, string LastName, DateTime DateOfBirth,
          short Gender, string Address, string PhoneNumber, string Email, string ImagePath)
        {
            int rowsAffected = 0;

            string query = @"UPDATE People  
                    SET FirstName = @FirstName,
                        SecondName = @SecondName,
                        ThirdName = @ThirdName,
                        LastName = @LastName, 
                        DateOfBirth = @DateOfBirth,
                        Gender = @Gender,
                        Address = @Address,  
                        PhoneNumber = @PhoneNumber,
                        Email = @Email, 
                        ImagePath = @ImagePath
                    WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);


                    command.Parameters.AddWithValue("@ThirdName",
                        clsDataAccessSettings.CheckIsNullOrEmpty(ThirdName) ? (object)DBNull.Value : ThirdName);

                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

                    command.Parameters.AddWithValue("@Email",
                        clsDataAccessSettings.CheckIsNullOrEmpty(Email) ? (object)DBNull.Value : Email);
                    command.Parameters.AddWithValue("@ImagePath",
                        clsDataAccessSettings.CheckIsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }

            return rowsAffected > 0;
        }



        public static DataTable GetAllPeople()
        {

            DataTable dt = new DataTable();
       
            string query =
              @"SELECT People.PersonID, 
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  convert(varchar,  People.DateOfBirth, 0) as DateOfBirth, People.Gender,  
				  CASE
                  WHEN People.Gender = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GendorCaption ,
			  People.Address, People.PhoneNumber, People.Email, People.ImagePath
              FROM People
                ORDER BY People.FirstName;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)

                            {
                                dt.Load(reader);
                            }

                            reader.Close();
                        }
                    }

                    catch (Exception ex)
                    {
                        // Console.WriteLine("Error: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return dt;

        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            string query = @"DELETE FROM People WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Log the error message
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return rowsAffected > 0;
        }


        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            string query = "SELECT 1 FROM People WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error message
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return isFound;
        }
    }
}
