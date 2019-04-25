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
using TestSystemProject.Logic.Services;

namespace TestSystemProject.FormsForAdmin.Theme
{
    /// <summary>
    /// Логика взаимодействия для Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        private readonly ThemeService _themeService;

        public Create()
        {
            InitializeComponent();

            _themeService = new ThemeService();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(txbTheme.Text != string.Empty && txbDescription.Text != string.Empty)
            {
                _themeService.Create(new Entities.Theme { Name = txbTheme.Text, Description = txbDescription.Text });

                MessageBox.Show("Объект успешно создан!");

                Close();
            }
            else
            {
                MessageBox.Show("Заполните все ключевые поля!");
            }
        }
    }
}
