using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DEPO_Computers_DataBase.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; } = "Компания";
        public string TIN { get; set; } = "ИНН";//ИНН
        public string LegalAddress { get; set; } = "Улица Название 0, г. Город ";
        public string ActualAddress { get; set; } = "Улица Название 0, г.Город ";
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public Company() { }
    }
}
