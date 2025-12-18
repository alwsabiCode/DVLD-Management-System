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
    public class clsCountryData
    {
        public static List<clsCountryDTO> GetAllCountries()
        {
            var ListCountry= new List<clsCountryDTO>(); 
            using (SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllCountries", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListCountry.Add(new clsCountryDTO(
                                reader.GetInt32(reader.GetOrdinal("CountryID")),
                                reader.GetString(reader.GetOrdinal("CountryName"))
                                ));
                        }
                    }
                }
            }
            return ListCountry;

        }
        public static clsCountryDTO Find(string countryName)
        {
            using (var connection=new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using(var command = new SqlCommand("SP_FindCountryByName", connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CountryName",countryName);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader()) 
                    {
                        if (reader.Read())
                        {
                            return new clsCountryDTO(
                            reader.GetInt32(reader.GetOrdinal("CountryID")),
                                reader.GetString(reader.GetOrdinal("CountryName"))
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
        public static clsCountryDTO Find(int countryID)
        {
            using (var connection = new SqlConnection(clsConnectionSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindCountryByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CountryID", countryID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsCountryDTO(
                            reader.GetInt32(reader.GetOrdinal("CountryID")),
                                reader.GetString(reader.GetOrdinal("CountryName"))
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
    }
}
