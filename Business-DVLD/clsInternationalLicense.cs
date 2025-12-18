using DataAccess_DVLD;
using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_DVLD
{
    public class clsInternationalLicense : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { set; get; }

        private clsInternationalLicenseDTO _ILDTO;
        public clsInternationalLicenseDTO InternationalDTO
        {
            get
            {
                return _ILDTO;
            }

        }
        public clsDriver DriverInfo;

        public clsInternationalLicense(clsInternationalLicenseDTO ILDTO, clsApplicationDTO ADTO, enMode eMode = enMode.AddNew)
            : base(ADTO, (clsApplication.enMode)eMode)
        {
            _ILDTO = ILDTO ?? new clsInternationalLicenseDTO();
            this.Mode=eMode;
            this.InternationalLicenseID = _ILDTO.InternationalLicenseID;
            this.DriverInfo = clsDriver.FindByDriverID(_ILDTO.DriverID);

        }
        public static clsInternationalLicense FindByInternationalLicenseID(int internationalLicenseID)
        {
            clsInternationalLicenseDTO ilDTO = clsInternationalLicenseData.FindByInternationalLicenseID(internationalLicenseID);
            if (ilDTO != null)
            {
                clsApplicationDTO aDTO = clsApplicationData.FindBaseApplication(ilDTO.ApplicationID);
                return new clsInternationalLicense(ilDTO, aDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        private bool _AddInternationalLicense()
        {
            int NewID = clsInternationalLicenseData.AddInternationalLicense(_ILDTO);
            if (NewID <= 0)
            {
                return false;
            }
            this.InternationalLicenseID = NewID;
            return true;
        }
        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(_ILDTO);
        }
        public static List<clsInternationalLicenseDTO> GetAllInternationalLicense()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }
        public bool Save()
        {
            base.Mode=(clsApplication.enMode)Mode;
            if (!base.Save())
            {
                return false;
            }
            InternationalDTO.ApplicationID = base.ApplicationID;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateInternationalLicense();

            }
            return false;
        }
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }

        public static List<clsDriverInternationalLicensesViewDTO> GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
    }
}