using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsLocalDrivingLicenseApplicationDTO
    {
    
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }

        public int LicenseClassID { set; get; }

        public clsLocalDrivingLicenseApplicationDTO() 
        {
            LocalDrivingLicenseApplicationID = -1;
            ApplicationID = -1;  
            LicenseClassID = -1;
        }
        public clsLocalDrivingLicenseApplicationDTO(int localDrivingLicenseApplicationID,int applicationID ,int licenseClassID)
        {
            LocalDrivingLicenseApplicationID=localDrivingLicenseApplicationID;
            ApplicationID =applicationID;
            LicenseClassID=licenseClassID;

        }

    }
}
