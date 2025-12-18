using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;
using DataAccess_DVLD;

namespace Business_DVLD
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int UserID { get; set; }

        public clsPerson PersonInfo;

        private clsUserDTO _UserDTO;
        public clsUserDTO UserDTO
        {
            get { return _UserDTO; }
        }
        public clsUser(clsUserDTO UDTO, enMode eMode = enMode.AddNew)
        {
            _UserDTO = UDTO ?? new clsUserDTO();
            this.Mode = eMode;
            this.UserID = _UserDTO.UserID;
            this.PersonInfo = clsPerson.Find(_UserDTO.PersonID);
        }
        public static clsUser FindUserByUserID(int UserID)
        {
            clsUserDTO userDTO = clsUserData.FindUserByUserID(UserID);
            if (userDTO != null)
            {
                return new clsUser(userDTO, enMode.Update);
            }
            else
            {
                return null;
            }

        }
        public static clsUser FindUserByPersonID(int PersonID)
        {
            clsUserDTO userDTO = clsUserData.FindUserByPersonID(PersonID);
            if (userDTO != null)
            {
                return new clsUser(userDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByUsernameAndPassword(string Username, string Password)
        {
            clsUserDTO userDTO = clsUserData.FindByUsernameAndPassword(Username, Password);
            if (userDTO != null)
            {
                return new clsUser(userDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static List<clsUserDTO> GetAllUsers()
        {
            return clsUserData.GetAllUsers();

        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(_UserDTO);
            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(_UserDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }
        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }
       
    }
}
