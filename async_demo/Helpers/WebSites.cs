using async.Models;
using async.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace async.Helpers
{
    public static class WebSites
    {
        public static List<string> Sites = new List<string>() 
        {   
            "https://www.yahoo.com",
            "https://www.google.com",
            "https://www.stackoverflow.com",
            "https://www.cnn.com",
            "https://www.microsoft.com",
            "https://www.twitter.com"
        };

        public static void FillCollection(IBaseViewModel baseViewModel)
        {
            baseViewModel.WebsiteDatas = new ObservableCollection<WebsiteData>();
            foreach (var s in WebSites.Sites)
            {
                baseViewModel.WebsiteDatas.Add(new WebsiteData() { WebsiteUrl = s });
            }
        }

        public static void Download(WebsiteData s, CancellationToken token)
        {
            Stopwatch watch = Stopwatch.StartNew();
            using (WebClient client = new WebClient())
            {
                s.WebSiteData = client.DownloadString(s.WebsiteUrl);
                token.ThrowIfCancellationRequested();
            }
            watch.Stop();
            s.TimeTaken = watch.ElapsedMilliseconds;
            s.Status = "Successfull";
        }
    }
}