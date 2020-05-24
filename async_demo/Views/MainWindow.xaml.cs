using async.MVVMHelpers;
using System;
using System.Windows;

namespace async
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            BindingErrorListener.Listen(m => Console.WriteLine(m));
            InitializeComponent();
        }

        //private void executeSync_Click(object sender, RoutedEventArgs e)
        //{
        //    resultsWindowNormal.Text = "";
        //    var watch = System.Diagnostics.Stopwatch.StartNew();
        //    RunDownloadSync();
        //    watch.Stop();
        //    var elapsedMs = watch.ElapsedMilliseconds;
        //    resultsWindowNormal.Text += $"Total execution time : { elapsedMs }";
        //}

        //private void RunDownloadSync()
        //{
        //    foreach(string site in websites)
        //    {
        //        WebsiteDataModel results = DownloadWebsite(site);
        //        ReportWebsiteInfo(results, resultsWindowNormal);
        //    }
        //}

        //private WebsiteDataModel DownloadWebsite(string websiteURL)
        //{
        //    WebsiteDataModel output = new WebsiteDataModel();
        //    WebClient client = new WebClient();
        //    output.WebsiteUrl = websiteURL;
        //    output.WebsiteData = client.DownloadString(websiteURL);

        //    return output;
        //}

        //private void ReportWebsiteInfo(WebsiteDataModel data, TextBlock tb)
        //{
        //    tb.Text += $"{ data.WebsiteUrl } downloaded: { data.WebsiteData.Length } characters long. { Environment.NewLine }";
        //}

        //private async void executeAsync_Click(object sender, RoutedEventArgs e)
        //{
        //    resultsWindowAsync.Text = "";
        //    var watch = System.Diagnostics.Stopwatch.StartNew();
        //    await RunDownloadAsync();
        //    watch.Stop();
        //    var elapsedMs = watch.ElapsedMilliseconds;
        //    resultsWindowAsync.Text += $"Total execution time : { elapsedMs }";
        //}

        //private async Task RunDownloadAsync()
        //{
        //    foreach (string site in websites)
        //    {
        //        WebsiteDataModel results = await Task.Run(() => DownloadWebsite(site));
        //        ReportWebsiteInfo(results, resultsWindowAsync);
        //    }
        //}        
        
        //private async Task RunDownloadParallelAsync()
        //{
        //    List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

        //    foreach (string site in websites)
        //    {
        //        tasks.Add(Task.Run(() => DownloadWebsite(site)));
        //    }

        //    var results = await Task.WhenAll(tasks);

        //    foreach(var item in results)
        //    {
        //        ReportWebsiteInfo(item, resultsWindowParallelAsync);
        //    }
        //}

        //private async void executeParallelAsync_Click(object sender, RoutedEventArgs e)
        //{
        //    resultsWindowParallelAsync.Text = "";
        //    var watch = System.Diagnostics.Stopwatch.StartNew();
        //    await RunDownloadParallelAsync();
        //    watch.Stop();
        //    var elapsedMs = watch.ElapsedMilliseconds;
        //    resultsWindowParallelAsync.Text += $"Total execution time : { elapsedMs }";
        //}
    }
}
