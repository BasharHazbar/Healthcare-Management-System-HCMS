﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;

namespace HCMS_DataAccess
{
    public class clsPrescriptionData
    {

        public static bool GetPrescriptionInfoByID(int PrescriptionID, ref int MedicalRecordID, ref string MedicationDetails,
           ref string Dosage,ref DateTime PrescriptionDate,ref string SpecialInstructions)
        {
            bool isFound = false;

            string query = "SELECT * FROM Prescriptions WHERE PrescriptionID = @PrescriptionID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;

                                PrescriptionID = (int)reader["AppointmentID"];
                                MedicalRecordID = (int)reader["MedicalRecordID"];
                                MedicationDetails = (string)reader["MedicationDetails"];
                                Dosage = (string)reader["Dosage"];
                                PrescriptionDate = (DateTime)reader["PrescriptionDate"];
                                SpecialInstructions = (string)reader["SpecialInstructions"];

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

        public static int AddNewPrescription(int MedicalRecordID, string MedicationDetails,
            string Dosage, string SpecialInstructions)
        {
            int PrescriptionID = -1;

            string query = @"INSERT INTO [dbo].[Prescription]
                                           ([MedicalRecordID]
                                           ,[MedicationDetails]
                                           ,[Dosage]
                                           ,[PrescriptionDate]
                                           ,[SpecialInstructions])
                                     VALUES
                                           (@MedicalRecordID,
                                           @MedicationDetails,
                                           @Dosage,
                                           @PrescriptionDate,
                                           @SpecialInstructions);
                                        SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);
                    command.Parameters.AddWithValue("@MedicationDetails", MedicationDetails);
                    command.Parameters.AddWithValue("@Dosage", Dosage);
                    command.Parameters.AddWithValue("@PrescriptionDate", DateTime.Now);
                    command.Parameters.AddWithValue("@SpecialInstructions", SpecialInstructions);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertID))
                        {
                            PrescriptionID = insertID;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                    }
                }
            }

            return PrescriptionID;
        }

        public static bool UpdatePrescription(int PrescriptionID,int MedicalRecordID, string MedicationDetails,
            string Dosage, DateTime PrescriptionDate, string SpecialInstructions)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Prescriptions
                                   SET MedicalRecordID = @MedicalRecordID
                                      ,Treatment = @Treatment
                                      ,Dosage = @Dosage
                                      ,StartDate = @StartDate
                                      ,SpecialInstructions = @SpecialInstructions
                                 WHERE PrescriptionID = @PrescriptionID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);
                    command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);
                    command.Parameters.AddWithValue("@MedicationDetails", MedicationDetails);
                    command.Parameters.AddWithValue("@Dosage", Dosage);
                    command.Parameters.AddWithValue("@StartDate", PrescriptionDate);
                    command.Parameters.AddWithValue("@SpecialInstructions", SpecialInstructions);

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



        public static DataTable GetAllPrescriptionsPerMedicalRecord(int MedicalRecordID)
        {

            DataTable dt = new DataTable();

            string query = @"Select PrescriptionID,MedicationDetails,Dosage,PrescriptionDate,SpecialInstructions from Prescription
                            Where MedicalRecordID = @MedicalRecordID;";

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

        public static bool DeletePrescription(int PrescriptionID)
        {
            int rowsAffected = 0;
            string query = @"DELETE FROM Prescriptions WHERE Prescriptions = @Prescriptions";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

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


        public static bool IsPrescriptionExist(int PrescriptionID)
        {
            bool isFound = false;
            string query = "select 1 from Prescriptions where PrescriptionID = @PrescriptionID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

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
