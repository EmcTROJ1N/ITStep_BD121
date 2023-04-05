using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;


namespace SMTPAutoMailing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> Emails { get; set; } = new ObservableCollection<string>();
        private ObservableCollection<string> Attachments { get; set; } = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            EmailListView.ItemsSource = Emails;
            AttachmentsListView.ItemsSource = Attachments;
        }

        private void AttachmentButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                foreach (string filename in fileDialog.FileNames)
                    Attachments.Add(filename);
            }
        }
        
        
        private MailMessage CreateMailMessage(string to)
        {
            MailMessage mailMsg = new MailMessage();

            // заполнить поля почтового сообщения из полей окна
            mailMsg.From = new MailAddress(LoginTextBox.Text);
            mailMsg.To.Add(new MailAddress(to));
            mailMsg.IsBodyHtml = false;
            mailMsg.Subject = SubjectTextBox.Text;
            mailMsg.Body = MessageTextBox.Text;

            foreach (string attachmanetPath in Attachments)
                mailMsg.Attachments.Add(new Attachment(attachmanetPath));

            return mailMsg;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.mail.ru", 587);
            
                client.Credentials = new NetworkCredential(LoginTextBox.Text, PasswdBox.Password);
                client.EnableSsl = true;
            
				// отправляем почту с учётной записи на сервере Google
				//SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

				// Ящик на yandex.ru - protoshadow77@yandex.ru
				// Пароль только для приложений


                MailingProgress.Maximum = Emails.Count();
                MailingProgress.Value = 0;
                Task.Run(() =>
                {
                    foreach (string mail in Emails)
                    {
                        client.Send(Dispatcher.Invoke(() => CreateMailMessage(mail)));
                        Dispatcher.Invoke(() => MailingProgress.Value++);
                    }
                });
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка такая: " + exp.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (emailRegex.IsMatch(EmailTextBox.Text))
            {
                Emails.Add(EmailTextBox.Text);
                EmailTextBox.Text = string.Empty;
            }
            else
                MessageBox.Show("Invalid email address format.");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmailListView.SelectedItems.Count > 0)
            {
                // Loop through the selected items and remove them from the ObservableCollection
                for (int i = EmailListView.SelectedItems.Count - 1; i >= 0; i--)
                    Emails.Remove((string)EmailListView.SelectedItems[i]!);
            }
        }

        private void DeleteAllButton_OnClick(object sender, RoutedEventArgs e) => Emails.Clear();
    }
}