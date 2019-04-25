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

namespace TestSystemProject.FormsForAdmin.Answer
{
    /// <summary>
    /// Логика взаимодействия для Answer.xaml
    /// </summary>
    public partial class Answer : Window
    {
        private readonly User _user;

        private readonly AnswerService _answerService;

        public Answer(User user)
        {
            _user = user;

            InitializeComponent();

            _answerService = new AnswerService();

            dgAnswer.ItemsSource = _answerService.GetAll().ToList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Index indexForm = new Index(_user);
            indexForm.Show();
            Close();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Create createForm = new Create();
            createForm.ShowDialog();

            RefreshDataGrid();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Entities.Answer)dgAnswer.SelectedItem;

            if(selectedItem != null)
            {
                _answerService.Delete(selectedItem);

                MessageBox.Show("Объект успешно удалён!");

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите объект из таблицы!");
            }
        }

        private void RefreshDataGrid()
        {
            dgAnswer.ItemsSource = null;
            dgAnswer.ItemsSource = _answerService.GetAll().ToList();
            dgAnswer.Items.Refresh();
        }
    }
}
