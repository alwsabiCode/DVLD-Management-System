using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD_System.Global_Classes
{
    public class clsValidatoin
    {
        public static bool ValidateEmail(string email)
        {
            var pattern = @"[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            var regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        public static bool ValidateInteger(string input)
        {
            var pattern = @"^[0-9]*$";
            var regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        public static bool ValidateFlaot(string input)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";
            var regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        public static bool IsNumber(string input)
        {
            return (ValidateInteger(input) || ValidateFlaot(input));
        }
    }
}
