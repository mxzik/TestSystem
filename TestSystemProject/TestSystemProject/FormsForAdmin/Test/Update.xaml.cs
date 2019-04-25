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

namespace TestSystemProject.FormsForAdmin.Test
{
    /// <summary>
    /// Логика взаимодействия для Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        private readonly Entities.Test _test;

        private readonly ThemeService _themeService;
        private readonly TestService _testService;

        public Update(Entities.Test test)
        {
            _test = test;

            InitializeComponent();

            _testService = new TestService();
            _themeService = new ThemeService();

            txbTheme.ItemsSource = _themeService.GetAll().Select(x => x.Name).ToArray();

            txbTest.Text = _test.Name;
            txbTheme.Text = _themeService.GetById(_test.ThemeId).Name;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txbTest.Text != string.Empty && txbTheme.Text != string.Empty)
            {
                var theme = _themeService.GetByName(txbTheme.Text);

                _test.Name = txbTest.Text;
                _test.ThemeId = theme.ThemeId;

                _testService.Update(_test);

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
