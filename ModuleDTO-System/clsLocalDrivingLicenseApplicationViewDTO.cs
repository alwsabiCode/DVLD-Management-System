using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsLocalDrivingLicenseApplicationViewDTO
    {
        public int LocalDrivingLicenseApplicationID { get; set; }
        public string ClassName { get; set; }
        public string NationalNo { get; set; }
        public string FullName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int PassedTestCount { get; set; }
        public string Status { get; set; }
        
        public clsLocalDrivingLicenseApplicationViewDTO(
            int id, string className, string nationalNo,
            string fullName, DateTime applicationDate,
            int passedCount, string status)
        {
            LocalDrivingLicenseApplicationID = id;
            ClassName = className;
            NationalNo = nationalNo;
            FullName = fullName;
            ApplicationDate = applicationDate;
            PassedTestCount = passedCount;
            Status = status;
        }
    }

}
