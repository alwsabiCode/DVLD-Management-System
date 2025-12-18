using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsDriverLicensesViewDTO
    {
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public string ClassName { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }
    
        public clsDriverLicensesViewDTO(int licenseID, int applicationID, string className, DateTime issueDate, DateTime expirationDate, bool isActive)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            ClassName = className;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
        }
    }
}
