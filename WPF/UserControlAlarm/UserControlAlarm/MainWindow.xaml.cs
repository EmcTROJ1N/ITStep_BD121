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

namespace UserControlAlarm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Alarm.Handler += qqq;
            Alarm.Alarms.Add(new DateTime(2023, 2, 7, 15, 43, 15));
            Alarm.Alarms.Add(new DateTime(2023, 2, 7, 15, 43, 20));
        }

        private void qqq(object sender, EventArgs e) =>
            MessageBox.Show("Alarm!!!");


    }
}
