using Business_DVLD;
using ModuleDTO_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_DVLD;
namespace Business_DVLD
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestAppointmentID { get; set; }

        private clsTestAppointmentDTO _TADTO;
        public clsTestAppointmentDTO TestAppointmentDTO
        {
            get
            {
                return _TADTO;
            }
        }
        public clsApplication RetakeTestAppInfo { set; get; }
        public int TestID
        {
            get
            {
                return _GetTestID();
            }
        }
        public clsTestAppointment(clsTestAppointmentDTO TADTO, enMode eMode = enMode.AddNew)
        {
            _TADTO = TADTO ?? new clsTestAppointmentDTO();
            this.Mode=eMode;
            this.TestAppointmentID = _TADTO.TestAppointmentID;
            this.RetakeTestAppInfo = clsApplication.FindBaseApplication(_TADTO.RetakeTestApplicationID);

        }
        private bool _AddNewAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(_TADTO);
            return (this.TestAppointmentID != -1);
        }
        private bool _UpdateAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(_TADTO);
        }
        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestIDByAppointmentID(this.TestAppointmentID);
        }
        public bool SaveAppointment()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewAppointment())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        ;

                    }
                case enMode.Update:

                    return _UpdateAppointment();


            }
            return false;
        }
        public static clsTestAppointment FindTestAppointmentByID(int testAppointmentID)
        {
            clsTestAppointmentDTO taDTO = clsTestAppointmentData.FindTestAppointmentByID(testAppointmentID);
            if (taDTO != null)
            {
                return new clsTestAppointment(taDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static clsTestAppointment FindLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            clsTestAppointmentDTO taDTO = clsTestAppointmentData.FindLastTestAppointment(LocalDrivingLicenseApplicationID,(int)TestTypeID);
            if (taDTO != null)
            {
                return new clsTestAppointment(taDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static List<clsTestAppointmentViewDTO> GetAllTestAppointments()
        {
           return clsTestAppointmentData.GetAllTestAppointments();
        }

        public static List<clsTestAppointmentsPerTestTypeViewDTO> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public List<clsTestAppointmentsPerTestTypeViewDTO> GetApplicationTestAppointmentsPerTestType( clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(this._TADTO.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }


    }
}
