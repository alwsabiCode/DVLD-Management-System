using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsDetainedLicenseViewDTO
    {
        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }
        public bool IsReleased { set; get; }
        public decimal FineFees { set; get; }
        public DateTime ReleaseDate { set; get; }
        public string NationalNo { set; get; }
        public string FullName { set; get; }
        public int ReleaseApplicationID { set; get; }

        public clsDetainedLicenseViewDTO(
        int detainID ,
        int licenseID,
        DateTime detainDate ,
        bool isReleased ,     
        decimal fineFees ,    
        DateTime releaseDate ,
        string nationalNo  ,  
        string fullName ,     
        int releaseApplicationID )
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            IsReleased = isReleased;
            FineFees = fineFees;
            ReleaseDate = releaseDate;
            NationalNo = nationalNo;
            FullName = fullName;
            ReleaseApplicationID = releaseApplicationID;

        }

        

    }    
}
