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

namespace TestSystemProject.FormsForAdmin.Answer
{
    /// <summary>
    /// Логика взаимодействия для Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        private readonly AnswerService _answerService;
        private readonly QuestionService _questionService;
        private readonly TestService _testService;

        public Create()
        {
            InitializeComponent();

            _answerService = new AnswerService();
            _questionService = new QuestionService();
            _testService = new TestService();

            txbTest.ItemsSource = _testService.GetAll().Select(x => x.Name).ToList();

            txbQuestion.IsEnabled = false;
            txbAnswer.IsEnabled = false;
            txbIsRight.IsEnabled = false;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txbAnswer.Text != string.Empty && txbQuestion.Text != string.Empty)
            {
                var question = _questionService.GetByName(txbQuestion.Text);

                //проверка: есть ли правильный ответ у вопроса
                int countIsRightAnswer = _answerService.GetAll().Where(x => x.QuestionId == question.QuestionId && x.IsRight && x.IsRight == Convert.ToBoolean(txbIsRight.IsChecked)).Count();

                if (countIsRightAnswer == 0)
                {
                    //проверка: на свопадение такого ответа
                    var answer = _answerService.GetAll().Where(x => x.QuestionId == question.QuestionId && x.Text == txbAnswer.Text).FirstOrDefault();

                    if (answer == null)
                    {
                        _answerService.Create(new Entities.Answer { Text = txbAnswer.Text, IsRight = Convert.ToBoolean(txbIsRight.IsChecked), QuestionId = question.QuestionId });

                        MessageBox.Show("Объект успешно создан!");

                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Такой ответ у этого теста уже есть!");
                    }
                }
                else
                {
                    MessageBox.Show("Правильный ответ у этого вопроса уже есть!");
                }
            }
            else
            {
                MessageBox.Show("Заполните все ключевые поля!");
            }
        }

        private void TxbTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            try
            {
                if (comboBox != null)
                {
                    var test = _testService.GetByName(comboBox.SelectedItem.ToString());

                    txbQuestion.IsEnabled = true;

                    //обнуляем данные для ввода других значений
                    txbAnswer.Text = "";
                    txbIsRight.IsChecked = false;
                    txbAnswer.IsEnabled = false;
                    txbIsRight.IsEnabled = false;

                    txbQuestion.ItemsSource = new List<string>(); //для того, чтобы обновить combobox вопросов
                    txbQuestion.ItemsSource = _questionService.GetAll().Where(x => x.TestId == test.TestId).Select(x => x.Text).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxbQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedItem != null)
            {
                if (comboBox.SelectedItem.ToString() != string.Empty)
                {
                    var question = _questionService.GetByName(comboBox.SelectedItem.ToString());

                    txbAnswer.IsEnabled = true;
                    txbIsRight.IsEnabled = true;
                }
            }
            else
            {
                txbQuestion.ItemsSource = null;
            }
        }
    }
}
