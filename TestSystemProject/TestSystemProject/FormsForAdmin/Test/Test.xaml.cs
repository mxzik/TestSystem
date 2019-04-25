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

namespace TestSystemProject.FormsForAdmin.Test
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        private readonly User _user;

        private readonly TestService _testService;

        public Test(User user)
        {
            _user = user;

            InitializeComponent();

            _testService = new TestService();

            dgTest.ItemsSource = _testService.GetAll().ToList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Index indexForm = new Index(_user);
            indexForm.Show();
            Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Entities.Test)dgTest.SelectedItem;

            if (selectedItem != null)
            {
                Update updateForm = new Update(selectedItem);
                updateForm.ShowDialog();

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите объект их таблицы!");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Entities.Test)dgTest.SelectedItem;

            if (selectedItem != null)
            {
                _testService.Delete(selectedItem);

                MessageBox.Show("Объект успешо удалён!");

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите объект их таблицы!");
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Create createForm = new Create();
            createForm.ShowDialog();

            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            dgTest.ItemsSource = null;
            dgTest.ItemsSource = _testService.GetAll().ToList();
            dgTest.Items.Refresh();
        }
    }
}
