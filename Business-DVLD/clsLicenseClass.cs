using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_DVLD;

namespace Business_DVLD
{
    public class clsLicenseClass
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 };
        public enMode Mode = enMode.AddNew;
        public int LicenseClassID { get; set; }

        private clsLicenseClassDTO _LCDTO;
        public clsLicenseClassDTO LicenseClassDTO
        {
            get
            {
                return _LCDTO;
            }
        }
        public clsLicenseClass(clsLicenseClassDTO LCDTO, enMode eMode = enMode.AddNew)
        {
            _LCDTO = LCDTO ?? new clsLicenseClassDTO();
            this.Mode = eMode;
            this.LicenseClassID = _LCDTO.LicenseClassID;
        }
        public static clsLicenseClass Find(int licenseClassID)
        {
            clsLicenseClassDTO classDTO = clsLicenseClassData.Find(licenseClassID);
            if (classDTO != null)
            {
                return new clsLicenseClass(classDTO, enMode.UpdateNew);
            }
            else
            {
                return null;
            }
        }
        public static clsLicenseClass Find(string ClassName)
        {
            clsLicenseClassDTO classDTO = clsLicenseClassData.Find(ClassName);
            if (classDTO != null)
            {
                return new clsLicenseClass(classDTO, enMode.UpdateNew);
            }
            else
            {
                return null;
            }
        }
        public static List<clsLicenseClassDTO> GetAllLicenseClass()
        {
            return clsLicenseClassData.GetAllLicenseClass();
        }
        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassData.AddNewLicenseClass(_LCDTO);
            return (this.LicenseClassID != -1);

        }
        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassData.UpdateLicenseClass(_LCDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {
                        Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateNew:
                    return _UpdateLicenseClass();

            }
            return false;
        }
    }
}
