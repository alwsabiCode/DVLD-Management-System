using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsCountryDTO
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public clsCountryDTO()
        {
            CountryID = -1;
            CountryName = "";
        }
        public clsCountryDTO(int countryID, string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
        }


    }
}
