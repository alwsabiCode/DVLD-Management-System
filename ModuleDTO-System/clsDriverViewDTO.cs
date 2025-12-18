using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsDriverViewDTO
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FullName { get; set; }
        public DateTime CrearedDate { get; set; }
        public int NumberOfActiveLicenses { get; set; }

        public clsDriverViewDTO(
            int driverID, int personID,
            string nationalNo, string fullName,
            DateTime createdDate, int numberOfActiveLicenses)
        {
            DriverID = driverID;
            PersonID = personID;
            NationalNo = nationalNo;
            FullName = fullName;
            CrearedDate = createdDate;
            NumberOfActiveLicenses = numberOfActiveLicenses;
        }
    }
}
