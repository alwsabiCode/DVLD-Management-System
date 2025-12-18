using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDTO_DVLD
{
    public class clsTestTypeDTO
    {
        public int ID {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Fees { get; set; }

        public clsTestTypeDTO()
        {
            ID= 1;
            Title = "";
            Description = "";
            Fees = 0;


        }
        public clsTestTypeDTO(int id,string title, string description,decimal fees)
        {
            this.ID = id;
            this.Title = title;
            this.Description = description;
            this.Fees = fees;


        }
    }
}
