using AdministratorClientApp.ServiceReference;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
using static System.Net.Mime.MediaTypeNames;

namespace AdministratorClientApp
{
    /// <summary>
    /// Логика взаимодействия для Screen.xaml
    /// </summary>
    public partial class Screen : Window
    {
        AdminServiceClient Server;
        Administrator Admin;
        string TargetLogin;
        public Screen(AdminServiceClient server, Administrator admin, string targetLogin)
        {
            InitializeComponent();
            this.Server = server;
            this.Admin = admin;
            this.TargetLogin = targetLogin;
            try { ScreenshotImg.Source = GetScreen(); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); Close(); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); Close(); }
        }


        BitmapSource GetScreen()
        {
            byte[] screenBytes = Server.GetScreenshot(Admin.Login, Admin.Password, TargetLogin);
            BitmapSource screenshot;
            using (MemoryStream stream = new MemoryStream(screenBytes))
            {
                BitmapDecoder deconder = BitmapDecoder.Create(stream,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.OnLoad);
                BitmapFrame frame = deconder.Frames.First();

                frame.Freeze();
                screenshot = frame;
            }
            return screenshot;
        }

        private void SaveScreen(object sender, RoutedEventArgs e)
        {
            if (ScreenshotImg.Source == null) return;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG Image|*.png";
            dialog.Title = "Save an Image File";
            if (dialog.ShowDialog() == true)
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ScreenshotImg.Source));
                using (FileStream file = new FileStream(dialog.FileName, FileMode.Create))
                    encoder.Save(file);
            }
        }

        private void GetScreenShotClick(object sender, RoutedEventArgs e)
        {
            try { ScreenshotImg.Source = GetScreen(); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString());  }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message);  }
        }
    }
}
