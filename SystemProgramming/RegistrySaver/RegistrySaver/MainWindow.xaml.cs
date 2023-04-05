using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
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
using Microsoft.Win32;

namespace RegistrySaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Color CurrentColor;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            RegistryKey key;
            if (Registry.CurrentUser.GetSubKeyNames().Contains("RegistrySaver"))
            {
                key = Registry.CurrentUser.OpenSubKey("RegistrySaver", true);
                Width = Double.Parse((string)key.GetValue("windowWidth"));
                Height = Double.Parse((string)key.GetValue("windowHeight"));
                MainGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString((string)key.GetValue("windowColor")));
                CurrentColor = (Color)ColorConverter.ConvertFromString((string)key.GetValue("windowColor"));
            }
        }

        private void MainWindow_OnClosed(object? sender, EventArgs e)
        {
            RegistryKey key;
            if (Registry.CurrentUser.GetSubKeyNames().Contains("RegistrySaver")) key = Registry.CurrentUser.OpenSubKey("RegistrySaver", true);
            else key = Registry.CurrentUser.CreateSubKey("RegistrySaver");
            key.SetValue("windowWidth", this.Width);
            key.SetValue("windowHeight", this.Height);
            key.SetValue("windowColor", this.CurrentColor);
        }

        private void GreenBgClick(object sender, RoutedEventArgs e)
        {
            MainGrid.Background = new SolidColorBrush(Colors.Green);
            CurrentColor = Colors.Green;
        }

        private void YellowBgClick(object sender, RoutedEventArgs e)
        {
            MainGrid.Background = new SolidColorBrush(Colors.Yellow);
            CurrentColor = Colors.Yellow;
        }

        private void WhiteBgClick(object sender, RoutedEventArgs e)
        {
            MainGrid.Background = new SolidColorBrush(Colors.White);
            CurrentColor = Colors.White;
        }
    }
}