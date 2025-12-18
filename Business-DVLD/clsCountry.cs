using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_DVLD;
using ModuleDTO_DVLD;

namespace Business_DVLD
{
    public class clsCountry
    {
        public int CountryID { get; set; }

        private clsCountryDTO _CDTO;
        public clsCountryDTO CountryDTO
        {
            get
            {
                return _CDTO;
            }
         
        }

        public clsCountry(clsCountryDTO CDTO)
        {
            _CDTO = CDTO ?? new clsCountryDTO();
            this.CountryID =_CDTO.CountryID;

        }
        public static clsCountry Find(string CountryName)
        {
            clsCountryDTO countryDTO = clsCountryData.Find(CountryName);
            if (countryDTO != null)
            {
                return new clsCountry(countryDTO);
            }
            else
            {
                return null;
            }

        }
        public static clsCountry Find(int CountryID)
        {
            clsCountryDTO countryDTO = clsCountryData.Find(CountryID);
            if (countryDTO != null)
            {
                return new clsCountry(countryDTO);
            }
            else
            {
                return null;
            }

        }
        public static List<clsCountryDTO> GetAllCountries()
        {
           return clsCountryData.GetAllCountries();
        }
    }
}
