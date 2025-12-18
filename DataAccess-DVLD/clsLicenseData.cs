using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess_DVLD
{
    public class clsLicenseData
    {
        public static clsLicenseDTO FindByLicenseID(int licenseID)
        {
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindLicenseByID", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", licenseID);
                    conection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           return new clsLicenseDTO(
                               reader.GetInt32(reader.GetOrdinal("LicenseID")),
                               reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                               reader.GetInt32(reader.GetOrdinal("DriverID")),
                               reader.GetInt32(reader.GetOrdinal("LicenseClass")),
                               reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                               reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                               reader.IsDBNull(reader.GetOrdinal("Notes"))?" ":
                               reader.GetString(reader.GetOrdinal("Notes")),
                               reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                               reader.GetBoolean(reader.GetOrdinal("IsActive")),
                               reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                               reader.GetByte(reader.GetOrdinal("IssueReason"))
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
        public static int AddNewLicense(clsLicenseDTO licenseDTO)
        {
            using (var connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command =new SqlCommand("SP_AddNewLicense",connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", licenseDTO.ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", licenseDTO.DriverID);
                    command.Parameters.AddWithValue("@LicenseClass", licenseDTO.LicenseClass);
                    command.Parameters.AddWithValue("@IssueDate", licenseDTO.IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", licenseDTO.ExpirationDate);

                    if (licenseDTO.Notes == "")
                        command.Parameters.AddWithValue("@Notes", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@Notes",licenseDTO.Notes);

                    command.Parameters.AddWithValue("@PaidFees",licenseDTO.PaidFees);
                    command.Parameters.AddWithValue("@IsActive",licenseDTO. IsActive);
                    command.Parameters.AddWithValue("@IssueReason", licenseDTO.IssueReason);

                    command.Parameters.AddWithValue("@CreatedByUserID", licenseDTO.CreatedByUserID);

                    var output = new SqlParameter("@LicenseID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(output);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(output.Value);

                }

            }

        }
        public static List<clsLicenseDTO> GetAllLicenses()
        {
            var ListLicenses = new List<clsLicenseDTO>();
            using (var connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command=new SqlCommand("SP_GetAllLicenses",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader= command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListLicenses.Add(new clsLicenseDTO(
                               reader.GetInt32(reader.GetOrdinal("LicenseID")),
                               reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                               reader.GetInt32(reader.GetOrdinal("DriverID")),
                               reader.GetInt32(reader.GetOrdinal("LicenseClass")),
                               reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                               reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                               reader.GetString(reader.GetOrdinal("Notes")),
                               reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                               reader.GetBoolean(reader.GetOrdinal("IsActive")),
                               reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                               reader.GetByte(reader.GetOrdinal("IssueReason"))
                                ));
                        }

                    }
                }
            }
            return ListLicenses;
        }
        public static bool DeactivateLicense(int licenceID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_DeactivateLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", licenceID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
            }
        }
        public static bool UpdateLicense(clsLicenseDTO licenseDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdateLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", licenseDTO.LicenseID);
                    command.Parameters.AddWithValue("@ApplicationID", licenseDTO.ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", licenseDTO.DriverID);
                    command.Parameters.AddWithValue("@LicenseClass", licenseDTO.LicenseClass);
                    command.Parameters.AddWithValue("@IssueDate", licenseDTO.IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", licenseDTO.ExpirationDate);

                    if (licenseDTO.Notes == "")
                        command.Parameters.AddWithValue("@Notes", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@Notes", licenseDTO.Notes);

                    command.Parameters.AddWithValue("@PaidFees", licenseDTO.PaidFees);
                    command.Parameters.AddWithValue("@IsActive", licenseDTO.IsActive);
                    command.Parameters.AddWithValue("@IssueReason", licenseDTO.IssueReason);
                    command.Parameters.AddWithValue("@CreatedByUserID", licenseDTO.CreatedByUserID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
            }
        }
        public static List<clsDriverLicensesViewDTO> GetDriverLicenses(int Driver)
        {
            List<clsDriverLicensesViewDTO> licenses = new List<clsDriverLicensesViewDTO>();

            using (var connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command =new SqlCommand("SP_GetDriverLicenses",connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverID",Driver);
                    connection.Open();
                    using (SqlDataReader reader=command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            licenses.Add(new clsDriverLicensesViewDTO(
                                reader.GetInt32(reader.GetOrdinal("LicenseID")),
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetString(reader.GetOrdinal("ClassName")),
                                reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                                reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                                reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                ));
                        }
                        return licenses;
                    }

                }

            }
        }
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int GetActiveLicenseIDByPersonID = -1;
            using (var conection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command=new SqlCommand("SP_GetActiveLicenseIDByPersonID",conection))
                {
                   command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID",PersonID);
                    command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
                    conection.Open();
                    object result = command.ExecuteScalar();
                    if (result!=null && int.TryParse(result.ToString(),out int value ))
                    {
                       GetActiveLicenseIDByPersonID=value;
                    }


                }

            }
            return GetActiveLicenseIDByPersonID;
        }
    }
}
