using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;
namespace DataAccess_DVLD
{
    public class clsInternationalLicenseData
    {
        public static clsInternationalLicenseDTO FindByInternationalLicenseID(int internationalLicenseID)
        {
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindByInternationalLicenseID", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseID);
                    conection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsInternationalLicenseDTO(
                                reader.GetInt32(reader.GetOrdinal("InternationalLicenseID")),
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("DriverID")),
                                reader.GetInt32(reader.GetOrdinal("IssuedUsingLocalLicenseID")),
                                reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                                reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                                reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
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
        public static int AddInternationalLicense(clsInternationalLicenseDTO ilDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddInternationalLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", ilDTO.ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", ilDTO.DriverID);
                    command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", ilDTO.IssuedUsingLocalLicenseID);
                    command.Parameters.AddWithValue("@IssueDate", ilDTO.IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", ilDTO.ExpirationDate);
                    command.Parameters.AddWithValue("@IsActive", ilDTO.IsActive);
                    command.Parameters.AddWithValue("@CreatedByUserID", ilDTO.CreatedByUserID);
                    SqlParameter outputIdParam = new SqlParameter("@InternationalLicenseID", SqlDbType.Int)
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
        public static bool UpdateInternationalLicense(clsInternationalLicenseDTO ilDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdateInternationalLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InternationalLicenseID", ilDTO.InternationalLicenseID);
                    command.Parameters.AddWithValue("@ApplicationID", ilDTO.ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", ilDTO.DriverID);
                    command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", ilDTO.IssuedUsingLocalLicenseID);
                    command.Parameters.AddWithValue("@IssueDate", ilDTO.IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", ilDTO.ExpirationDate);
                    command.Parameters.AddWithValue("@IsActive", ilDTO.IsActive);
                    command.Parameters.AddWithValue("@CreatedByUserID", ilDTO.CreatedByUserID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }

        }
        public static List<clsInternationalLicenseDTO> GetAllInternationalLicenses()
        {
            var ListInternationalLicenses = new List<clsInternationalLicenseDTO>();
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetAllInternationalLicenses", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListInternationalLicenses.Add(new clsInternationalLicenseDTO(
                                reader.GetInt32(reader.GetOrdinal("InternationalLicenseID")),
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("DriverID")),
                                reader.GetInt32(reader.GetOrdinal("IssuedUsingLocalLicenseID")),
                                reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                                reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                                reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))

                                ));
                        }
                    }
                }
            }
            return ListInternationalLicenses;
        }
        public static List<clsDriverInternationalLicensesViewDTO> GetDriverInternationalLicenses(int driverID)
        {
            var ListDriverInternationalLicenses = new List<clsDriverInternationalLicensesViewDTO>();
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetDriverInternationalLicenses", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverID", driverID);
                    conection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListDriverInternationalLicenses .Add(new clsDriverInternationalLicensesViewDTO(
                                reader.GetInt32(reader.GetOrdinal("InternationalLicenseID")),
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("IssuedUsingLocalLicenseID")),
                                reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                                reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                                reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                ));
                        }
                    }
                }
            }
            return ListDriverInternationalLicenses;
        }
        public static int GetActiveInternationalLicenseIDByDriverID(int driver)
        {
            var InternationalLicenseID = -1;
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetActiveInternationalLicenseIDByDriverID", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverID", driver);
                    conection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int value))
                    {
                        InternationalLicenseID = value;
                    }
                }

            }
            return InternationalLicenseID;
        }
        
    }
}
