using async.Models;
using async.ViewModels;
using async.ViewModels.Interfaces;
using System;
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
            baseViewModel.Urls = new ObservableCollection<string>();
            foreach (var s in WebSites.Sites)
            {
                baseViewModel.Urls.Add(s);
            }
        }

        public static void Download(IProgress<ResultData> r, string s, CancellationToken token)
        {
            Stopwatch watch = Stopwatch.StartNew();
            ResultData resultData = new ResultData();
            using (WebClient client = new WebClient())
            {
                resultData.Url = s;
                try
                {
                    string data = client.DownloadString(s);
                    token.ThrowIfCancellationRequested();
                    resultData.DataLength = data.Length;
                }
                catch (WebException)
                {
                    resultData.DataLength = 0;
                }
                
            }
            watch.Stop();
            resultData.Status = resultData.DataLength > 0 ? "Success" : "Fail";
            resultData.TimeTaken = watch.ElapsedMilliseconds;
            r.Report(resultData);
        }
    }
}