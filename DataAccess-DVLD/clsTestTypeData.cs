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
    public class clsTestTypeData
    {
        public static clsTestTypeDTO FindTestTypeByID(int TestType)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindTestTypeByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestTypeID", TestType);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsTestTypeDTO(
                                reader.GetInt32(reader.GetOrdinal("TestTypeID")),
                                reader.GetString(reader.GetOrdinal("TestTypeTitle")),
                                reader.GetString(reader.GetOrdinal("TestTypeDescription")),
                                reader.GetDecimal(reader.GetOrdinal("TestTypeFees"))

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
        public static List<clsTestTypeDTO> GetAllTestType()
        {
            var ListTestType = new List<clsTestTypeDTO>();
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetAllTestType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListTestType.Add(new clsTestTypeDTO(
                                reader.GetInt32(reader.GetOrdinal("TestTypeID")),
                                reader.GetString(reader.GetOrdinal("TestTypeTitle")),
                                reader.GetString(reader.GetOrdinal("TestTypeDescription")),
                                reader.GetDecimal(reader.GetOrdinal("TestTypeFees"))

                            ));

                        }

                    }


                }

            }
            return ListTestType;
        }
        public static int AddNewTestType(clsTestTypeDTO testTypeDTO)
        {
            using(var connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using(var command = new SqlCommand("SP_AddNewTestType", connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestTypeTitle", testTypeDTO.Title);
                    command.Parameters.AddWithValue("@TestTypeDescription", testTypeDTO.Description);
                    command.Parameters.AddWithValue("@TestTypeFees", testTypeDTO.Fees);
                    connection.Open() ;
                    var output = new SqlParameter("@TestTypeID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,

                    };
                    command.Parameters.Add(output);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(output.Value);


                }

            }

        }
        public static bool UpdateTestType(clsTestTypeDTO testTypeDTO) 
        {
            using (var connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command=new SqlCommand("SP_UpdateTestType",connection))
                {
                    command.CommandType= CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestTypeID", testTypeDTO.ID);
                    command.Parameters.AddWithValue("@TestTypeTitle", testTypeDTO.Title);
                    command.Parameters.AddWithValue("@TestTypeDescription", testTypeDTO.Description);
                    command.Parameters.AddWithValue("@TestTypeFees", testTypeDTO.Fees);
                    connection.Open() ;
                    command.ExecuteNonQuery();
                    return true;

                }

            }
        }
    }
}
