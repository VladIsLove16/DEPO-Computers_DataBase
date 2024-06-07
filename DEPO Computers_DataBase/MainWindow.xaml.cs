using Microsoft.Win32;
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

namespace DEPO_Computers_DataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataBase DataBase=new DataBase();
        private Repository Repository;
        private List<Company> Companys;
        private List<Employee> Employees;
        public MainWindow()
        {
            InitializeComponent();
            Repository = new Repository(DataBase);
            Companys = DataBase.GetCompanyList();
            Employees = DataBase.GetEmployeeList();
        }
        private void CreateCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            Company company = new Company()
            {
                Name = CompanyName.Text,
                ActualAddress = CompanyActualAddress.Text,
                LegalAddress = CompanyLegalAddress.Text,
                TIN = CompanyTIN.Text
            };
            DataBase.AddCompany(company);
        }

        private void CreateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee()
            {
                FirstName = EmployeeFirstName.Text,
                LastName = EmployeeLastName.Text,
                PassportSerial = EmployeePassportSerial.Text,
                PassportNumber = EmployeePassportNumber.Text

            };
            DataBase.AddEmployee(employee);
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

        }
    }
}