using System.Windows.Controls;

namespace OprosnikSQL
{
    /// <summary>
    /// Логика взаимодействия для ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public ResultPage(string FIO, int balance, int rightAns)
        {
            InitializeComponent();
            Result.Text = $"Dear {FIO}, Your balance: {balance}\nRight answers: {rightAns}";
        }
    }
}
