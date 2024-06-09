using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPO_Computers_DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Options;
namespace DEPO_Computers_DataBase.Data
{

    public class DataBase : DbContext
    {
        public DbSet<Company> Companys { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        public DataBase()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DbContextOptionsBuilder optionsBuilder1 = optionsBuilder.UseMySql("server=localhost;user=root;password=123123;database=depoComputers;",
                new MySqlServerVersion(new Version(8, 3, 0)));
            if (optionsBuilder1 == null) Debug.WriteLine("Connection error");
            else Debug.WriteLine("Connected");
        }
    }
}
