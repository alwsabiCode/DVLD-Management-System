using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;
using DataAccess_DVLD;

namespace Business_DVLD
{
    public class clsTestTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        private clsTestTypeDTO _TTDTO;
        public clsTestTypeDTO testTypeDTO
        {
            get { return _TTDTO; }
        }
        public enTestType ID { get; set; }
      
        public clsTestTypes(clsTestTypeDTO TTDTO, enMode eMode = enMode.AddNew)
        {
            _TTDTO = TTDTO ?? new clsTestTypeDTO();
            this.Mode = eMode;
            this.ID = (enTestType)_TTDTO.ID;
        }
        private bool _AddNewTestType()
        {
            this.ID= (enTestType)clsTestTypeData.AddNewTestType(testTypeDTO);
            return ( this._TTDTO.Title != "");
        }

        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType(testTypeDTO);
        }
        public  static clsTestTypes FindTestTypeByID(enTestType testTypeID)
        {
            clsTestTypeDTO typeDTO = clsTestTypeData.FindTestTypeByID((int)testTypeID);
            if (typeDTO != null)
            {
                return new clsTestTypes(typeDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static List<clsTestTypeDTO> GetAllTestTyepe()
        {
            return clsTestTypeData.GetAllTestType();
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestType();
            }
            return false;
        }

    }

}

