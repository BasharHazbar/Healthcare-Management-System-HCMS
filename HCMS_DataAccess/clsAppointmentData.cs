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
			ref TimeSpan EndTime, ref byte Status,ref string Notes, ref int CreatedBy, ref DateTime CreatedDate)
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


                                CreatedDate = (DateTime)reader["CreatedDate"];

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

		public static int AddNewAppointment(int PatientID, int DoctorID, DateTime AppointmentDate, byte Status, string Notes, int CreatedBy)
		{
			int AppointmentID = -1;

			string query = @"INSERT INTO Appointments
											   (PatientID
											   ,DoctorID
											   ,AppointmentDate
											   ,EndTime
											   ,Status
											   ,Notes
											   ,CreatedBy
											   ,CreatedDate)
										 VALUES
											   (@PatientID,@DoctorID,@AppointmentDate,
											   @EndTime,@Status,@Notes,@CreatedBy, @CreatedDate)
                                   SELECT SCOPE_IDENTITY();";

			using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@PatientID", PatientID);
					command.Parameters.AddWithValue("@DoctorID", DoctorID);
					command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
					command.Parameters.AddWithValue("@EndTime", DBNull.Value);
					command.Parameters.AddWithValue("@Status", Status);
					command.Parameters.AddWithValue("@Notes", 
						clsDataAccessSettings.CheckIsNullOrEmpty(Notes) ? (object)DBNull.Value : Notes);
                    command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

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
			 TimeSpan EndTime, byte Status, string Notes, int CreatedBy, DateTime CreatedDate)
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
									  ,CreatedDate = @CreatedDate
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
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

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


        public static bool UpdateStatus(int AppointmentID, byte Status)
        {
            int rowsAffected = 0;

            string query = @"update Appointments set Status = @Status where AppointmentID = @AppointmentID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    command.Parameters.AddWithValue("@Status", Status);
                    

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


        public static DataTable GetAppointmentsPerPatientID(int PersonID)
		{

			DataTable dt = new DataTable();

			string query = @"Select ap.AppointmentID, d.DoctorID, FullName = p.FirstName + ' ' + p.SecondName + ' ' + 
                                ISNULL( p.ThirdName,'') +' ' + p.LastName,d.Specialization,d.ClinicAddress,
								 convert(varchar, ap.AppointmentDate, 0) as AppointmentDate,ap.EndTime,
								 convert(varchar, ap.CreatedDate, 0) as CreatedDate,
								 case when ap.Status = 0 then 'Scheduled' when ap.Status = 1 then 'Completed' 
								when ap.Status = 2 then 'Canceled' else 'Unknown' end as AppointmentStatus
								 from Appointments ap
								join Doctors d on d.DoctorID = ap.DoctorID
								join Patients pt on pt.PatientID = ap.PatientID
								join People p on d.PersonID = p.PersonID
								where pt.PersonID = @PersonID;";

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


        public static DataTable GetAppointmentsPerDoctorID(int PersonID)
        {

            DataTable dt = new DataTable();

            string query = @"Select ap.AppointmentID, pt.PatientID, FullName = p.FirstName + ' ' + p.SecondName + ' ' + 
                                ISNULL( p.ThirdName,'') +' ' + p.LastName,
								 convert(varchar, ap.AppointmentDate, 0) as AppointmentDate,ap.EndTime,
								 ap.Notes,
								 convert(varchar, ap.CreatedDate, 0) as CreatedDate,
								 case when ap.Status = 0 then 'Scheduled' when ap.Status = 1 then 'Completed' 
								when ap.Status = 2 then 'Canceled' else 'Unknown' end as AppointmentStatus
								 from Appointments ap
								join Patients pt on pt.PatientID = ap.PatientID
								join Doctors d on d.DoctorID = ap.DoctorID
								join People p on pt.PersonID = p.PersonID
								where d.PersonID = @PersonID;";

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

        public static bool IsTherActiveInspectionAppointment(int PersonID)
        {
            bool isFound = false;

            string query = @"Select top 1 found = 1 from Appointments where
										PatientID = 4 and Status = 0 order by AppointmentID desc;";

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
