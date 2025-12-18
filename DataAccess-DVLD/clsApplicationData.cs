using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;


namespace DataAccess_DVLD
{
    public class clsApplicationData
    {
        public static clsApplicationDTO FindBaseApplication(int ApplicatinID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindBaseApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", ApplicatinID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsApplicationDTO(
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),
                                reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                                reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                                reader.GetByte(reader.GetOrdinal("ApplicationStatus")),
                                reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                                reader.GetDecimal(reader.GetOrdinal("PaidFees")),
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
        public static int AddNewApplication(clsApplicationDTO applicationDTO)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicantPersonID", applicationDTO.ApplicationPersonID);
                    command.Parameters.AddWithValue("@ApplicationDate", applicationDTO.ApplicationDate);
                    command.Parameters.AddWithValue("@ApplicationTypeID", applicationDTO.ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationStatus", applicationDTO.ApplicationStatus);
                    command.Parameters.AddWithValue("@LastStatusDate", applicationDTO.LastStatusDate);
                    command.Parameters.AddWithValue("@PaidFees", applicationDTO.PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", applicationDTO.CreatedByUserID);

                    var OutPut = new SqlParameter("@ApplicationID", SqlDbType.Int)
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
        public static bool UpdateApplication(clsApplicationDTO applicationDTO)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateApplication ", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", applicationDTO.ApplicationID);
                    command.Parameters.AddWithValue("@ApplicantPersonID", applicationDTO.ApplicationPersonID);
                    command.Parameters.AddWithValue("@ApplicationDate", applicationDTO.ApplicationDate);
                    command.Parameters.AddWithValue("@ApplicationTypeID", applicationDTO.ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationStatus", applicationDTO.ApplicationStatus);
                    command.Parameters.AddWithValue("@LastStatusDate", applicationDTO.LastStatusDate);
                    command.Parameters.AddWithValue("@PaidFees", applicationDTO.PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", applicationDTO.CreatedByUserID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }

            }
        }
        public static List<clsApplicationDTO> GetAllApplication()
        {
            List<clsApplicationDTO> ListApplication = new List<clsApplicationDTO>();
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllApplications", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListApplication.Add(new clsApplicationDTO(
                                reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),
                                reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                                reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                                reader.GetByte(reader.GetOrdinal("ApplicationStatus")),
                                reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                                reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))


                                ));

                        }

                    }
                }

            }
            return ListApplication;
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteApplication", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        public static bool IsApplicationExist(int ApplicationID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_IsApplicationExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ApplicationID", ApplicationID);
                    var output = new SqlParameter("@Exist", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(output);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (bool)output.Value;

                }
            }

        }
        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int GetActiveApplicationID = -1;
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetActiveApplicationID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result!=null && int.TryParse(result.ToString(),out int value))
                    {
                        GetActiveApplicationID=value;
                    }
                    


                }
            }
            return GetActiveApplicationID;

        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            var GetActiveApplicationIDForLicenseClass=-1;
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetActiveApplicationIDForLicenseClass", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result!=null && int.TryParse(result.ToString(),out int value))
                    {
                        GetActiveApplicationIDForLicenseClass=value;
                    }

                }
            }
            return GetActiveApplicationIDForLicenseClass;
        }
        public static bool UpdateStatus(int ApplicationID,short NewStatus)
        {
            using (SqlConnection connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command=new SqlCommand("SP_UpdateStatus",connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NewStatus", NewStatus);
                    command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }

        }
    }
}