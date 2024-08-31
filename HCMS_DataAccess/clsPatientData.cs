using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_DataAccess
{
    public class clsPatientData
    {
        public static bool GetPatientInfoByID(int PatientID, ref int PersonID)
        {
            bool isFound = false;

            string query = "Select * from Patients where PatientID = @PatientID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", PatientID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;

                                PatientID = (int)reader["PatientID"];
                                PersonID = (int)reader["PersonID"];
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

        public static int AddNewPatient(int PersonID)
        {
            int PatientID = -1;

            string query = @"INSERT INTO Patients
                                   (PersonID)
                             VALUES
                                   (@PersonID);
                     SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertID))
                        {
                            PatientID = insertID;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return PatientID;
        }

        public static bool UpdatePatient(int PatientID, int PersonID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Patients
                           SET PersonID = @PersonID
                         WHERE PatientID = @PatientID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@PatientID", PatientID);
                    

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



        public static DataTable GetAllPatients()
        {

            DataTable dt = new DataTable();

            string query =
              @"SELECT * from Patients;";

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

        public static bool DeletePatient(int PatientID)
        {
            int rowsAffected = 0;
            string query = @"DELETE FROM Patients WHERE PatientID = @PatientID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", PatientID);

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


        public static bool IsPatientExist(int PatientID)
        {
            bool isFound = false;
            string query = "Select 1 from Patients where PatientID = @PatientID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", PatientID);

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
