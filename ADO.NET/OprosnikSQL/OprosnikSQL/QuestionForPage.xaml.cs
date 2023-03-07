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

namespace OprosnikSQL
{
    /// <summary>
    /// Логика взаимодействия для QuestionForPage.xaml
    /// </summary>
    public partial class QuestionForPage : Page
    {
        static DataContext DB = new DataContext();
        static int ID = 1;
        static int MaxID;
        static int Balance = 0;
        static int RightAnswers = 0;
        static NavigationService Service;
        static string FIO;
        public QuestionForPage(string fio)
        {
            InitializeComponent();
            FIO = fio;

            if (ID == 1)
                MaxID = (from q in DB.questions select q).Count();

            Question.Text = (from q in DB.questions
                             where q.question_id == ID
                             select q).First().question_text;

            var res = (from q in DB.answers
                       where q.question_id == ID
                       select q);

            string[] ansArr = (from a in res select a.answer_text).ToArray();
            for (int i = 0; i < ansArr.Length; i++)
            {
                AnswersGrid.ColumnDefinitions.Add(new ColumnDefinition());
                Button button = new Button();
                button.FontFamily = new FontFamily("Impact");
                button.Foreground = new SolidColorBrush(Colors.White);
                button.FontSize = 15;
                button.Background = new SolidColorBrush(Colors.DarkGray);
                button.Margin = new Thickness(10);
                button.Content = ansArr[i];
                button.Click += Button_Click;
                Grid.SetColumn(button, i);
                AnswersGrid.Children.Add(button);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            answers ans = (from q in DB.answers
                           where q.question_id == ID && q.is_correct != null
                           select q).First();
            if ((string)((Button)sender).Content == ans.answer_text)
            { 
                Balance += ans.points;
                RightAnswers++;
            }
            Service = NavigationService.GetNavigationService(this);
            
            ID++;
            if (ID > MaxID) Service.Navigate(new ResultPage(FIO, Balance, RightAnswers));
            else Service.Navigate(new QuestionForPage(FIO));
        }
    }
}
