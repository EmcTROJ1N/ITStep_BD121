using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

namespace ImageParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CurrentUrl = "bing.com";
        List<Uri> ImagesUrlList = new List<Uri>();
        private string FilesFolder = Environment.CurrentDirectory;
        private ObservableCollection<string> DownloadedImages { get; set; } = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            Gallery.ItemsSource = DownloadedImages;
            async void InitializeAsync() => await Browser.EnsureCoreWebView2Async(null);
            AddressBox.Text = CurrentUrl;
            Browser.Source = new Uri($"http://{CurrentUrl}");
        }

        private void NavigateToUrlClick(object sender, RoutedEventArgs e) => NavigateToUrl(AddressBox.Text);
        private void NavigateToUrl(string url)
        {
            try
            {
                Browser.CoreWebView2.Navigate($"http://{url}");
                CurrentUrl = url;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BeginDownloadImages(object sender, RoutedEventArgs e)
        {
            FileDownloadBar.Minimum = 0;
            Int32.TryParse(Depth.Text, out int maxDepth);
            if (maxDepth == 0) MessageBox.Show("Enter correct depth");
            else Task.Run(() =>
            {
                GetImagesFromWebPage($"http://{CurrentUrl}", maxDepth);
                DownloadImages(FilesFolder);
            });
        }


        void DownloadImages(string path)
        {
            string folderPath = $"{path}/DownloadedImages";
            Directory.CreateDirectory(folderPath);
            Dispatcher.Invoke(() =>
            {
                FileDownloadBar.Minimum = 0;
                FileDownloadBar.Maximum = ImagesUrlList.Count;
            });


            WebClient client = new WebClient();
            foreach (Uri imageUri in ImagesUrlList)
            {
                try
                {
                    string filename = imageUri.ToString().Split('/').Last();
                    client.DownloadFile(imageUri, $"{folderPath}/{filename}");
                    Dispatcher.Invoke(() =>
                    {
                        FileDownloadBar.Value++;
                        DownloadedImages.Add(filename);
                    });
                }
                catch (Exception) { }
            }
        }
        void GetImagesFromWebPage(string url, int maxDepth, int currentDepth = 1)
        {
            HtmlDocument page;
            HtmlWeb webPage = new HtmlWeb();
            try
            {
                webPage.Load(url);
                page = webPage.Load(url);
            }
            catch (Exception) { return; }

            if (currentDepth < maxDepth)
            {
                var aNodes = page.DocumentNode.SelectNodes("//a");
                if (aNodes != null)
                {
                    foreach (HtmlNode node in aNodes)
                    {
                        if (node.Attributes != null && node.Attributes["href"] != null &&
                            node.Attributes["href"].Value != null && 
                            Uri.IsWellFormedUriString(node.Attributes["href"].Value, UriKind.Absolute))
                            GetImagesFromWebPage(node.Attributes["href"].Value, maxDepth, ++currentDepth);
                    }
                }
            }
            var imgNodes = page.DocumentNode.SelectNodes("//img");
            if (imgNodes != null)
            {
                foreach (HtmlNode node in imgNodes)
                    if (node.Attributes["src"].Value.StartsWith("http://") ||
                        node.Attributes["src"].Value.StartsWith("https://"))
                    ImagesUrlList.Add(new Uri(node.Attributes["src"].Value));
                    else ImagesUrlList.Add(new Uri($"http://{CurrentUrl}/{node.Attributes["src"].Value}"));
                    
            }
        }
        
        private void Gallery_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Process.Start($"{FilesFolder}/")
        }
    }
}