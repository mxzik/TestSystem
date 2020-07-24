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

namespace TestSystemProject.FormsForUser
{
    /// <summary>
    /// Логика взаимодействия для Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        private readonly User _user;

        private readonly ThemeService _themeService;
        private readonly TestService _testService;
        private readonly QuestionService _questionService;
        private readonly AnswerService _answerService;
        private readonly ResultService _resultService;
        private readonly UserService _userService;

        public Index(User user)
        {
            _user = user;

            InitializeComponent();

            _themeService = new ThemeService();
            _testService = new TestService();
            _questionService = new QuestionService();
            _answerService = new AnswerService();
            _resultService = new ResultService();
            _userService = new UserService();

            txbTheme.ItemsSource = _themeService.GetAll().Select(x => x.Name).ToList();

            txbTest.IsEnabled = false;
            btnCheckQuestions.IsEnabled = false;
        }

        private void TxbTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCheckQuestions.IsEnabled = false;

            ComboBox comboBox = (ComboBox)sender;

            try
            {
                if (comboBox != null)
                {
                    var theme = _themeService.GetByName(comboBox.SelectedItem.ToString());

                    txbTest.IsEnabled = true;

                    var allTests = _testService.GetAll().Where(x => x.ThemeId == theme.ThemeId).ToList();

                    List<string> testsForForm = new List<string>();

                    for (int i = 0; i < allTests.Count; i++)
                    {
                        var allQuestions = _questionService.GetAll().Where(x => x.TestId == allTests[i].TestId).ToList();

                        int countIsRightAnswer = 0;

                        //буду проверять есть ли у всех вопросов правильный ответ
                        for (int j = 0; j < allQuestions.Count; j++)
                        {
                            var allAnswers = _answerService.GetAll().Where(x => x.QuestionId == allQuestions[j].QuestionId).ToList();

                            for (int k = 0; k < allAnswers.Count; k++)
                            {
                                if (allAnswers[k].IsRight)
                                {
                                    countIsRightAnswer++;
                                }
                            }
                        }

                        if(allQuestions.Count == countIsRightAnswer)
                        {
                            testsForForm.Add(allTests[i].Name);
                        }
                    }

                    txbTest.ItemsSource = testsForForm;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txbTest.Text != string.Empty)
                {
                    btnCheckQuestions.IsEnabled = true;

                    QuestionsPanel.Children.Clear();

                    var test = _testService.GetByName(txbTest.Text);

                    var allQuestions = _questionService.GetAll().Where(x => x.TestId == test.TestId).ToList();

                    for (int i = 0; i < allQuestions.Count; i++)
                    {

                        Border border = new Border();
                        border.Margin = new Thickness(5);
                        border.CornerRadius = new CornerRadius(15);
                        border.BorderThickness = new Thickness(1);
                        border.BorderBrush = new SolidColorBrush(Colors.Black);

                        StackPanel tempStackPanel = new StackPanel();
                        tempStackPanel.Margin = new Thickness(10);

                        TextBlock tempQuestion = new TextBlock();
                        tempQuestion.Text = allQuestions[i].Text;
                        tempQuestion.TextAlignment = TextAlignment.Center;
                        tempQuestion.FontWeight = FontWeights.Bold;
                        tempQuestion.FontSize = 15;
                        tempQuestion.TextWrapping = TextWrapping.Wrap;

                        tempStackPanel.Children.Add(tempQuestion);

                        var allAnswer = _answerService.GetAll().Where(x => x.QuestionId == allQuestions[i].QuestionId).ToList();

                        for (int j = 0; j < allAnswer.Count; j++)
                        {
                            RadioButton tempAnswer = new RadioButton();
                            tempAnswer.Content = allAnswer[j].Text;

                            tempStackPanel.Children.Add(tempAnswer);
                        }

                        StackPanel tempPanelForMyAnswer = new StackPanel();
                        tempPanelForMyAnswer.Orientation = Orientation.Horizontal;

                        TextBlock tempTextBlockMyAnswer = new TextBlock();
                        tempTextBlockMyAnswer.Text = "Свой ответ: ";
                        tempTextBlockMyAnswer.FontWeight = FontWeights.Bold;
                        tempTextBlockMyAnswer.Margin = new Thickness(0, 10, 0, 0);
                        tempTextBlockMyAnswer.HorizontalAlignment = HorizontalAlignment.Left;

                        CheckBox checkBoxForMyAnswer = new CheckBox();
                        checkBoxForMyAnswer.Content = "Да";

                        tempPanelForMyAnswer.Children.Add(tempTextBlockMyAnswer);
                        tempPanelForMyAnswer.Children.Add(checkBoxForMyAnswer);

                        tempStackPanel.Children.Add(tempPanelForMyAnswer);

                        TextBox tempTextBoxAnswer = new TextBox();
                        tempTextBoxAnswer.Width = 200;
                        tempTextBoxAnswer.HorizontalAlignment = HorizontalAlignment.Left;

                        tempStackPanel.Children.Add(tempTextBoxAnswer);

                        border.Child = tempStackPanel;

                        QuestionsPanel.Children.Add(border);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите тест из списка!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void CheckQuestions_Click(object sender, RoutedEventArgs e)
        {
            List<string> answers = new List<string>(); 

            try
            {
                foreach (var item1 in QuestionsPanel.Children)
                {
                    
                    if(item1.GetType() == typeof(Border))
                    {
                        
                        Border border = (Border)item1;
                        var tempPanel = border.Child;
                        StackPanel panel1 = (StackPanel)tempPanel;

                        
                        string tempAnswer = string.Empty;

                        
                        var IsChecked = false;

                        foreach (var item2 in panel1.Children)
                        {
                            
                            if(item2.GetType() == typeof(RadioButton))
                            {
                                if(((RadioButton)item2).IsChecked == true)
                                {
                                    tempAnswer = ((RadioButton)item2).Content.ToString();
                                }
                            }

                            
                            if(item2.GetType() == typeof(StackPanel))
                            {
                                StackPanel panel2 = (StackPanel)item2;
                                
                                foreach(var item3 in panel2.Children)
                                {
                                    if(item3.GetType() == typeof(CheckBox))
                                    {
                                        if (((CheckBox)item3).IsChecked == true)
                                        {
                                            IsChecked = true;
                                        }
                                    }
                                }
                            }

                            
                            if (item2.GetType() == typeof(TextBox))
                            {
                                if (IsChecked) 
                                {
                                    tempAnswer = ((TextBox)item2).Text;
                                }                             
                            }
                        }

                        
                        if(tempAnswer.Trim() != string.Empty)
                        {
                            answers.Add(tempAnswer);
                        }
                    }                  
                }
                //-----------------------------------------------------------------------------------------------
                var test = _testService.GetByName(txbTest.Text);

                var questions = _questionService.GetAll().Where(x => x.TestId == test.TestId).ToList();
                //var result = _resultService.GetByName(txbTest.Text);
                int totalScore = 0;
                int currentScore = 0;
                int countMyAnswerIsRight = 0;

                for(int i = 0; i < questions.Count; i++)
                {
                    totalScore += questions[i].Score;

                    var answersIsRight = _answerService.GetAll().Where(x => x.QuestionId == questions[i].QuestionId && x.IsRight).Select(x => x.Text).FirstOrDefault();

                    for(int j = 0; j < answers.Count; j++)
                    {
                        
                        if(answers[j] == answersIsRight)
                        {
                            countMyAnswerIsRight++;
                            currentScore += questions[i].Score;
                            break;
                        }
                    }
                }

                MessageBox.Show("Пользователь: " + _user.Email + Environment.NewLine +
                                "Количество вопросов: " + questions.Count() + Environment.NewLine +
                                "Количество правильных ответов: " + questions.Count() + Environment.NewLine +
                                "Количество ваших правильных ответов: " + countMyAnswerIsRight + Environment.NewLine +
                                "Общее количество баллов: " + totalScore.ToString() + Environment.NewLine +
                                "Ваше количество баллов: " + currentScore.ToString());

                _resultService.Create(new Result { DateOfTest = DateTime.Now, TestId = test.TestId, TotalScore = currentScore, UserId = _user.UserId });

                QuestionsPanel.Children.Clear();
                txbTest.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
        }
        private void BtnRes_Click(object sender, RoutedEventArgs e)
        {
            Results result = new Results(_user);
            result.Show();
            Close();
        }
    }
}
