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
    public class clsUserData
    {
        public static clsUserDTO FindUserByUserID(int UserID)
        {
            using (SqlConnection connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetUserByUserID", connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    connection.Open();
                    using (SqlDataReader reader=command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsUserDTO(
                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Password")),
                                reader.GetBoolean(reader.GetOrdinal("isActive"))

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
        public static clsUserDTO FindUserByPersonID(int PersonID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetUserByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsUserDTO(
                               reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Password")),
                                reader.GetBoolean(reader.GetOrdinal("isActive"))
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

        public static List<clsUserDTO>GetAllUsers()
        {
            var ListUser=new List<clsUserDTO>();
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command=new SqlCommand ("SP_GetAllUser",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader= command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListUser.Add(new clsUserDTO(
                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Password")),
                                reader.GetBoolean(reader.GetOrdinal("isActive"))
                                ));

                        }

                    }


                }

            }
            return ListUser;
        }

        public static clsUserDTO FindByUsernameAndPassword(string UserName, string Password)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetUserByUserNameAndPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsUserDTO(
                               reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Password")),
                                reader.GetBoolean(reader.GetOrdinal("isActive"))
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

        public static int AddNewUser(clsUserDTO userDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddNewUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
                    command.Parameters.AddWithValue("@UserName", userDTO.Username);
                    command.Parameters.AddWithValue("@Password", userDTO.Password);
                    command.Parameters.AddWithValue("@IsActive", userDTO.IsActive);

                    var output = new SqlParameter("@UserID", SqlDbType.Int)
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
        public static bool UpdateUser(clsUserDTO userDTO)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userDTO.UserID);
                    command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
                    command.Parameters.AddWithValue("@UserName", userDTO.Username);
                    command.Parameters.AddWithValue("@Password", userDTO.Password);
                    command.Parameters.AddWithValue("@IsActive", userDTO.IsActive);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
        public static bool DeleteUser(int ID)
        {
            try
            {
                using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", ID);
                        connection.Open();
                        int rowsAffectecd = command.ExecuteNonQuery();
                        return (rowsAffectecd > 0);
                    }
                }
            }
            catch(SqlException ex)
            {
                return false;
            }
        }
        public static bool IsUserExist(string UserName)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsUserExistUserName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    var output = new SqlParameter("@Exist", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(output);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (bool)output.Value;
                }
            }
        }
        public static bool IsUserExist(int UserID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsUserExistUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    var output = new SqlParameter("@Exist", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(output);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (bool)output.Value;
                }
            }
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsUserExistForPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    var output = new SqlParameter("@Exist", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(output);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (bool)output.Value;
                }
            }
        }

    }
}
