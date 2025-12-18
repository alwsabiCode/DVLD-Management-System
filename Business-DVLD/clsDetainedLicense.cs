using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;
using DataAccess_DVLD;

namespace Business_DVLD
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DetainID { set; get; }

        private clsDetainedLicenseDTO _DLDTO;
        public clsDetainedLicenseDTO DetainedLicenseDTO
        {
            get
            {
                return _DLDTO;
            }
        }
        public clsUser CreatedByUserInfo;
        public clsUser ReleasedByUserInfo;
        public clsDetainedLicense(clsDetainedLicenseDTO DLDTO, enMode eMode = enMode.AddNew)
        {
            _DLDTO = DLDTO ?? new clsDetainedLicenseDTO();
            this.DetainID = _DLDTO.DetainID;
            this.Mode = eMode;
            this.CreatedByUserInfo = clsUser.FindUserByUserID(_DLDTO.CreatedByUserID);
            this.ReleasedByUserInfo = clsUser.FindUserByUserID(_DLDTO.ReleasedByUserID);
        }
        public static clsDetainedLicense Find(int DetainID)
        {
            clsDetainedLicenseDTO detainedLicenseDTO = clsDetainedLicenseData.Find(DetainID);
            if (detainedLicenseDTO != null)
            {
                return new clsDetainedLicense(detainedLicenseDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            clsDetainedLicenseDTO detainedLicenseDTO = clsDetainedLicenseData.FindByLicenseID(LicenseID);
            if (detainedLicenseDTO != null)
            {
                return new clsDetainedLicense(detainedLicenseDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        public static List<clsDetainedLicenseViewDTO> GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }
        
        public bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(_DLDTO);
            return (this.DetainID !=-1);

        }
        public bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(_DLDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDetainedLicense();

            }
            return false;
        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }
    }
}
