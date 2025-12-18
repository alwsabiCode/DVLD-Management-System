using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;
using DataAccess_DVLD;

namespace Business_DVLD
{
    public class clsApplicationType
    {
        public enum enMode {AddNew=0,Update=1 }
        public enMode Mode = enMode.AddNew;
        public int ApplicationTypeID { set;get; }

        private clsApplicationTypeDTO _ADTO;
        public clsApplicationTypeDTO ApplicationTypeDTO
        {
            get { return _ADTO; }
            
        }
        public clsApplicationType(clsApplicationTypeDTO ADTO,enMode eMode=enMode.AddNew) 
        {
            _ADTO = ADTO ?? new clsApplicationTypeDTO();
            this.Mode = eMode;
            this.ApplicationTypeID = _ADTO.ApplicationTypeID;
        }
        public static clsApplicationType FindApplicationTypeByID(int ID)
        {
            clsApplicationTypeDTO atDTO = clsApplicationTypeData.FindApplicationTypeByID(ID);
            if (atDTO != null)
            {
                return new clsApplicationType(atDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public bool _AddNewApplicationType()
        {
            this.ApplicationTypeID = clsApplicationTypeData.AddNewApplicationType(ApplicationTypeDTO);

            return (this.ApplicationTypeID != -1);
        }
        public bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(ApplicationTypeDTO);
        }
        
        public static List<clsApplicationTypeDTO> GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                       Mode=enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                 return _UpdateApplicationType();

            }
            return false;
        }
    }
}
