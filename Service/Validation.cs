using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service
{
    public static class Validation
    {
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) &&
                   name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            // תבנית לפלאפון ישראלי – מתחיל ב־05 ומכיל 10 ספרות
            return Regex.IsMatch(phone ?? "", @"^05\d{8}$");
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email ?? "", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        public static bool IsValidAddress(string address)
        {
            return !string.IsNullOrWhiteSpace(address) && address.Length >= 5;
        }


    }

}

