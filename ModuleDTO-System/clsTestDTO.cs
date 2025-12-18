using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsTestDTO
    {
        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public bool TestResult { set; get; }
        public string Note { set; get; }
        public int CreatedByUerID { set; get; }

        public clsTestDTO()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Note = "";
            CreatedByUerID = -1;
        }
       public clsTestDTO(int testID, int testAppointmentID, bool testResult, string note, int createdByUerID)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestResult = testResult;
            Note = note;
            CreatedByUerID = createdByUerID;
        }


    }
}
