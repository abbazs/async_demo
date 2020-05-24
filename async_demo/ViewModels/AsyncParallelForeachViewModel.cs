using async.Helpers;
using async.Models;
using async.MVVMHelpers;
using async.ViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace async.ViewModels
{
    public class AsyncParallelForeachViewModel : ObservableObject, IBaseViewModel
    {
        private CancellationTokenSource cancellationToken;

        public CancellationTokenSource cts
        {
            get => cancellationToken;
            set => SetProperty(ref cancellationToken, value);
        }

        private string title;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private long totalTime;

        public long TotalTime
        {
            get => totalTime;
            set => SetProperty(ref totalTime, value);
        }

        private int percentageCompleted;

        public int PercentageCompleted
        {
            get => percentageCompleted;
            set => SetProperty(ref percentageCompleted, value);
        }

        public ObservableCollection<WebsiteData> WebsiteDatas { get; set; }

        public ICommand GetDataCommand => new RelayCommand(async prop => await GetData(prop), cp => cts == null);

        public ICommand CancelCommand => new RelayCommand(prop => Cancel(prop), cp => cts != null);

        private void Cancel(object prop)
        {
            cts.Cancel();
        }

        public AsyncParallelForeachViewModel()
        {
            Title = "Async Parallel.Foreach";
            WebSites.FillCollection(this);
        }

        private async Task GetData(object prop)
        {
            cts = new CancellationTokenSource();
            Stopwatch topwatch = Stopwatch.StartNew();
            int i = 1;
            try
            {
                await Task.Run(() =>
                {
                    Parallel.ForEach<WebsiteData>(WebsiteDatas, (s) =>
                    {
                        WebSites.Download(s, cts.Token);
                        PercentageCompleted = (i * 100) / WebsiteDatas.Count;
                        i++;
                    });
                });
            }
            catch (OperationCanceledException)
            {
                //Do nothing
            }

            topwatch.Stop();

            TotalTime = topwatch.ElapsedMilliseconds;
            cts = null;
        }
    }
}
