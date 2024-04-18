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
        private DataBase dataBase;
        string csvDatabaseFilePath = Directory.GetCurrentDirectory().ToString() + "\\database.csv";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CreateCompany(object sender, RoutedEventArgs e)
        {
            Company company = new Company()
            {
                Name = CompanyName.Text,
                ActualAddress = CompanyActualAddress.Text,
                LegalAddress = CompanyLegalAddress.Text,
                TIN = CompanyTIN.Text
            };
            dataBase.AddCompany(company);
        }

        private void CreateEmployee(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee()
            {
                FirstName = EmployeeFirstName.Text,
                LastName = EmployeeLastName.Text,
                PassportSerial = EmployeePassportSerial.Text,
                PassportNumber = EmployeePassportNumber.Text

            };
            dataBase.AddEmployee(employee);
        }
    }
}