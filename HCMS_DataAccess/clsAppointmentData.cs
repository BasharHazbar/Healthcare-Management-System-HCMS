using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace HCMS_DataAccess
{
    public class clsAppointmentData
    {
		public static bool GetAppointmentInfoByID(int AppointmentID, ref int PatientID, ref int DoctorID, ref DateTime AppointmentDate,
			ref TimeSpan EndTime, ref byte Status,ref string Notes, ref int CreatedBy)
		{
			bool isFound = false;

			string query = "Select * from Appointments where AppointmentID = @AppointmentID;";

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
								PatientID = (int)reader["PatientID"];
								DoctorID = (int)reader["DoctorID"];
								AppointmentDate = (DateTime)reader["AppointmentDate"];
								
								if (reader["EndTime"] != DBNull.Value)
								{
                                    EndTime = (TimeSpan)reader["EndTime"];
                                }
							
								Status = (byte)reader["Status"];



                                if (reader["Notes"] != DBNull.Value)
                                {
                                    Notes = (string)reader["Notes"];
                                }

                                CreatedBy = (int)reader["CreatedBy"];
                            }
							else
							{
								isFound = false;
							}
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: " + ex);

						isFound = false;
					}
				}
			}
			return isFound;
		}

		public static int AddNewAppointment(int PatientID, int DoctorID, byte Status, string Notes, int CreatedBy)
		{
			int AppointmentID = -1;

			string query = @" INSERT INTO Appointments
											   (PatientID
											   ,DoctorID
											   ,AppointmentDate
											   ,EndTime
											   ,Status
											   ,Notes
											   ,CreatedBy)
										 VALUES
											   (@PatientID,@DoctorID,@AppointmentDate,
											   @EndTime,@Status,@Notes,@CreatedBy)
                                   SELECT SCOPE_IDENTITY();";

			using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@PatientID", PatientID);
					command.Parameters.AddWithValue("@DoctorID", DoctorID);
					command.Parameters.AddWithValue("@AppointmentDate", DateTime.Now);
					command.Parameters.AddWithValue("@EndTime", DBNull.Value);
					command.Parameters.AddWithValue("@Status", Status);
					command.Parameters.AddWithValue("@Notes", 
						clsDataAccessSettings.CheckIsNullOrEmpty(Notes) ? (object)DBNull.Value : Notes);
                    command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                    try
					{
						connection.Open();

						object result = command.ExecuteScalar();

						if (result != null && int.TryParse(result.ToString(), out int insertID))
						{
							AppointmentID = insertID;
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: " + ex);
					}
				}
			}

			return AppointmentID;
		}

		public static bool UpdateAppointment(int AppointmentID, int PatientID, int DoctorID, DateTime AppointmentDate,
			 TimeSpan EndTime, byte Status, string Notes, int CreatedBy)
		{
			int rowsAffected = 0;

			string query = @"UPDATE Appointments
								   SET PatientID = @PatientID
									  ,DoctorID = @DoctorID
									  ,AppointmentDate = @AppointmentDate
									  ,EndTime = @EndTime
									  ,Status = @Status
									  ,Notes = @Notes
									  ,CreatedBy = @CreatedBy
								 WHERE AppointmentID = @AppointmentID";

			using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
					command.Parameters.AddWithValue("@DoctorID", DoctorID);
					command.Parameters.AddWithValue("@PatientID", PatientID);
					command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
					command.Parameters.AddWithValue("@EndTime", EndTime);
					command.Parameters.AddWithValue("@Status", Status);
                    command.Parameters.AddWithValue("@Notes",clsDataAccessSettings.CheckIsNullOrEmpty(Notes) ? (object)DBNull.Value : Notes);
                    command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

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



		public static DataTable GetAllAppointments()
		{

			DataTable dt = new DataTable();

			string query = "SELECT * from Appointments";

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

		public static bool DeleteAppointment(int AppointmentID)
		{
			int rowsAffected = 0;

			string query = @"DELETE FROM Appointments WHERE AppointmentID = @AppointmentID";

			using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

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


		public static bool IsAppointmentExist(int AppointmentID)
		{
			bool isFound = false;

			string query = "Select 1 from Appointments where AppointmentID = @AppointmentID";

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
