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

namespace TestSystemProject.FormsForAdmin.Theme
{
    /// <summary>
    /// Логика взаимодействия для Theme.xaml
    /// </summary>
    public partial class Theme : Window
    {
        private readonly User _user;

        private readonly ThemeService _themeService;

        //когда форма загружается 
        public Theme(User user)
        {
            _user = user;

            InitializeComponent();

            _themeService = new ThemeService();

            dgTheme.ItemsSource = _themeService.GetAll().ToList();
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
            var selectedItem = (Entities.Theme)dgTheme.SelectedItem;

            if(selectedItem != null)
            {
                Update updateForm = new Update(selectedItem);
                updateForm.ShowDialog();

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите объект из таблицы!");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Entities.Theme)dgTheme.SelectedItem;

            if (selectedItem != null)
            {
                _themeService.Delete(selectedItem);

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
            dgTheme.ItemsSource = null;
            dgTheme.ItemsSource = _themeService.GetAll().ToList();
            dgTheme.Items.Refresh();
        }
    }
}
