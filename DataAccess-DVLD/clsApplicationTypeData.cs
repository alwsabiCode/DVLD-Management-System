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
    public class clsApplicationTypeData
    {
        public static clsApplicationTypeDTO FindApplicationTypeByID(int ApplicationTypeID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindApplicationTypeByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsApplicationTypeDTO(
                                reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                                reader.GetString(reader.GetOrdinal("ApplicationTypeTitle")),
                                reader.GetDecimal(reader.GetOrdinal("ApplicationFees"))
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
        public static List<clsApplicationTypeDTO> GetAllApplicationTypes()
        {
            var applicationTypes = new List<clsApplicationTypeDTO>();
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllApplicationTypes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applicationTypes.Add(new clsApplicationTypeDTO(
                                reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                                reader.GetString(reader.GetOrdinal("ApplicationTypeTitle")),
                               reader.GetDecimal(reader.GetOrdinal("ApplicationFees"))
                                ));
                        }
                    }
                }
            }
            return applicationTypes;
        }
        public static int AddNewApplicationType(clsApplicationTypeDTO applicationType)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewApplicationType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationTypeTitle", applicationType.ApplicationTypeTitle);
                    command.Parameters.AddWithValue("@ApplicationTypeFees", applicationType.ApplicationTypeFees);
                    SqlParameter outputID = new SqlParameter("@ApplicationTypeID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(outputID.Value);
                }
            }
        }
        public static bool UpdateApplicationType(clsApplicationTypeDTO applicationType)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateApplicationType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationTypeID", applicationType.ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationTypeTitle", applicationType.ApplicationTypeTitle);
                    command.Parameters.AddWithValue("@ApplicationTypeFees", applicationType.ApplicationTypeFees);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}
