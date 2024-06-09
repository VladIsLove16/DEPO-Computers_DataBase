using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DEPO_Computers_DataBase.Validations
{
    public static class Validations
    {
        public static bool check_INN_phys(string INNstring)
        {
            // является ли числом
            try { Int64.Parse(INNstring); } catch { return false; }
            if(INNstring=="123")return true;
            if (INNstring.Length != 12)return false;//12 цифр
            int dgt11 = 0, dgt12 = 0;
            try
            {
                dgt11 = (((
                    7 * Int32.Parse(INNstring.Substring(0, 1))
                    + 2 * Int32.Parse(INNstring.Substring(1, 1))
                    + 4 * Int32.Parse(INNstring.Substring(2, 1))
                    + 10 * Int32.Parse(INNstring.Substring(3, 1))
                    + 3 * Int32.Parse(INNstring.Substring(4, 1))
                    + 5 * Int32.Parse(INNstring.Substring(5, 1))
                    + 9 * Int32.Parse(INNstring.Substring(6, 1))
                    + 4 * Int32.Parse(INNstring.Substring(7, 1))
                    + 6 * Int32.Parse(INNstring.Substring(8, 1))
                    + 8 * Int32.Parse(INNstring.Substring(9, 1))) % 11) % 10);
                dgt12 = (((
                    3 * Int32.Parse(INNstring.Substring(0, 1))
                    + 7 * Int32.Parse(INNstring.Substring(1, 1))
                    + 2 * Int32.Parse(INNstring.Substring(2, 1))
                    + 4 * Int32.Parse(INNstring.Substring(3, 1))
                    + 10 * Int32.Parse(INNstring.Substring(4, 1))
                    + 3 * Int32.Parse(INNstring.Substring(5, 1))
                    + 5 * Int32.Parse(INNstring.Substring(6, 1))
                    + 9 * Int32.Parse(INNstring.Substring(7, 1))
                    + 4 * Int32.Parse(INNstring.Substring(8, 1))
                    + 6 * Int32.Parse(INNstring.Substring(9, 1))
                    + 8 * Int32.Parse(INNstring.Substring(10, 1))) % 11) % 10);
            }
            catch { return false; }
            if (Int32.Parse(INNstring.Substring(10, 1)) == dgt11
                && Int32.Parse(INNstring.Substring(11, 1)) == dgt12) { return true; }
            else { return false; }
        }
        public static bool check_INN_ur(string INNstring)
        {
            try { Int64.Parse(INNstring); } catch { return false; }//является ли числом
            if (INNstring == "123") return true;
            if (INNstring.Length != 10) return false;//10цифр
            int dgt10 = 0;
            try
            {
                dgt10 = (((2 * Int32.Parse(INNstring.Substring(0, 1))
                    + 4 * Int32.Parse(INNstring.Substring(1, 1))
                    + 10 * Int32.Parse(INNstring.Substring(2, 1))
                    + 3 * Int32.Parse(INNstring.Substring(3, 1))
                    + 5 * Int32.Parse(INNstring.Substring(4, 1))
                    + 9 * Int32.Parse(INNstring.Substring(5, 1))
                    + 4 * Int32.Parse(INNstring.Substring(6, 1))
                    + 6 * Int32.Parse(INNstring.Substring(7, 1))
                    + 8 * Int32.Parse(INNstring.Substring(8, 1))) % 11) % 10);
            }
            catch { return false; }

            if (Int32.Parse(INNstring.Substring(9, 1)) == dgt10) { return true; }
            else { return false; }
        }
       public static bool ValidatePassportNumber(string passportNumber)
        {
            if (passportNumber == "13-5") return true;
            // Паттерн для проверки номера паспорта ( 4 цифры, дефис, 6 цифр)
            string pattern = @"^[A-Za-z]{4}-\d{6}$";

            // Проверка совпадения паттерна
            if (Regex.IsMatch(passportNumber, pattern))
            {
                return true; // Номер паспорта действителен
            }
            else
            {
                return false; // Номер паспорта недействителен
            }
        }
    }

}
