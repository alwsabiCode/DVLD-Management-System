using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsTestAppointmentsPerTestTypeViewDTO
    {
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PiadFees { get; set; }
        public bool IsLocked { get; set; }


        public clsTestAppointmentsPerTestTypeViewDTO(int testAppointmentID, DateTime appointmentDate, decimal piadFees, bool isLocked)
        {
            TestAppointmentID = testAppointmentID;
            AppointmentDate = appointmentDate;
            PiadFees = piadFees;
            IsLocked = isLocked;
        }
    }
}
