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

namespace TestSystemProject.FormsForAdmin.Question
{
    /// <summary>
    /// Логика взаимодействия для Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        private readonly TestService _testService;
        private readonly QuestionService _questionService;

        public Create()
        {
            InitializeComponent();

            _testService = new TestService();
            _questionService = new QuestionService();

            txbTest.ItemsSource = _testService.GetAll().Select(x => x.Name).ToArray();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txbQuestion.Text != string.Empty && txbTest.Text != string.Empty && txbScore.Text != string.Empty)
            {
                int score;

                if(Int32.TryParse(txbScore.Text, out score))
                {
                    if(score > 0)
                    {   
                        var test = _testService.GetByName(txbTest.Text);

                        //проверка: нет ли такого вопроса у этого теста
                        var question = _questionService.GetAll().Where(x => x.TestId == test.TestId && x.Text == txbQuestion.Text).FirstOrDefault();

                        if(question == null)
                        {
                            _questionService.Create(new Entities.Question { Text = txbQuestion.Text, Score = Convert.ToInt32(txbScore.Text), TestId = test.TestId });

                            MessageBox.Show("Объект успешно создан!");

                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Такой вопрос у этого теста уже есть!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите положительное число!");
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели некорректное значение в числовое поле!");
                }
            }
            else
            {
                MessageBox.Show("Заполните все ключевые поля!");
            }
        }
    }
}
