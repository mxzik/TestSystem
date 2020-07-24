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
using TestSystemProject.Entities;
using TestSystemProject.Logic.Services;

namespace TestSystemProject.FormsForUser
{
    /// <summary>
    /// Логика взаимодействия для Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        private readonly User _user;

        private readonly ResultService _resultService;
        public Results(User user)
        {
            _user = user;

            InitializeComponent();

            _resultService = new ResultService();

            dgResults.ItemsSource = _resultService.GetAll().ToList();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Index indexForm = new Index(_user);
            indexForm.Show();
            Close();
        }
        private void RefreshDataGrid()
        {
            dgResults.ItemsSource = null;
            dgResults.ItemsSource = _resultService.GetAll().ToList();
            dgResults.Items.Refresh();
        }
    }
}
