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
    /// Логика взаимодействия для Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        private readonly ThemeService _themeService;
        private readonly TestService _testService;

        public Create()
        {
            InitializeComponent();

            _testService = new TestService();
            _themeService = new ThemeService();

            txbTheme.ItemsSource = _themeService.GetAll().Select(x => x.Name).ToArray();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txbTest.Text != string.Empty && txbTheme.Text != string.Empty)
            {
                var theme = _themeService.GetByName(txbTheme.Text);

                var test = _testService.GetAll().Where(x => x.ThemeId == theme.ThemeId && x.Name == txbTest.Text).FirstOrDefault();

                if(test == null)
                {
                    _testService.Create(new Entities.Test { Name = txbTest.Text, ThemeId = theme.ThemeId });

                    MessageBox.Show("Объект успешно создан!");

                    Close();
                }
                else
                {
                    MessageBox.Show("Такой тест у этой темы уже есть!");
                }
            }
            else
            {
                MessageBox.Show("Заполните все ключевые поля!");
            }
        }
    }
}
