using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_DVLD
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static clsLocalDrivingLicenseApplicationDTO FindLocalDrivingID(int LocalDrivingLicenseApplicationID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindLocalDrivingID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsLocalDrivingLicenseApplicationDTO(
                                reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("LicenseClassID"))
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

        public static clsLocalDrivingLicenseApplicationDTO FindApplicationID(int applicationID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindApplicationID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", applicationID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsLocalDrivingLicenseApplicationDTO(
                                reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("LicenseClassID"))
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
        public static int AddNewLocalDriving(clsLocalDrivingLicenseApplicationDTO localDTO)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewLocalDriving", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", localDTO.ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", localDTO.LicenseClassID);

                    var OutPut = new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,
                    };
                    command.Parameters.Add(OutPut);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(OutPut.Value);
                }
            }
        }
        public static bool UpdateLocalDriving(clsLocalDrivingLicenseApplicationDTO localDTO)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateLocalDriving", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDTO.LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@ApplicationID", localDTO.ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", localDTO.LicenseClassID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
        public static List<clsLocalDrivingLicenseApplicationViewDTO> GetAllLocalDriving()
        {
            var list = new List<clsLocalDrivingLicenseApplicationViewDTO>();

            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllLocalDriving", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new clsLocalDrivingLicenseApplicationViewDTO(
                                reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                reader.GetString(reader.GetOrdinal("ClassName")),
                                reader.GetString(reader.GetOrdinal("NationalNo")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                                reader.GetInt32(reader.GetOrdinal("PassedTestCount")),
                                reader.GetString(reader.GetOrdinal("Status"))
                            ));
                        }
                    }
                }
            }

            return list;
        }
        public static bool DeleteLocalDriving(int LocalDrivingLicenseApplicationID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteLocalDriving", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return (rowsAffected > 0);

                    }
                }
            }
            catch (SqlException ex)
            {
                return false;
            }

        }
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool returnedResult = false;
            using (var connetion = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_DoesPassTestType", connetion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    connetion.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && bool.TryParse(result.ToString(), out bool value))
                    {
                        returnedResult = value;
                    }
                }
            }
            return returnedResult;

        }
        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool returnedResult = false;
            using (var connetion = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_DoesAttendTestType", connetion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    connetion.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && bool.TryParse(result.ToString(), out bool value))
                    {
                        returnedResult = value;
                    }
                }
            }

            return returnedResult;
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            byte returnedResult = 0;
            using (var connetion = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_TotalTrialsPerTest", connetion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    connetion.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && byte.TryParse(result.ToString(), out byte value))
                    {
                        returnedResult = value;
                    }
                }
            }

            return returnedResult;

        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool returnedResult = false;
            using (var connetion = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_IsThereAnActiveScheduledTest", connetion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    connetion.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && bool.TryParse(result.ToString(), out bool value))
                    {
                        returnedResult = value;
                    }
                }
            }

            return returnedResult;
        }
    }
}

