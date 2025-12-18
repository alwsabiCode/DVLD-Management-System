using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;

namespace DataAccess_DVLD
{
    public class clsPersonData
    {
        public static List<clsPersonDTO> GetAllPeople()
        {
            var peopleList = new List<clsPersonDTO>();
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllPeople", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            peopleList.Add(new clsPersonDTO(
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("NationalNo")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("SecondName")),
                                reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? "" :
                                reader.GetString(reader.GetOrdinal("ThirdName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                reader.GetByte(reader.GetOrdinal("Gendor")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("CountryName")),
                                reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                                reader.GetString(reader.GetOrdinal("Phone")),
                                reader.IsDBNull(reader.GetOrdinal("Email")) ? ""
                                : reader.GetString(reader.GetOrdinal("Email")),
                                reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? ""
                                : reader.GetString(reader.GetOrdinal("ImagePath"))
                                ));

                        }
                    }
                }
            }
            return peopleList;
        }
        public static clsPersonDTO GetPersonByID(int PersonID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPersonByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsPersonDTO(
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("NationalNo")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("SecondName")),
                                reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? "" :
                                reader.GetString(reader.GetOrdinal("ThirdName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                reader.GetByte(reader.GetOrdinal("Gendor")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("CountryName")),

                                reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                                reader.GetString(reader.GetOrdinal("Phone")),
                                reader.IsDBNull(reader.GetOrdinal("Email")) ? ""
                                : reader.GetString(reader.GetOrdinal("Email")),
                                reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? ""
                                : reader.GetString(reader.GetOrdinal("ImagePath"))
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
        public static clsPersonDTO GetPersonByNationalNo(string NationalNo)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPersonByNationalNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsPersonDTO(
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("NationalNo")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("SecondName")),
                                reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? "" :
                                reader.GetString(reader.GetOrdinal("ThirdName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                reader.GetByte(reader.GetOrdinal("Gendor")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("CountryName")),

                                reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                                reader.GetString(reader.GetOrdinal("Phone")),
                                reader.IsDBNull(reader.GetOrdinal("Email")) ? ""
                                : reader.GetString(reader.GetOrdinal("Email")),
                                reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? ""
                                : reader.GetString(reader.GetOrdinal("ImagePath"))
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
        public static int AddNewPerson(clsPersonDTO PDTO)
        {
            using (var connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using(var command =new SqlCommand("SP_AddNewPerson", connection))
                {
                    command.CommandType= CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NationalNo",PDTO.NationalNo);
                    command.Parameters.AddWithValue("@FirstName", PDTO.FirstName);
                    command.Parameters.AddWithValue("@SecondName", PDTO.SecondName);
                    if (PDTO.ThirdName != "" && PDTO.ThirdName != null)
                        command.Parameters.AddWithValue("@ThirdName", PDTO.ThirdName);
                    else
                        command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

                    command.Parameters.AddWithValue("@LastName", PDTO.LastName);

                    if (PDTO.DateOfBirth < (DateTime)SqlDateTime.MinValue)
                        command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@DateOfBirth", PDTO.DateOfBirth);
                    
                    command.Parameters.AddWithValue("@Gendor", PDTO.Gendor);
                    command.Parameters.AddWithValue("@Address", PDTO.Address);
                    command.Parameters.AddWithValue("@Phone", PDTO.Phone);

                    if(PDTO.Email !=""&& PDTO.Email != null)
                        command.Parameters.AddWithValue("@Email",PDTO.Email);
                    else
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);

                    command.Parameters.AddWithValue("@NationalityCountryID", PDTO.NationalityCountryID);
                    if (PDTO.ImagePath != "" && PDTO.ImagePath != null)
                        command.Parameters.AddWithValue("@ImagePath",PDTO.ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

                    var OutPutID = new SqlParameter("@PersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,

                    };
                    command.Parameters.Add(OutPutID);
                    connection.Open();
                    command.ExecuteNonQuery();


                    return Convert.ToInt32(OutPutID.Value);
                }
            }
        }
        public static bool UpdatePerson(clsPersonDTO PDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdatePerson", connection)) 
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID",PDTO.PersonID);
                    command.Parameters.AddWithValue("@NationalNo", PDTO.NationalNo);
                    command.Parameters.AddWithValue("@FirstName", PDTO.FirstName);
                    command.Parameters.AddWithValue("@SecondName", PDTO.SecondName);
                    if (PDTO.ThirdName != "" && PDTO.ThirdName != null)
                        command.Parameters.AddWithValue("@ThirdName", PDTO.ThirdName);
                    else
                        command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

                    command.Parameters.AddWithValue("@LastName", PDTO.LastName);

                    if (PDTO.DateOfBirth < (DateTime)SqlDateTime.MinValue)
                        command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@DateOfBirth", PDTO.DateOfBirth);

                    command.Parameters.AddWithValue("@Gendor", PDTO.Gendor);
                    command.Parameters.AddWithValue("@Address", PDTO.Address);
                    command.Parameters.AddWithValue("@Phone", PDTO.Phone);

                    if (PDTO.Email != "" && PDTO.Email != null)
                        command.Parameters.AddWithValue("@Email", PDTO.Email);
                    else
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);

                    command.Parameters.AddWithValue("@NationalityCountryID", PDTO.NationalityCountryID);
                    if (PDTO.ImagePath != "" && PDTO.ImagePath != null)
                        command.Parameters.AddWithValue("@ImagePath", PDTO.ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;



                
                }
            }

        }
        public static bool DeletePerson(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_DeletePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", Id);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return (rowsAffected>0);
                    }

                }
            }
            catch(SqlException ex)
            {
               return false;
            }
        }
        public static bool isPersonExist(string nationalNo)
        {
            using (var connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command=new SqlCommand("SP_IsPersonExist",connection))
                {
                    command.CommandType= CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NationalNo", nationalNo);
                    var outputParam = new SqlParameter("@Exist",SqlDbType.Bit)
                    {
                        Direction= ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (bool)outputParam.Value;
                }
            }
        }

    }
}

