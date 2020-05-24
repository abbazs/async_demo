using async.Models;
using async.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            "https://www.youtube.com"
        };

        public static void FillCollection(IBaseViewModel baseViewModel)
        {
            baseViewModel.WebsiteDatas = new ObservableCollection<WebsiteData>();
            foreach (var s in WebSites.Sites)
            {
                baseViewModel.WebsiteDatas.Add(new WebsiteData() { WebsiteUrl = s });
            }
        }


        public static void DownloadWebsite(IBaseViewModel ibvm)
        {
            Stopwatch topwatch = Stopwatch.StartNew();
            foreach (WebsiteData s in ibvm.WebsiteDatas)
            {
                Download(s);
            }
            topwatch.Stop();
            ibvm.TotalTime = topwatch.ElapsedMilliseconds;
        }

        private static void Download(WebsiteData s)
        {
            Stopwatch watch = Stopwatch.StartNew();
            using (WebClient client = new WebClient())
            {
                s.WebSiteData = client.DownloadString(s.WebsiteUrl);
            }
            watch.Stop();
            s.TimeTaken = watch.ElapsedMilliseconds;
        }

        public static async Task DownloadWebsiteAsync(IBaseViewModel ibvm)
        {
            Stopwatch topwatch = Stopwatch.StartNew();
            foreach (WebsiteData s in ibvm.WebsiteDatas)
            {
                await Task.Run(() => Download(s));
            }
            topwatch.Stop();
            ibvm.TotalTime = topwatch.ElapsedMilliseconds;
        }

        public static async Task DownloadWebsiteParallelAsync(IBaseViewModel ibvm)
        {
            Stopwatch topwatch = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();

            foreach (WebsiteData s in ibvm.WebsiteDatas)
            {
                tasks.Add(Task.Run(() => Download(s)));
            }

            await Task.WhenAll(tasks);

            ibvm.TotalTime = topwatch.ElapsedMilliseconds;
        }
    }
}