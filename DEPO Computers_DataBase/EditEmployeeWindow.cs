using DEPO_Computers_DataBase.Models;
using DEPO_Computers_DataBase.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DEPO_Computers_DataBase
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        
        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            Employee= employee;
            EmployeeData.DataContext = Employee;

        }
        public Employee Employee { get; internal set; } = new Employee();
        private void SaveNewEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validations.Validations.ValidatePassportNumber(Employee.PassportSerial + "-"+ Employee.PassportNumber))
            {
                DialogResult = true;
                Close();
            }
            else
                MessageBox.Show("Паспорт не валиден", "Предупреждение!",
                    MessageBoxButton.OK);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
