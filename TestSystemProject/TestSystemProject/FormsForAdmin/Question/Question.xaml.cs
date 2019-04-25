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

namespace TestSystemProject.FormsForAdmin.Question
{
    /// <summary>
    /// Логика взаимодействия для Question.xaml
    /// </summary>
    public partial class Question : Window
    {
        private readonly User _user;

        private readonly QuestionService _questionService;

        public Question(User user)
        {
            _user = user;

            InitializeComponent();

            _questionService = new QuestionService();

            dgQuestion.ItemsSource = _questionService.GetAll().ToList();
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

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Entities.Question)dgQuestion.SelectedItem;

            if(selectedItem != null)
            {
                Update updateForm = new Update(selectedItem);
                updateForm.ShowDialog();

                RefreshDataGrid();

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите объект из таблицы!");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Entities.Question)dgQuestion.SelectedItem;

            if (selectedItem != null)
            {
                _questionService.Delete(selectedItem);

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
            dgQuestion.ItemsSource = null;
            dgQuestion.ItemsSource = _questionService.GetAll().ToList();
            dgQuestion.Items.Refresh();
        }
    }
}
