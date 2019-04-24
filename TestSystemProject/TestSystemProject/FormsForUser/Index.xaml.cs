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

namespace TestSystemProject.FormsForUser
{
    /// <summary>
    /// Логика взаимодействия для Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        private readonly User _user;

        public Index(User user)
        {
            _user = user;

            InitializeComponent();
        }
    }
}
