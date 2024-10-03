using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_DataAccess
{
    public class clsPaymentData
    {
        public static bool GetPaymentInfoByID(int PaymentID, ref int AppointmentID,ref int PatientID, 
            ref float TotalAmount, ref byte PaymentStatus, ref DateTime PaymentDate)
        {
            bool isFound = false;

            string query = "Select * from Payments Where PaymentID = @PaymentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PaymentID", PaymentID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;

                                AppointmentID = (int)reader["AppointmentID"];
                                PatientID = (int)reader["PatientID"];
                                TotalAmount = Convert.ToSingle(reader["TotalAmount"]);
                                PaymentStatus = (byte)reader["PaymentStatus"];
                                PaymentDate = (DateTime)reader["PaymentDate"];
           
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

        public static int AddNewPayment(int AppointmentID, int PatientID, float TotalAmount,
            byte PaymentStatus, DateTime PaymentDate)
        {
            int PaymentID = -1;

            string query = @"INSERT INTO [dbo].[Payments]
                                   (AppointmentID
                                   ,PatientID
                                   ,TotalAmount
                                   ,PaymentStatus
                                   ,PaymentDate)
                             VALUES
                                   (@AppointmentID,@PatientID,@TotalAmount,
		                           @PaymentStatus,@PaymentDate);
                                   SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    command.Parameters.AddWithValue("@TotalAmount", PatientID);
                    command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                    command.Parameters.AddWithValue("@TotalAmount", Convert.ToSingle(TotalAmount));
                    command.Parameters.AddWithValue("@PaymentDate", PaymentDate);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertID))
                        {
                            PaymentID = insertID;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                    }
                }
            }

            return PaymentID;
        }


        public static bool UpdatePayment(int PaymentID,int AppointmentID, int PatientID, float TotalAmount,
            byte PaymentStatus, DateTime PaymentDate)
        {
            int rowsAffected = 0;

            string query = @"UPDATE [dbo].[Payments]
                               SET AppointmentID = @AppointmentID
                                  ,PatientID = @PatientID
                                  ,TotalAmount = @TotalAmount
                                  ,PaymentStatus = @PaymentStatus
                                  ,PaymentDate = @PaymentDate
                             WHERE PaymentID = @PaymentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    {
                        command.Parameters.AddWithValue("@PaymentID", PaymentID);
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        command.Parameters.AddWithValue("@TotalAmount", PatientID);
                        command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                        command.Parameters.AddWithValue("@TotalAmount", Convert.ToSingle(TotalAmount));
                        command.Parameters.AddWithValue("@PaymentDate", PaymentDate);


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

        }


        public static bool DeletePayment(int PaymentID)
        {
            int rowsAffected = 0;
            string query = "Delete from Payments where PaymentID = @PaymentID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PaymentID", PaymentID);

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


        public static bool IsPaymentExist(int PaymentID)
        {
            bool isFound = false;
            string query = "Select 1 from Payments where PaymentID = @PaymentID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PaymentID", PaymentID);

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
