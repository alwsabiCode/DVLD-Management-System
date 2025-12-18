using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsPersonDTO
    {
        public int PersonID { get; set; }
        public string NationalNo{ get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CountryName { get; set; }

        private string _ImagePath;
        public string ImagePath
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                _ImagePath = value;
            }
        }
        public int NationalityCountryID { get; set; }

        public clsPersonDTO()
        {
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.MinValue;
            Gendor = 0;
            Address = "";
            CountryName = "";
            Phone = "";
            Email = "";
            ImagePath = "";

        }
        public clsPersonDTO(int personID,string nationalNo,string firstName,
            string secondName,string thirdName,string lastName,DateTime dateOfBirth,
            short gendor,string address,string countryName,int nationalityCountryID,string phone,string email, string imagePath)
        {
            PersonID = personID;
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gendor = gendor;
            Address = address;
            CountryName = countryName;
            NationalityCountryID = nationalityCountryID;
            Phone = phone;
            Email = email;
            ImagePath = imagePath;

        }

    }

}
