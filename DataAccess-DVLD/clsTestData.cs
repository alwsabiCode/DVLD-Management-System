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
    public class clsTestData
    {
        public static clsTestDTO FindTestByTestID(int testID)
        {
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindTestByTestID", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestID", testID);
                    conection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsTestDTO(
                                reader.GetInt32(reader.GetOrdinal("TestID")),
                                reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                                reader.GetBoolean(reader.GetOrdinal("TestResult")),
                                reader.IsDBNull(reader.GetOrdinal("Notes"))?"":
                                reader.GetString(reader.GetOrdinal("Notes")),
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
        public static clsTestDTO FindLastTestPerPersonAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID)
        {
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FinFindLastTest", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    conection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsTestDTO(
                                reader.GetInt32(reader.GetOrdinal("TestID")),
                                reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                                reader.GetBoolean(reader.GetOrdinal("TestResult")),
                                reader.IsDBNull(reader.GetOrdinal("Notes"))? "":
                                reader.GetString(reader.GetOrdinal("Notes")),
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
        public static int AddNewTest(clsTestDTO testDTO)
        {
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddNewTest", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestAppointmentID", testDTO.TestAppointmentID);
                    command.Parameters.AddWithValue("@TestResult", testDTO.TestResult);
                    command.Parameters.AddWithValue("@Notes", testDTO.Note);
                    command.Parameters.AddWithValue("@CreatedByUserID", testDTO.CreatedByUerID);
                    var outPut = new SqlParameter("@TestID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outPut);
                    conection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(outPut.Value);

                }
            }
        }
        public static bool UpdateTest(clsTestDTO testDTO)
        {
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdateTest", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TesID", testDTO.TestID);
                    command.Parameters.AddWithValue("@TestAppointmentID", testDTO.TestAppointmentID);
                    command.Parameters.AddWithValue("@TestResult", testDTO.TestResult);
                    command.Parameters.AddWithValue("@Notes", testDTO.Note);
                    command.Parameters.AddWithValue("@CreatedByUserID", testDTO.CreatedByUerID);
                    conection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
            }
        }
        public static List<clsTestDTO> GetAllTest()
        {
            var ListTest = new List<clsTestDTO>();
            using (var conection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetAllTest", conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListTest.Add(new clsTestDTO(
                               reader.GetInt32(reader.GetOrdinal("TestID")),
                               reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                               reader.GetBoolean(reader.GetOrdinal("TestResult")),
                               reader.IsDBNull(reader.GetOrdinal("Notes"))? "" :
                                 reader.GetString(reader.GetOrdinal("Notes")),
                               reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))

                               ));
                        }

                    }
                }
            }
            return ListTest;
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;
            using (var conection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command=new SqlCommand("SP_GetPassedTestCount",conection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    conection.Open();
                    object result = command.ExecuteScalar();
                    if(result!=null && byte.TryParse(result.ToString(),out byte value))
                    {
                        PassedTestCount= value;
                    }

                }

            }
            return PassedTestCount;
        }
    }
    
}
