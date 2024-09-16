using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_DataAccess
{
    public class clsDoctorData
    {
        public static bool GetDoctorInfoByID(int DoctorID, ref int PersonID, ref string Specialization, ref string ClinicAddress)
        {
            bool isFound = false;

            string query = "Select * from Doctors where DoctorID = @DoctorID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", DoctorID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;

                                DoctorID = (int)reader["DoctorID"];
                                PersonID = (int)reader["PersonID"];
                                Specialization = (string)reader["Specialization"];
                                ClinicAddress = (string)reader["ClinicAddress"];
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

        public static int AddNewDoctor(int PersonID, string Specialization, string ClinicAddress)
        {
            int DoctorID = -1;

            string query = @"INSERT INTO [dbo].[Doctors]
                                   (PersonID
                                   ,Specialization
                                   ,ClinicAddress)
                                   VALUES
                                   (@PersonID,@Specialization,@ClinicAddress)
                                   SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@Specialization", Specialization);
                    command.Parameters.AddWithValue("@ClinicAddress", ClinicAddress);


                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertID))
                        {
                            DoctorID = insertID;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return DoctorID;
        }

        public static bool UpdateDoctor(int DoctorID, int PersonID, string Specialization, string ClinicAddress)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Doctors
                                   SET PersonID = @PersonID
                                      ,Specialization = @Specialization
                                      ,ClinicAddress = @ClinicAddress
                                 WHERE DoctorID = @DoctorID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", DoctorID);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@Specialization", Specialization);
                    command.Parameters.AddWithValue("@ClinicAddress", ClinicAddress);
                    


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



        public static DataTable GetAllDoctors()
        {

            DataTable dt = new DataTable();

            string query = @"Select d.DoctorID,d.PersonID,
                            FullName = p.FirstName + ' ' + p.SecondName + ' ' +  
                            ISNULL( p.ThirdName,'') + ' ' + p.LastName, d.Specialization,d.ClinicAddress from Doctors d
                            join People p on p.PersonID = d.PersonID order by d.DoctorID desc;";

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
                         Console.WriteLine("Error: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return dt;

        }

        public static bool DeleteDoctor(int DoctorID)
        {
            int rowsAffected = 0;

            string query = @"DELETE FROM Doctors WHERE DoctorID = @DoctorID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", DoctorID);

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


        public static bool IsDoctorExist(int DoctorID)
        {
            bool isFound = false;

            string query = "Select 1 from Doctors where DoctorID = @DoctorID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", DoctorID);

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

        public static bool IsDoctorExistByPersonID(int PersonID)
        {
            bool isFound = false;

            string query = "Select 1 from Doctors where PersonID = @PersonID";

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
