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
    /// Логика взаимодействия для Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        private readonly Entities.Question _question;

        private readonly TestService _testService;
        private readonly QuestionService _questionService;

        public Update(Entities.Question question)
        {
            _question = question;

            InitializeComponent();

            _testService = new TestService();
            _questionService = new QuestionService();

            txbTest.ItemsSource = _testService.GetAll().Select(x => x.Name).ToArray();

            txbQuestion.Text = _question.Text;
            txbScore.Text = _question.Score.ToString();
            txbTest.Text = _testService.GetById(_question.TestId).Name;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txbQuestion.Text != string.Empty && txbTest.Text != string.Empty && txbScore.Text != string.Empty)
            {
                int score;

                if (Int32.TryParse(txbScore.Text, out score))
                {
                    if (score > 0)
                    {
                        var test = _testService.GetByName(txbTest.Text);

                        _question.Text = txbQuestion.Text;
                        _question.Score = Convert.ToInt32(txbScore.Text);
                        _question.TestId = test.TestId;

                        _questionService.Update(_question);

                        MessageBox.Show("Объект успешно изменён!");

                        Close();
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
