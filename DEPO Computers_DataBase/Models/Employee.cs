using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPO_Computers_DataBase.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = "Фамилия";
        public string LastName { get; set; } = "Имя";
        public string PassportSerial { get; set; } = "1234";
        public string PassportNumber { get; set; } = "123456";
        public int? CompanyId { get; set; }
        public Company Company { get; set; } = null;
        public Employee() { }

    }
}
