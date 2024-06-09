using DEPO_Computers_DataBase.Models;
using DEPO_Computers_DataBase.Validations;
using Microsoft.IdentityModel.Tokens;
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
    public partial class EditCompanyWindow : Window
    {
        public EditCompanyWindow()
        {
            InitializeComponent();
            CompanyData.DataContext = Company;
        }

        public Company Company { get; internal set; } = new Company();

        private void SaveNewCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            if (INNValidation.check_INN_ur(Company.TIN))
            {
                DialogResult = true;
                Close();
            }
            else
                MessageBox.Show("Инн не валиден", "Предупреждение!", 
                    MessageBoxButton.OK);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
