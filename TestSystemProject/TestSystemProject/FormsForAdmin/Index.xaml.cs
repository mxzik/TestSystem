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

namespace TestSystemProject.FormsForAdmin
{
    /// <summary>
    /// Логика взаимодействия для Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        private User _user;

        public Index(User user)
        {
            _user = user;

            InitializeComponent();
        }

        private void BtnTheme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
