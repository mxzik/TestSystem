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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestSystemProject.Common;
using TestSystemProject.Entities;
using TestSystemProject.Logic.Services;

namespace TestSystemProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserService _userService;

        public MainWindow()
        {
            InitializeComponent();

            _userService = new UserService();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txbRegisterEmail.Text != string.Empty && txbRegisterPassword.Password != string.Empty && txbRegisterPasswordConfirm.Password != string.Empty)
            {
                if (txbRegisterPassword.Password == txbRegisterPasswordConfirm.Password)
                {
                    try
                    {
                        int countUsers = _userService.GetAll().Count();

                        User user = _userService.GetByEmail(txbRegisterEmail.Text);

                        if (countUsers == 0)
                        {
                            user = CreateAdmin(txbRegisterEmail.Text, txbRegisterPassword.Password);

                            _userService.Create(user);

                            //перейти на форму админа

                            FormsForAdmin.Index indexAdmin = new FormsForAdmin.Index(user);
                            indexAdmin.Show();

                            Close();
                        }
                        else if (countUsers > 0)
                        {
                            if (user == null)
                            {
                                user = CreateCustomer(txbRegisterEmail.Text, txbRegisterPassword.Password);

                                _userService.Create(user);

                                //перейти на форму юзера

                                FormsForUser.Index indexUser = new FormsForUser.Index(user);
                                indexUser.Show();

                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Такой логин уже существует!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают!");
                    txbRegisterPassword.Password = "";
                    txbRegisterPasswordConfirm.Password = "";
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля формы регистрация!");
                txbRegisterPassword.Password = "";
                txbRegisterPasswordConfirm.Password = "";
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txbLoginEmail.Text != string.Empty && txbLoginPassword.Password != string.Empty)
            {
                User user = _userService.GetByEmail(txbLoginEmail.Text);

                if (user != null)
                {
                    var passwordHash = Crypto.Sha256(txbLoginEmail.Text + txbLoginPassword.Password);

                    if (user.Password == passwordHash)
                    {
                        if (user.Role)
                        {
                            FormsForAdmin.Index indexAdmin = new FormsForAdmin.Index(user);
                            indexAdmin.Show();

                            Close();
                        }
                        else
                        {
                            FormsForUser.Index indexUser = new FormsForUser.Index(user);
                            indexUser.Show();

                            Close();
                        }       
                    }
                    else
                    {
                        MessageBox.Show("Пароль не корректный!");
                        txbLoginPassword.Password = "";
                    }
                }
                else
                {
                    MessageBox.Show("Такого логина не существует!");
                    txbLoginPassword.Password = "";
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля формы регистрация!");
                txbLoginPassword.Password = "";
            }
        }

        public User CreateAdmin(string email, string password)
        {
            password = Crypto.Sha256(email + password);

            User admin = new User { Email = email, Password = password, Role = true };

            return admin;
        }

        public User CreateCustomer(string email, string password)
        {
            password = Crypto.Sha256(email + password);

            User customer = new User { Email = email, Password = password, Role = false };

            return customer;
        }
    }
}
