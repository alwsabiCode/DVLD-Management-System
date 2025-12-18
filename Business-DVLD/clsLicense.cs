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
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseID {  get; set; }
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get
            {
                switch (IssueReason)
                {
                    case enIssueReason.FirstTime:
                        return "First Time";
                    case enIssueReason.Renew:
                        return "Renew";
                    case enIssueReason.DamagedReplacement:
                        return "Replacement for Damaged";
                    case enIssueReason.LostReplacement:
                        return "Replacement for Lost";
                    default:
                        return "First Time";
                }
            }
        }
        public bool IsDetained
        {
            get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID); }
        }

        public clsDetainedLicense DetainedInfo;
        public clsLicenseClass LicenseClassIfo;
        public clsDriver DriverInfo;

        private clsLicenseDTO _LicenseDTO;
        public clsLicenseDTO licenseDTO
        {
            get
            {
                return _LicenseDTO;
            }

        }
        public clsLicense(clsLicenseDTO LDTO,enMode eMode=enMode.AddNew)
        {
            _LicenseDTO=LDTO??new clsLicenseDTO();
            this.Mode=eMode;
            this.LicenseID = _LicenseDTO.LicenseID;

            DriverInfo = clsDriver.FindByDriverID(_LicenseDTO.DriverID);
            LicenseClassIfo = clsLicenseClass.Find(_LicenseDTO.LicenseClass);
            DetainedInfo = clsDetainedLicense.FindByLicenseID(_LicenseDTO.LicenseID);

        }


        public static clsLicense FindByLicenseID(int licenseID)
        {
            clsLicenseDTO licenseDTO = clsLicenseData.FindByLicenseID(licenseID);
            if (licenseDTO != null)
            {
                return new clsLicense(licenseDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(licenseDTO);
            return (this.LicenseID != -1);
        }
        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(licenseDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicense();

            }

            return false;
        }
        public static List<clsLicenseDTO> GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }
      
        public static bool IsLicenseExistByPersonID(int PersonID,int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }
        public static List<clsDriverLicensesViewDTO> GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }
        public Boolean IsLicenseExpired()
        {
            return (this.licenseDTO.ExpirationDate < DateTime.Now);
        }
        public  bool DeactivateCurrentLicense()
        {
            return (clsLicenseData.DeactivateLicense(this.LicenseID));
        }
        public int Detain(decimal FineFees,int CreatedByUserID)
        {
            clsDetainedLicense _detainedLicense;
            _detainedLicense = new clsDetainedLicense(new clsDetainedLicenseDTO(),clsDetainedLicense.enMode.AddNew);
            _detainedLicense.DetainedLicenseDTO.LicenseID=this.LicenseID;
            _detainedLicense.DetainedLicenseDTO.DetainDate=DateTime.Now;
            _detainedLicense.DetainedLicenseDTO.FineFees=Convert.ToDecimal(FineFees);
            _detainedLicense.DetainedLicenseDTO.CreatedByUserID = CreatedByUserID;

            if (!_detainedLicense.Save())
            {
                return -1;

            }
            return _detainedLicense.DetainID;
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication _application = new clsApplication(new clsApplicationDTO(),clsApplication.enMode.AddNew);
            _application.ApplicatDTO.ApplicationPersonID = this.DriverInfo.DriverDTO.PersonID;
            _application.ApplicatDTO.ApplicationDate = DateTime.Now;
            _application.ApplicatDTO.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            _application.ApplicatDTO.ApplicationStatus = (byte)clsApplication.enApplicationStatus.Completed;
            _application.ApplicatDTO.LastStatusDate=DateTime.Now;
            _application.ApplicatDTO.PaidFees = clsApplicationType.FindApplicationTypeByID((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationTypeDTO.ApplicationTypeFees;
            _application.ApplicatDTO.CreatedByUserID = ReleasedByUserID;

            if (!_application.Save())
            {
                ApplicationID = -1;
                return false;
            }
            ApplicationID=_application.ApplicationID;
            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID,_application.ApplicationID);
        }
        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            clsApplication application = new clsApplication(new clsApplicationDTO(),clsApplication.enMode.AddNew);
            application.ApplicatDTO.ApplicationPersonID = this.DriverInfo.DriverDTO.PersonID;
            application.ApplicatDTO.ApplicationDate = DateTime.Now;
            application.ApplicatDTO.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            application.ApplicatDTO.ApplicationStatus = (byte)clsApplication.enApplicationStatus.Completed;
            application.ApplicatDTO.LastStatusDate = DateTime.Now;
            application.ApplicatDTO.PaidFees = clsApplicationType.FindApplicationTypeByID((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationTypeDTO.ApplicationTypeFees;
            application.ApplicatDTO.CreatedByUserID= CreatedByUserID;

            if (!application.Save())
            {
                return null;
            }
            clsLicense NewLicense = new clsLicense(new clsLicenseDTO(), clsLicense.enMode.AddNew);
            NewLicense._LicenseDTO. ApplicationID = application.ApplicationID;
            NewLicense._LicenseDTO.DriverID =this._LicenseDTO.DriverID;
            NewLicense._LicenseDTO.LicenseClass = this._LicenseDTO.LicenseClass;
            NewLicense._LicenseDTO.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClassIfo.LicenseClassDTO.DefaultValidityLength;

            NewLicense._LicenseDTO.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense._LicenseDTO. Notes = Notes;
            NewLicense._LicenseDTO.PaidFees = this.LicenseClassIfo.LicenseClassDTO.ClassFees;
            NewLicense._LicenseDTO.IsActive = true;
            NewLicense._LicenseDTO.IssueReason = (int)clsLicense.enIssueReason.Renew;
            NewLicense._LicenseDTO. CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }
        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {


            //First Create Applicaiton 
            clsApplication Application = new clsApplication(new clsApplicationDTO(),clsApplication.enMode.AddNew);

            Application.ApplicatDTO.ApplicationPersonID = this.DriverInfo.DriverDTO.PersonID;
            Application.ApplicatDTO.ApplicationDate = DateTime.Now;

            Application.ApplicatDTO.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicatDTO.ApplicationStatus = (byte)clsApplication.enApplicationStatus.Completed;
            Application.ApplicatDTO .LastStatusDate = DateTime.Now;
            Application.ApplicatDTO.PaidFees = clsApplicationType.FindApplicationTypeByID(Application.ApplicatDTO.ApplicationTypeID).ApplicationTypeDTO.ApplicationTypeFees;
            Application.ApplicatDTO.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense(new clsLicenseDTO(),clsLicense.enMode.AddNew );

            NewLicense._LicenseDTO.ApplicationID = Application.ApplicationID;
            NewLicense._LicenseDTO.DriverID = this._LicenseDTO.DriverID;
            NewLicense._LicenseDTO.LicenseClass = this._LicenseDTO.LicenseClass;
            NewLicense._LicenseDTO.IssueDate = DateTime.Now;
            NewLicense._LicenseDTO.ExpirationDate = this._LicenseDTO.ExpirationDate;
            NewLicense._LicenseDTO.Notes = this._LicenseDTO.Notes;
            NewLicense._LicenseDTO.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense._LicenseDTO.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense._LicenseDTO.CreatedByUserID = CreatedByUserID;



            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }

    }

}

