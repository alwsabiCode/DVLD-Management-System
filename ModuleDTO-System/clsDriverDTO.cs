using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsDriverDTO
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreateDate { get; set; }

        public clsDriverDTO()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreateDate = DateTime.Now;
        }
        public clsDriverDTO(int driverID,int personID,int createByUserID,DateTime createDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createByUserID;
            CreateDate = createDate;


        }
    }
}
