using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_DataAccess
{
    public class clsMedicalRecordData
    {
        public static bool GetMedicalRecordInfoByID(int MedicalRecordID, ref int AppointmentID, ref string Diagnosis, ref string VisitDescription)
        {
            bool isFound = false;

            string query = "select * from MedicalRecords where MedicalRecordID = @MedicalRecordID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;

                                AppointmentID = (int)reader["AppointmentID"];

                                MedicalRecordID = (int)reader["MedicalRecordID"];

                                Diagnosis = (string)reader["Diagnosis"];

                                VisitDescription = (string)reader["VisitDescription"];
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

        public static bool GetMedicalRecordInfoByAppointmentID(int AppointmentID, ref int MedicalRecordID, ref string Diagnosis, ref string VisitDescription)
        {
            bool isFound = false;

            string query = "select * from MedicalRecords where AppointmentID = @AppointmentID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;

                                AppointmentID = (int)reader["AppointmentID"];

                                MedicalRecordID = (int)reader["MedicalRecordID"];

                                Diagnosis = (string)reader["Diagnosis"];

                                VisitDescription = (string)reader["VisitDescription"];
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

        public static int AddNewMedicalRecord(int AppointmentID, string Diagnosis, string VisitDescription)
        {
            int PatientID = -1;

            string query = @"INSERT INTO MedicalRecords
                                       (AppointmentID
                                       ,Diagnosis
                                       ,VisitDescription)
                                 VALUES (@AppointmentID,@Diagnosis,@VisitDescription)
                                  SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    command.Parameters.AddWithValue("@Diagnosis", Diagnosis);
                    command.Parameters.AddWithValue("@VisitDescription", VisitDescription);

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

        public static bool UpdateMedicalRecord(int MedicalRecordID, int AppointmentID, string Diagnosis, string VisitDescription)
        {
            int rowsAffected = 0;

            string query = @"UPDATE MedicalRecords
                                       SET AppointmentID = 
                                          ,Diagnosis = 
                                          ,VisitDescription = 
                                     WHERE MedicalRecordID = @MedicalRecordID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);
                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    command.Parameters.AddWithValue("@Diagnosis", Diagnosis);
                    command.Parameters.AddWithValue("@VisitDescription", VisitDescription);

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



        public static DataTable GetAllMedicalRecords()
        {

            DataTable dt = new DataTable();

            string query = @"SELECT * from MedicalRecords;";

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

        public static bool DeleteMedicalRecord(int MedicalRecordID)
        {
            int rowsAffected = 0;
            string query = @"DELETE FROM MedicalRecords WHERE MedicalRecordID = @MedicalRecordID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);

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


        public static bool IsMedicalRecordExist(int MedicalRecordID)
        {
            bool isFound = false;
            string query = "select 1 from MedicalRecords where MedicalRecordID = @MedicalRecordID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);

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

        public static bool IsMedicalRecordExisByAppointmentID(int AppointmentID)
        {
            bool isFound = false;

            string query = "select 1 from MedicalRecords where AppointmentID = @AppointmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

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
