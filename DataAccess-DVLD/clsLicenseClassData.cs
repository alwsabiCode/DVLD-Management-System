using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;

namespace DataAccess_DVLD
{
    public class clsLicenseClassData
    {
        public static List<clsLicenseClassDTO> GetAllLicenseClass()
        {
            var listLicenseClass = new List<clsLicenseClassDTO>();
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllLicenseClass", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listLicenseClass.Add(new clsLicenseClassDTO(
                                reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                                reader.GetString(reader.GetOrdinal("ClassName")),
                                reader.GetString(reader.GetOrdinal("ClassDescription")),
                                reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),
                                reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),
                                reader.GetDecimal(reader.GetOrdinal("ClassFees"))

                                ));
                        }
                    }
                }
            }
            return listLicenseClass;
        }
        public static clsLicenseClassDTO Find(int licenseClassID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindLicenseClass", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsLicenseClassDTO(
                                reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                                reader.GetString(reader.GetOrdinal("ClassName")),
                                reader.GetString(reader.GetOrdinal("ClassDescription")),
                                reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),
                                reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),
                                reader.GetDecimal(reader.GetOrdinal("ClassFees"))

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
        public static clsLicenseClassDTO Find(string ClassName)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindLicenseClassName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassName", ClassName);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsLicenseClassDTO(
                                reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                                reader.GetString(reader.GetOrdinal("ClassName")),
                                reader.GetString(reader.GetOrdinal("ClassDescription")),
                                reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),
                                reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),
                                reader.GetDecimal(reader.GetOrdinal("ClassFees"))

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
        public static int AddNewLicenseClass(clsLicenseClassDTO licenseClassDTO)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewLicenseClass", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassName", licenseClassDTO.ClassName);
                    command.Parameters.AddWithValue("@ClassDescription", licenseClassDTO.ClassDescription);
                    command.Parameters.AddWithValue("@MinimumAllowedAge", licenseClassDTO.MinimumAllowedAge);
                    command.Parameters.AddWithValue("@DefaultValidityLength", licenseClassDTO.DefaultValidityLength);
                    command.Parameters.AddWithValue("@ClassFees", licenseClassDTO.ClassFees);
                    SqlParameter outputIdParam = new SqlParameter("@LicenseClassID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(outputIdParam.Value);
                }
            }
        }
        public static bool UpdateLicenseClass(clsLicenseClassDTO licenseClassDTO)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateLicenseClass", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseClassID", licenseClassDTO.LicenseClassID);
                    command.Parameters.AddWithValue("@ClassName", licenseClassDTO.ClassName);
                    command.Parameters.AddWithValue("@ClassDescription", licenseClassDTO.ClassDescription);
                    command.Parameters.AddWithValue("@MinimumAllowedAge", licenseClassDTO.MinimumAllowedAge);
                    command.Parameters.AddWithValue("@DefaultValidityLength", licenseClassDTO.DefaultValidityLength);
                    command.Parameters.AddWithValue("@ClassFees", licenseClassDTO.ClassFees);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}