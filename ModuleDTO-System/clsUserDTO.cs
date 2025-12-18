using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsUserDTO
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUserDTO()
        {
            UserID = -1;
            PersonID = -1;
            FullName = "";
            Username = "";
            Password = "";
            IsActive = true;

        }
        public clsUserDTO(int userID, int personID, string fullName, string username, string password, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            FullName = fullName;
            Username = username;
            Password = password;
            IsActive = isActive;
        }
    }
}
