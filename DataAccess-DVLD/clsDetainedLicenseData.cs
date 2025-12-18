using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;

namespace DataAccess_DVLD
{
    public class clsDetainedLicenseData
    {
        public static clsDetainedLicenseDTO Find(int DetainID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindDetainedLicenseByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DetainID", DetainID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsDetainedLicenseDTO(
                                reader.GetInt32(reader.GetOrdinal("DetainID")),
                                reader.GetInt32(reader.GetOrdinal("LicenseID")),
                                reader.GetDateTime(reader.GetOrdinal("DetainDate")),
                                reader.GetDecimal(reader.GetOrdinal("FineFees")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                reader.GetBoolean(reader.GetOrdinal("IsReleased")),
                                reader.IsDBNull(reader.GetOrdinal("ReleaseDate")) ?
                                DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                                reader.IsDBNull(reader.GetOrdinal("ReleasedByUserID")) ?
                                -1 : reader.GetInt32(reader.GetOrdinal("ReleasedByUserID")),
                                reader.IsDBNull(reader.GetOrdinal("ReleaseApplicationID")) ?
                                -1 : reader.GetInt32(reader.GetOrdinal("ReleaseApplicationID"))

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
        public static clsDetainedLicenseDTO FindByLicenseID(int LicenseID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindDetainedLicenseByLicenseID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsDetainedLicenseDTO(
                                reader.GetInt32(reader.GetOrdinal("DetainID")),
                                reader.GetInt32(reader.GetOrdinal("LicenseID")),
                                reader.GetDateTime(reader.GetOrdinal("DetainDate")),
                                reader.GetDecimal(reader.GetOrdinal("FineFees")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                reader.GetBoolean(reader.GetOrdinal("IsReleased")),
                                reader.IsDBNull(reader.GetOrdinal("ReleaseDate")) ?
                                DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                                reader.IsDBNull(reader.GetOrdinal("ReleasedByUserID")) ?
                                -1 : reader.GetInt32(reader.GetOrdinal("ReleasedByUserID")),
                                reader.IsDBNull(reader.GetOrdinal("ReleaseApplicationID")) ?
                                -1 : reader.GetInt32(reader.GetOrdinal("ReleaseApplicationID"))
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
        public static List<clsDetainedLicenseViewDTO> GetAllDetainedLicenses()
        {
          var detainedLicenses = new List<clsDetainedLicenseViewDTO>();
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetAllDetainedLicenses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            detainedLicenses.Add( new clsDetainedLicenseViewDTO(
                                reader.GetInt32(reader.GetOrdinal("DetainID")),
                                reader.GetInt32(reader.GetOrdinal("LicenseID")),
                                reader.GetDateTime(reader.GetOrdinal("DetainDate")),
                                reader.GetBoolean(reader.GetOrdinal("IsReleased")),
                                reader.GetDecimal(reader.GetOrdinal("FineFees")),
                                reader.IsDBNull(reader.GetOrdinal("ReleaseDate")) ?  DateTime.MinValue :
                                reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                                reader.GetString(reader.GetOrdinal("NationalNo")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.IsDBNull(reader.GetOrdinal("ReleaseApplicationID")) ?-1 :
                                reader.GetInt32(reader.GetOrdinal("ReleaseApplicationID"))
                                ));
                        }
                    }
                }
            }
            return detainedLicenses;
        }
        public static int AddNewDetainedLicense(clsDetainedLicenseDTO detainedLicenseDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddNewDetainedLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", detainedLicenseDTO.LicenseID);
                    command.Parameters.AddWithValue("@DetainDate", detainedLicenseDTO.DetainDate);
                    command.Parameters.AddWithValue("@FineFees", detainedLicenseDTO.FineFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", detainedLicenseDTO.CreatedByUserID);

                    var DetainID = new SqlParameter("@DetainID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(DetainID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(DetainID.Value);
                }
            }
        }
        public static bool UpdateDetainedLicense(clsDetainedLicenseDTO detainedLicenseDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdateDetainedLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DetainID", detainedLicenseDTO.DetainID);
                    command.Parameters.AddWithValue("@LicenseID", detainedLicenseDTO.LicenseID);
                    command.Parameters.AddWithValue("@DetainDate", detainedLicenseDTO.DetainDate);
                    command.Parameters.AddWithValue("@FineFees", detainedLicenseDTO.FineFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", detainedLicenseDTO.CreatedByUserID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_IsLicenseDetained", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    var isDetainedParam = new SqlParameter("@IsDetained", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(isDetainedParam);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToBoolean(isDetainedParam.Value);
                }
            }
        }
        public static bool ReleaseDetainedLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_ReleaseDetainedLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DetainID", DetainID);
                    command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                    command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                    command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}