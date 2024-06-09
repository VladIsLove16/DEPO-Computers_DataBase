using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPO_Computers_DataBase.Data;
using DEPO_Computers_DataBase.Models;

namespace DEPO_Computers_DataBase.CSV
{
    public class Repository
    {
        DataBase database;
        public Repository(DataBase dataBase)
        {
            database = dataBase;
        }
        public bool SaveToCSV(string filepath)
        {
            try
            {
                WriteToCSV(filepath, database.Companys.ToList(), database.Employees.ToList());
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public bool GetFromCSV(string filepath)
        {
            Debug.WriteLine("Загрузка из " + filepath + " в бд");
            var companies = new List<Company>();
            var employees = new List<Employee>();
            try
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line;
                    bool isCompanySection = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }
                        if (line.StartsWith("Компания:"))
                        {
                            isCompanySection = true;
                            continue;
                        }
                        else if (line.StartsWith("Сотрудник:"))
                        {
                            isCompanySection = false;
                            continue;
                        }
                        string[] values = line.Split(',');
                        if (isCompanySection)
                        {
                            companies.Add(new Company
                            {
                                Name = values[0],
                                TIN = values[1],
                                LegalAddress = values[2],
                                ActualAddress = values[3]
                            });
                        }
                        else
                        {
                            employees.Add(new Employee
                            {
                                FirstName = values[0],
                                LastName = values[1],
                                PassportSerial = values[2],
                                PassportNumber = values[3]
                            });
                        }

                    }
                }
                foreach (var company in companies)
                {
                    database.Companys.Add(company);
                }
                foreach (var employee in employees)
                {
                    database.Employees.Add(employee);
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        private void WriteToCSV(string filePath, List<Company> companies, List<Employee> employees)
        {
            //if (!File.Exists(filePath))
            //{
            //    FileStream fs = File.Create(filePath);
            //}
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                if (companies == null || companies.Count == 0) Debug.WriteLine("Список Companies Пуст");
                else WriteCompanies(sw, companies);
                if (employees == null || employees.Count == 0) Debug.WriteLine("Список Employees Пуст");
                else WriteEmploees(sw, employees);
            }
        }
        private void WriteCompanies(StreamWriter sw, List<Company> companies)
        {
            sw.WriteLine("Компания:");
            //sw.WriteLine("Название,ИНН,Адресс Юр., Факт.");
            foreach (var company in companies)
            {
                sw.WriteLine($"{company.Name},{company.TIN},{company.LegalAddress},{company.ActualAddress}");
            }
            sw.WriteLine();
        }
        private void WriteEmploees(StreamWriter sw, List<Employee> employees)
        {
            sw.WriteLine("Сотрудник:");
            //sw.WriteLine("Имя,Фамилия,ПаспортСерия,Номер");
            foreach (var employee in employees)
            {
                sw.WriteLine($"{employee.FirstName},{employee.LastName},{employee.PassportSerial},{employee.PassportNumber}");
            }
        }
    }
}
