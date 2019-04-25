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
    /// Логика взаимодействия для Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        private readonly Entities.Theme _theme;
        private readonly ThemeService _themeService;


        public Update(Entities.Theme theme)
        {
            _theme = theme;

            InitializeComponent();

            txbTheme.Text = _theme.Name;
            txbDescription.Text = _theme.Description;

            _themeService = new ThemeService();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txbTheme.Text != string.Empty && txbDescription.Text != string.Empty)
            {
                _theme.Name = txbTheme.Text;
                _theme.Description = txbDescription.Text;

                _themeService.Update(_theme);

                MessageBox.Show("Объект успешно изменён!");

                Close();
            }
            else
            {
                MessageBox.Show("Заполните все ключевые поля!");
            }
        }
    }
}
