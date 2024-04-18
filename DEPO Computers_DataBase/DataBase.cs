using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace DEPO_Computers_DataBase
{
    public class DataBase
    {
        private List<Company> companys=new List<Company>();   
        private List<Employee> employees=new List<Employee>();
        public DataBase() { }
        public void AddCompany(Company company) {  companys.Add(company); }
        public void RemoveCompany(Company company) { companys.Remove(company); }
        public List<Company> GetCompanyList() {  return companys; }
        public void AddEmployee(Employee employee) { employees.Add(employee); }
        public void RemoveEmployee(Employee employee) { employees.Remove(employee); }
        public List<Employee> GetEmployeeList() { return employees; }
    }
}
