using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_DVLD;

namespace Business_DVLD
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { get; set; }

        private clsTestDTO _TDTO;
        public clsTestDTO testDTO
        {
            get
            {
                return _TDTO;
            }
        }
         public clsTestAppointment TestAppointmentInfo { set; get; }
        public clsTest(clsTestDTO TDTO, enMode eMode = enMode.AddNew)
        {
            _TDTO = TDTO ?? new clsTestDTO();
            this.TestID = _TDTO.TestID;
            this.TestAppointmentInfo = clsTestAppointment.FindTestAppointmentByID(_TDTO.TestAppointmentID);


        }
        public static clsTest Find(int TestID)
        {
            clsTestDTO testDTO = clsTestData.FindTestByTestID(TestID);
            if (testDTO != null)
            {
                return new clsTest(testDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static clsTest FindLastTestPerPersonAndLicenseClass(int PersonID, int LicenseClassID, clsTestTypes.enTestType TestTypeID)
        {
            clsTestDTO testDTO = clsTestData.FindLastTestPerPersonAndLicenseClass(PersonID, LicenseClassID, (int)TestTypeID);
            if (testDTO != null)
            {
                return new clsTest(testDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(_TDTO);
            return (this.TestID != -1);

        }
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(_TDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }
        public static List<clsTestDTO> GetAllTest()
        {
            return clsTestData.GetAllTest();
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }
    }
}
