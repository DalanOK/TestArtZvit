using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClientApp
{
    public static class Validation
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            if (email.Length > 100)
                return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            if (phoneNumber.StartsWith("+"))
            {
                phoneNumber = phoneNumber.Substring(1);
            }

            return phoneNumber.All(char.IsDigit) && phoneNumber.Length <= 20;
        }
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            foreach (char c in name)
            {
                if (!char.IsLetter(c))
                    return false;
            }

            if (name.Length > 30)
                return false;

            return true;
        }
        public static bool IsValidIndefNum(string num)
        {
            if (string.IsNullOrWhiteSpace(num))
                return false;
            if (num.Length != 10)
                return false;

            if (!IsNumeric(num))
            {
                return false; 
            }

            return true;
        }
        public static bool IsNumeric(string value)
        {
            return value.All(char.IsDigit);
        }
    }
}
