using DataAccess_DVLD;
using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_DVLD
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType
        {
            NewDrivingLicense = 1,
            RenewDrivingLicense = 2,
            ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4,
            ReleaseDetainedDrivingLicsense = 5,
            NewInternationalLicense = 6,
            RetakeTest = 7
        };
        public enum enApplicationStatus
        {
            New = 1, Cancelled = 2, Completed = 3
        };

        public int ApplicationID { get; set; }

        private clsApplicationDTO _ADTO;
        public clsApplicationDTO ApplicatDTO
        {
            get
            {
                return _ADTO;
            }
        }
        public clsUser CreatedByUserInfo;
        public clsApplicationType ApplicationTypeInfo;
        public clsPerson PersonInfo;

        public enApplicationStatus ApplicationStatus { get; set; }
         
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";

                }
            }
        }
     

        public clsApplication(clsApplicationDTO ADTO,enMode eMode=enMode.AddNew)
        {
            _ADTO = ADTO ?? new clsApplicationDTO();
            this.Mode= eMode;
            this.ApplicationID= _ADTO.ApplicationID;
            this.ApplicationTypeInfo=clsApplicationType.FindApplicationTypeByID(_ADTO.ApplicationTypeID);
            this.CreatedByUserInfo = clsUser.FindUserByUserID(_ADTO.CreatedByUserID);
            this.PersonInfo = clsPerson.Find(_ADTO.ApplicationPersonID);

        }
        public static clsApplication FindBaseApplication(int applicationID)
        {
            clsApplicationDTO applicationDTO = clsApplicationData.FindBaseApplication(applicationID);
            if (applicationDTO != null)
            {
                return new clsApplication(applicationDTO, enMode.Update);
            }
            else
            {
                return null;
            }

        }
        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 2);
        }
        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(_ADTO);
            return (this.ApplicationID != -1);
        }
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(_ADTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }
        public bool Delete() 
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }
        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicationID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID,(int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicatDTO.ApplicationPersonID, ApplicationTypeID);
        }


    }
}
