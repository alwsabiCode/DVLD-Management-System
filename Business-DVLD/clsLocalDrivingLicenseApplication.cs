using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_DVLD;
using System.Runtime.CompilerServices;


namespace Business_DVLD
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }

        public clsLicenseClass LicenseClassInfo;
        public string PersonFullName
        {
            get
            {
                return base.PersonInfo.PDTO.FullName;
            }
        }

        private clsLocalDrivingLicenseApplicationDTO _LocalDTO;
        public clsLocalDrivingLicenseApplicationDTO LocalDTO
        {
            get
            {
                return _LocalDTO;
            }
        }
        public clsLocalDrivingLicenseApplication(clsLocalDrivingLicenseApplicationDTO localDTO, clsApplicationDTO ADTO, enMode eMode = enMode.AddNew)
            : base(ADTO, (clsApplication.enMode)eMode)
        {
            _LocalDTO = localDTO ?? new clsLocalDrivingLicenseApplicationDTO();
            Mode = eMode;
            this.LocalDrivingLicenseApplicationID = _LocalDTO.LocalDrivingLicenseApplicationID;
            LicenseClassInfo = clsLicenseClass.Find(_LocalDTO.LicenseClassID);



        }
        public static clsLocalDrivingLicenseApplication FindLocalDriving(int LocalDrivingLicense)
        {
            clsLocalDrivingLicenseApplicationDTO LocalDTO = clsLocalDrivingLicenseApplicationData.FindLocalDrivingID(LocalDrivingLicense);
            if (LocalDTO != null)
            {
                clsApplicationDTO ADTO = clsApplicationData.FindBaseApplication(LocalDTO.ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDTO, ADTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static clsLocalDrivingLicenseApplication FindApplicationID(int ApplicationID)
        {
            clsLocalDrivingLicenseApplicationDTO LocalDTO = clsLocalDrivingLicenseApplicationData.FindApplicationID(ApplicationID);
            if (LocalDTO != null)
            {
                clsApplicationDTO ADTO = clsApplicationData.FindBaseApplication(LocalDTO.ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDTO, ADTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewLocalDriving()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDriving(_LocalDTO);
            return (this.LocalDrivingLicenseApplicationID != -1);


        }
        private bool _UpdateLocalDriving()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDriving(_LocalDTO);
        }
        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
            {
                return false;
            }
             LocalDTO.ApplicationID=base.ApplicationID;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDriving())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLocalDriving();

            }
            return false;
        }
        public static List<clsLocalDrivingLicenseApplicationViewDTO> GetAllLocalDriving()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDriving();
        }
        public bool Delete()
        {
            bool IsLocalDrivingApplicationDelete = false;
            bool IsBaseApplicationDelete = false;
            IsLocalDrivingApplicationDelete = clsLocalDrivingLicenseApplicationData.DeleteLocalDriving(this.LocalDrivingLicenseApplicationID);
            if (!IsLocalDrivingApplicationDelete)
            {
                return false;
            }
            IsBaseApplicationDelete = base.Delete();
            return IsBaseApplicationDelete;
        }
        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicatDTO.ApplicationPersonID, this._LocalDTO.LicenseClassID);
        }
        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public bool DoesPassTestType(clsTestTypes.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)testTypeID);

        }
        public bool DoesPassPreviousTest(clsTestTypes.enTestType CurrentTestType)
        {
            switch (CurrentTestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    return true;
                case clsTestTypes.enTestType.WrittenTest:
                    return this.DoesPassTestType(clsTestTypes.enTestType.VisionTest);
                case clsTestTypes.enTestType.StreetTest:
                    return this.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
                default:
                    return false;
            }

        }
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)testTypeID);

        }
        public bool DoesAttendTestType(clsTestTypes.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)testTypeID);


        }
        public byte TotalTrialsPerTest(clsTestTypes.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)testTypeID);

        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)testTypeID);

        }
        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public clsTest GetLastTestPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicatDTO.ApplicationPersonID, this._LocalDTO.LicenseClassID, TestTypeID);
        }
        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }
        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicatDTO.ApplicationPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver(new clsDriverDTO(),clsDriver.enMode.AddNew);

                Driver.DriverDTO.DriverID = this.ApplicatDTO.ApplicationPersonID;
                Driver.DriverDTO.DriverID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense(new clsLicenseDTO(),clsLicense.enMode.AddNew);
            License.licenseDTO.ApplicationID = this.ApplicationID;
            License.licenseDTO.DriverID = DriverID;
            License.licenseDTO.LicenseClass = this.LocalDTO.LicenseClassID;
            License.licenseDTO.IssueDate = DateTime.Now;
            License.licenseDTO.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.LicenseClassDTO.DefaultValidityLength);
            License.licenseDTO.Notes = Notes;
            License.licenseDTO.PaidFees = this.LicenseClassInfo.LicenseClassDTO.ClassFees;
            License.licenseDTO.IsActive = true;
            License.licenseDTO.IssueReason = (int)clsLicense.enIssueReason.FirstTime;
            License.licenseDTO.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

    }
}
