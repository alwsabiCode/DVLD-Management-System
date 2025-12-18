using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsTestAppointmentViewDTO
    {
        public int TestAppointmentID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public string TestTypeTitle { get; set; }
        public string ClassName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public string FullName { get; set; }
        public bool IsLocked { get; set; }


        public clsTestAppointmentViewDTO(int testAppointmentID, int localDrivingLicenseApplicationID, string testTypeTitle, string className, DateTime appointmentDate, decimal paidFees, string fullName, bool isLocked)
        {
            TestAppointmentID = testAppointmentID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            TestTypeTitle = testTypeTitle;
            ClassName = className;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            FullName = fullName;
            IsLocked = isLocked;
        }
    }
}
