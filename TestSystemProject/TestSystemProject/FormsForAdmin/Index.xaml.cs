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
            Theme.Theme themeForm = new Theme.Theme(_user);
            themeForm.Show();
            Close();
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            Test.Test testForm = new Test.Test(_user);
            testForm.Show();
            Close();
        }

        private void BtnQuestion_Click(object sender, RoutedEventArgs e)
        {
            Question.Question questionForm = new Question.Question(_user);
            questionForm.Show();
            Close();
        }

        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {
            Answer.Answer answerForm = new Answer.Answer(_user);
            answerForm.Show();
            Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
