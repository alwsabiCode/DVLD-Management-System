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
    public class clsDriver
    {
        public enum enMode {AddNew=0,Update=1 };
        public enMode Mode=enMode.AddNew;

        public int DriverID { set;get;}

        public clsPerson PersonInfo;

        private clsDriverDTO _DDTO;
        public clsDriverDTO DriverDTO
        {
            get { return _DDTO; }
        }
        public clsDriver(clsDriverDTO DDTO,enMode eMode=enMode.AddNew)
        {
            _DDTO = DDTO ??new clsDriverDTO();
            this.Mode = eMode;
            this.DriverID = _DDTO.DriverID;
            this.PersonInfo= clsPerson.Find(_DDTO.PersonID);

        }
        public static clsDriver FindByDriverID(int driverID)
        {
            clsDriverDTO driverDTO = clsDriverData.FindByDriverID(driverID);
            if (driverDTO != null)
            {
                return new clsDriver(driverDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static clsDriver FindByPersonID(int driverID)
        {
            clsDriverDTO driverDTO = clsDriverData.FindByPersonID(driverID);
            if (driverDTO != null)
            {
                return new clsDriver(driverDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(DriverDTO);
            return (this.DriverID != -1);
        }
        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(DriverDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDriver();
            }
            return false;
        }
        public static List<clsDriverViewDTO> GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }
        public static List<clsDriverLicensesViewDTO> GetLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }

        public static List<clsDriverInternationalLicensesViewDTO> GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
        }

    }
}
