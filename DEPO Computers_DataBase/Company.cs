using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPO_Computers_DataBase
{
    public class Company
    {
        public int ID {  get; set; }
        public string Name { get; set; } = "Компания";
        public string TIN { get; set; } = "ИНН";//ИНН
        public string LegalAddress { get; set; } = "Улица Название 0, г. Город ";
        public string ActualAddress { get; set; }= "Улица Название 0, г.Город ";

        public Company() { }
    }
}
