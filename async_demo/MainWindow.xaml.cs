using System;
using System.Collections.Generic;
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

namespace async
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> websites = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            websites.Add("https://www.yahoo.com");
            websites.Add("https://www.google.com");
            websites.Add("https://www.stackoverflow.com");
            websites.Add("https://www.cnn.com");
            websites.Add("https://www.microsoft.com");
            websites.Add("https://www.youtube.com");
        }

        private void executeSync_Click(object sender, RoutedEventArgs e)
        {
            resultsWindowNormal.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            RunDownloadSync();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            resultsWindowNormal.Text += $"Total execution time : { elapsedMs }";
        }

        private void RunDownloadSync()
        {
            foreach(string site in websites)
            {
                WebsiteDataModel results = DownloadWebsite(site);
                ReportWebsiteInfo(results, resultsWindowNormal);
            }
        }

        private WebsiteDataModel DownloadWebsite(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();
            output.WebsiteUrl = websiteURL;
            output.WebsiteData = client.DownloadString(websiteURL);

            return output;
        }

        private void ReportWebsiteInfo(WebsiteDataModel data, TextBlock tb)
        {
            tb.Text += $"{ data.WebsiteUrl } downloaded: { data.WebsiteData.Length } characters long. { Environment.NewLine }";
        }

        private async void executeAsync_Click(object sender, RoutedEventArgs e)
        {
            resultsWindowAsync.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await RunDownloadAsync();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            resultsWindowAsync.Text += $"Total execution time : { elapsedMs }";
        }

        private async Task RunDownloadAsync()
        {
            foreach (string site in websites)
            {
                WebsiteDataModel results = await Task.Run(() => DownloadWebsite(site));
                ReportWebsiteInfo(results, resultsWindowAsync);
            }
        }        
        
        private async Task RunDownloadParallelAsync()
        {
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

            foreach (string site in websites)
            {
                tasks.Add(Task.Run(() => DownloadWebsite(site)));
            }

            var results = await Task.WhenAll(tasks);

            foreach(var item in results)
            {
                ReportWebsiteInfo(item, resultsWindowParallelAsync);
            }
        }

        private async void executeParallelAsync_Click(object sender, RoutedEventArgs e)
        {
            resultsWindowParallelAsync.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await RunDownloadParallelAsync();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            resultsWindowParallelAsync.Text += $"Total execution time : { elapsedMs }";
        }
    }
}
