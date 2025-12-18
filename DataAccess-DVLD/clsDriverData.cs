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
    public class clsDriverData
    {
        public static clsDriverDTO FindByDriverID(int driverID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindDriverByDriverID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverID", driverID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsDriverDTO(
                                reader.GetInt32(reader.GetOrdinal("DriverID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
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
        public static clsDriverDTO FindByPersonID(int personID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindDriverByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", personID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsDriverDTO(
                                reader.GetInt32(reader.GetOrdinal("DriverID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
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

        public static int AddNewDriver(clsDriverDTO DDTO)
        {
            using (SqlConnection connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command=new SqlCommand("SP_AddNewDriver",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", DDTO.PersonID);
                    command.Parameters.AddWithValue("@CreatedByUserID", DDTO.CreatedByUserID);
                    command.Parameters.AddWithValue("@CreateDate", DDTO.CreateDate);
                    connection.Open();
                   var output=new SqlParameter("@DriverID",SqlDbType.Int)
                   {
                       Direction=ParameterDirection.Output
                   };
                      command.Parameters.Add(output);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32( output.Value);
                    
                }

            }

        }
        public static bool UpdateDriver(clsDriverDTO DDTO)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateDriver", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverID", DDTO.DriverID);
                    command.Parameters.AddWithValue("@PersonID", DDTO.PersonID);
                    command.Parameters.AddWithValue("@CreatedByUserID", DDTO.CreatedByUserID);
                    command.Parameters.AddWithValue("@CreateDate", DDTO.CreateDate);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                   
                }
            }
        }
        public static List<clsDriverViewDTO> GetAllDrivers()
        {
            var ListDriver = new List<clsDriverViewDTO>();
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllDrivers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListDriver.Add(new clsDriverViewDTO(
                                reader.GetInt32(reader.GetOrdinal("DriverID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("NationalNo")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                 reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                 reader.GetInt32(reader.GetOrdinal("NumberOfActiveLicenses"))
                                ));
                        }
                    }
                }
            }
            return ListDriver;
        }

    }
}



