using DEPO_Computers_DataBase.CSV;
using DEPO_Computers_DataBase.Data;
using DEPO_Computers_DataBase.Models;
using Microsoft.Win32;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

    
namespace DEPO_Computers_DataBase
{
    public partial class MainWindow : Window
    {
        private DataBase DataBase=new DataBase();
        private Repository Repository;
        private ObservableCollection<Employee> Employees;
        private ObservableCollection<Company> Companys;
        private Company _scompany;
        private Employee _semployee;
        private ObservableCollection<Employee> CompanyEmploees=new ObservableCollection<Employee>();
        private Company SCompany
        {
            get
            {
                return _scompany;
            }
            set
            {
                _scompany = value;
                LoadDataToCompanyDataView(value);
                LoadCompanysEmploeeys();
            }
        }
        private Employee SEmployee
        {
            get
            {
                return _semployee;
            }
            set
            {
                _semployee = value;
                LoadDataToEmployeeDataView(value);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Repository = new Repository(DataBase);
            Employees = new ObservableCollection<Employee>(DataBase.Employees.ToList());
            Companys = new ObservableCollection<Company>(DataBase.Companys.ToList());
            EmployeeListCB.ItemsSource = Employees;
            CompanyListCB.ItemsSource = Companys;
            Closing += Window_Closing;
            CompanysEmploeesLB.ItemsSource = CompanyEmploees;
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Сохранить изменения?",
                     "Внимание!",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                {
                    DataBase.SaveChanges();
                }
            }
        }
        private void CompanyListCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SCompany = (Company)CompanyListCB.SelectedItem;
        }
        private void EmployeeListListCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SEmployee = (Employee)EmployeeListCB.SelectedItem;
        }
        private void LoadDataToCompanyDataView(Company sCompany)
        {
            CompanyData.DataContext = sCompany;
        }
        private void LoadCompanysEmploeeys()
        {
            CompanyEmploees = new ObservableCollection<Employee>(Employees.Where(u => u.CompanyId == SCompany.ID).ToList());
            CompanysEmploeesLB.ItemsSource = CompanyEmploees;
        }
        private void LoadDataToEmployeeDataView(Employee sEmployee)
        {
            EmployeeData.DataContext = sEmployee;
        }
        private void ExportToCSVButton_Click(object sender, RoutedEventArgs e)
        {
            string? filePath = GetExportFilePath();
            if (string.IsNullOrEmpty(filePath))
                return;
            bool status = Repository.SaveToCSV(filePath);
            if (status) MessageBox.Show("Данные успешно экспортированы в CSV файл.");
            else MessageBox.Show("Ошибка экспорта в CSV файл.");
        }

        private void ImportFromCSVButton_Click(object sender, RoutedEventArgs e)
        {
            string? filePath = GetImportFilePath();
            if (string.IsNullOrEmpty(filePath))
                return;
            bool status = Repository.GetFromCSV(filePath);
            if (status) MessageBox.Show("Данные успешно импортированы из CSV файла.");
            else MessageBox.Show("Ошибка импорта из CSV файла.");
        }

        private string? GetExportFilePath()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV файл (*.csv)|*.csv";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Title = "Выберите файл CSV для экспорта";
            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }
            return null; 
        }
        private string? GetImportFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV файл (*.csv)|*.csv";
            openFileDialog.Title = "Выберите файл CSV для импорта";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null; 
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextBox text_box = (TextBox)sender;
            //if(string.IsNullOrEmpty(text_box.Text))
            //{
            //    text_box.Text = text_box.Name.ToString();
            //}
        }
        private Company GetCompanyFromTextBoxes()
        {
            Company company = new Company()
            {
                Name = CompanyName.Text,
                ActualAddress = CompanyActualAddress.Text,
                LegalAddress = CompanyLegalAddress.Text,
                TIN = CompanyTIN.Text
            };
            return company;
        } 
        private Employee GetEmployeeFromTextBoxes()
        {
            Employee employee = new Employee()
            {
                FirstName = EmployeeFirstName.Text,
                LastName = EmployeeLastName.Text,
                PassportSerial = EmployeePassportSerial.Text,
                PassportNumber = EmployeePassportNumber.Text

            };
            return employee;
        }
        //----
        //Crud
        //----
        private void CreateCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            EditCompanyWindow editCompanyWindow = new EditCompanyWindow();
            editCompanyWindow.Owner = this;
            if( editCompanyWindow.ShowDialog()==true)
            {
                try
                { Companys.Add(editCompanyWindow.Company);
                DataBase.Companys.Add(editCompanyWindow.Company);
                }
                catch {
                    MessageBox.Show("Данные не добавлены", "Ошибка!",
                        MessageBoxButton.OK);
                }
            }

            
            //DataBase.SaveChanges();
        }

        private void UpdateCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            SCompany.Name = CompanyName.Text;
            SCompany.ActualAddress = CompanyActualAddress.Text;
            SCompany.LegalAddress = CompanyLegalAddress.Text;
            SCompany.TIN = CompanyTIN.Text;
            //DataBase.SaveChanges();
        }

        private void DeleteCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            DataBase.Remove(SCompany);
            Companys.Remove(SCompany);
            //DataBase.SaveChanges();
        }

        private void CreateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 EditEmployeeWindow = new() { Owner=this};
            if (EditEmployeeWindow.ShowDialog() == true)
            {
                try
                {
                    Employees.Add(EditEmployeeWindow.Employee);
                    DataBase.Employees.Add(EditEmployeeWindow.Employee);
                }
                catch
                {
                    MessageBox.Show("Данные не добавлены", "Ошибка!",
                        MessageBoxButton.OK);
                }
            }
          
            //DataBase.SaveChanges();
        }
        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            SEmployee.FirstName = EmployeeFirstName.Text;
            SEmployee.LastName = EmployeeLastName.Text;
            SEmployee.PassportSerial = EmployeePassportSerial.Text;
            SEmployee.PassportNumber = EmployeePassportNumber.Text;
            //DataBase.SaveChanges();
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            DataBase.Remove(SEmployee);
            Employees.Remove(SEmployee);
            //DataBase.SaveChanges();
        }
        private void AddEmploeesToCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            if (SEmployee.Company != null)
            {
                if (MessageBox.Show("Изменить компанию?",
                     "Сотрудник уже состоит в компании",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    {
                        SEmployee.Company = SCompany;
                        EmployeeCompany.Text = SCompany.Name;
                    }
                }
            }
            else {
                SEmployee.Company = SCompany;
                EmployeeCompany.Text = SCompany.Name;
            }
        }
    }
}