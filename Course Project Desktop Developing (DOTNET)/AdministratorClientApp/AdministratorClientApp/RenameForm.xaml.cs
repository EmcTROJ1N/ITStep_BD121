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

namespace AdministratorClientApp
{
    /// <summary>
    /// Логика взаимодействия для RenameForm.xaml
    /// </summary>
    
    public class StringWrapper
    {
        public string Str;
    }

    public partial class RenameForm : Window
    {
        StringWrapper NewFolderName;
        public RenameForm(StringWrapper newFolderName)
        {
            InitializeComponent();
            NewFolderName = newFolderName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewFolderName.Str = InputTextBox.Text;
            this.Close();
        }
    }
}
