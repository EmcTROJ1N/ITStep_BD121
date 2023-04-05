using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TrafficLight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush YellowBrush = new SolidColorBrush(Colors.Yellow);
        private static readonly SolidColorBrush GreenBrush = new SolidColorBrush(Colors.Green);

        private readonly Thread _redThread;
        private readonly Thread _yellowThread;
        private readonly Thread _greenThread;

        private SolidColorBrush currentBrush;

        public MainWindow()
        {
            InitializeComponent();

            _redThread = new Thread(RedThread);
            _yellowThread = new Thread(YellowThread);
            _greenThread = new Thread(GreenThread);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _redThread.Start();
            _yellowThread.Start();
            _greenThread.Start();
        }

        private void RedThread()
        {
            while (true)
            {
                lock (this)
                {
                    currentBrush = RedBrush;
                    UpdateLight();
                    Thread.Sleep(1000);
                }

            }
        }

        private void YellowThread()
        {
            while (true)
            {
                lock (this)
                {
                    currentBrush = YellowBrush;
                    UpdateLight();
                    Thread.Sleep(1000);
                }

            }
        }

        private void GreenThread()
        {
            while (true)
            {
                lock (this)
                {
                    currentBrush = GreenBrush;
                    UpdateLight();
                    Thread.Sleep(1000);
                }
            }
        }

        private void UpdateLight()
        {
            // This method assumes that you have 3 Ellipse objects in your XAML
            // named RedLight, YellowLight, and GreenLight, respectively
            Dispatcher.Invoke(() => RedLight.Fill = RedBrush.Equals(currentBrush) ? currentBrush : Brushes.Gray);
            Dispatcher.Invoke(() => YellowLight.Fill = YellowBrush.Equals(currentBrush) ? currentBrush : Brushes.Gray);
            Dispatcher.Invoke(() => GreenLight.Fill = GreenBrush.Equals(currentBrush) ? currentBrush : Brushes.Gray);
        }
    }
}
