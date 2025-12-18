using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsApplicationTypeDTO
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationTypeFees { get; set; }
        public clsApplicationTypeDTO()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle ="";
            ApplicationTypeFees = 0;

        }
        public clsApplicationTypeDTO(int applicationTypeID, string applicationTypeTitle, decimal applicationTypeFees)
        {
           ApplicationTypeID = applicationTypeID;
           ApplicationTypeTitle = applicationTypeTitle;
           ApplicationTypeFees = applicationTypeFees;
        }
    }
}
