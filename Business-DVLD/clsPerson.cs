using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleDTO_DVLD;
using DataAccess_DVLD;


namespace Business_DVLD
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode= enMode.AddNew;

        public int PersonID { get; set; }

        public clsCountry CountryInfo;

        private clsPersonDTO _PDTO;        
        public clsPersonDTO PDTO
        {
            get { return _PDTO; }
        }
        
        public clsPerson(clsPersonDTO PDTO , enMode eMode=enMode.AddNew)
        {
            _PDTO = PDTO ?? new clsPersonDTO(); 
            this.Mode = eMode;
            this.PersonID=_PDTO.PersonID;
            this.CountryInfo= clsCountry.Find(_PDTO.NationalityCountryID);
        }
       
        public static clsPerson Find(int PersonID)
        {
            clsPersonDTO personDTO = clsPersonData.GetPersonByID(PersonID);

            if (personDTO != null)
            {
                return new clsPerson(personDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static clsPerson Find(string NationalNo)
        {
            clsPersonDTO nationalNoDTO = clsPersonData.GetPersonByNationalNo(NationalNo);

            if (nationalNoDTO != null)
            {
                return new clsPerson(nationalNoDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static List<clsPersonDTO> GetAllPeople()
        {
            return  clsPersonData.GetAllPeople();
        }
                
        
        private bool _AddNewPerson()
        {
            this.PersonID= clsPersonData.AddNewPerson(PDTO);
            return (this.PersonID != -1);
        }
        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdatePerson();
                     
            }
            return false;
        }
        public static bool DeletePerson(int Id)
        {
            return clsPersonData.DeletePerson(Id);
        }
        public static bool isPersonExist(string nationalNo)
        {
            return clsPersonData.isPersonExist(nationalNo);
        }
    }
}
