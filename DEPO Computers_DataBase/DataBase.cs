using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace DEPO_Computers_DataBase
{

    public class DataBase : DbContext
    {
        private DbSet<Company> companys => Set<Company>();
        private DbSet<Employee> employees => Set<Employee>();

        public DataBase()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DbContextOptionsBuilder optionsBuilder1 = optionsBuilder.UseMySql("server=localhost;user=root;password=123123;database=depoComputers;", new MySqlServerVersion(new Version(8, 3, 0    )));
            if (optionsBuilder1 == null) Debug.WriteLine("Connection error");
            else Debug.WriteLine("Connected");
        }
        public void AddCompany(Company company) { companys.Add(company); SaveChanges(); }
        public void RemoveCompany(Company company) { companys.Remove(company); SaveChanges(); }
        public List<Company> GetCompanyList() { return companys.ToList(); }
        public void AddEmployee(Employee employee) { employees.Add(employee); SaveChanges(); }
        public void RemoveEmployee(Employee employee) { employees.Remove(employee); SaveChanges(); }
        public List<Employee> GetEmployeeList() { return employees.ToList(); }
    }
}
