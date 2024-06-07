using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPO_Computers_DataBase
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = "Фамилия";
        public string LastName { get; set; } = "Имя";
        public string BirthDay { get; set; } = "День Рождения";
        public string PassportSerial { get; set; } = "123456";
        public string PassportNumber { get; set; } = "1234";
        public Company Company { get; set; }
        public Employee() { }   

    }
}
