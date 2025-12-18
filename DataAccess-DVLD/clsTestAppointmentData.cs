using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_DVLD
{
    public class clsTestAppointmentData
    {
        public static clsTestAppointmentDTO FindTestAppointmentByID(int testAppointmentID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindTestAppointmentByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsTestAppointmentDTO(
                                reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                                reader.GetInt32(reader.GetOrdinal("TestTypeID")),
                                reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                reader.GetBoolean(reader.GetOrdinal("IsLocked")),
                                reader.IsDBNull(reader.GetOrdinal("RetakeTestApplicationID")) ? -1 :
                                reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID"))
                                );
                        }
                        else
                        {
                            return null;
                        }
                    }

                }

            }
        }
        public static clsTestAppointmentDTO FindLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindLastTestAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsTestAppointmentDTO(
                                reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                                reader.GetInt32(reader.GetOrdinal("TestTypeID")),
                                reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                reader.GetBoolean(reader.GetOrdinal("IsLocked")),
                                reader.IsDBNull(reader.GetOrdinal("RetakeTestApplicationID")) ? -1 :
                                reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID"))

                                );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

        }
        public static int AddNewTestAppointment(clsTestAppointmentDTO taDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddNewTestAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestTypeID", taDTO.TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", taDTO.LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@AppointmentDate", taDTO.AppointmentDate);
                    command.Parameters.AddWithValue("@PaidFees", taDTO.PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", taDTO.CreatedByUserID);
                    command.Parameters.AddWithValue("@IsLocked", taDTO.IsLocked);
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", taDTO.RetakeTestApplicationID);
                    var outputIdParam = new SqlParameter("@TestAppointmentID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)outputIdParam.Value;
                }
            }
        }
        public static bool UpdateTestAppointment(clsTestAppointmentDTO taDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdateTestAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestAppointmentID", taDTO.TestAppointmentID);
                    command.Parameters.AddWithValue("@TestTypeID", taDTO.TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", taDTO.LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@AppointmentDate", taDTO.AppointmentDate);
                    command.Parameters.AddWithValue("@PaidFees", taDTO.PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", taDTO.CreatedByUserID);
                    command.Parameters.AddWithValue("@IsLocked", taDTO.IsLocked);
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", taDTO.RetakeTestApplicationID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
        public static List<clsTestAppointmentViewDTO> GetAllTestAppointments()
        {
            var appointments = new List<clsTestAppointmentViewDTO>();
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetAllTestAppointments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new clsTestAppointmentViewDTO(
                                reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                                reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                reader.GetString(reader.GetOrdinal("TestTypeTitle")),
                                reader.GetString(reader.GetOrdinal("ClassName")),
                                reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetBoolean(reader.GetOrdinal("IsLocked"))
                                ));
                        }
                    }
                }
            }
            return appointments;
        }
        public static List<clsTestAppointmentsPerTestTypeViewDTO> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            var appointments = new List<clsTestAppointmentsPerTestTypeViewDTO>();
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetApplicationTestAppointmentsPerTestType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new clsTestAppointmentsPerTestTypeViewDTO(
                                reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                                reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                                reader.GetBoolean(reader.GetOrdinal("IsLocked"))
                                ));
                        }
                    }
                }
            }
            return appointments;

        }
        public static int GetTestIDByAppointmentID(int testAppointmentID)
        {
            int AppointmentID = -1;
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetTestIDByAppointmentID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int testID))
                    {
                       AppointmentID= testID;
                    }
                    
                }
            }
            return AppointmentID;
        }
    }
}
