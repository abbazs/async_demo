﻿using async.Helpers;
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
    public class ParallelForeachViewModel : ObservableObject, IBaseViewModel
    {
        private CancellationTokenSource cancellationToken;

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

        public ICommand GetDataCommand => new RelayCommand(prop => GetData(prop), cp => cancellationToken == null);

        public ICommand CancelCommand => new RelayCommand(prop => Cancel(prop), cp => cancellationToken != null);

        private void Cancel(object prop)
        {
            cancellationToken.Cancel();
        }

        public ParallelForeachViewModel()
        {
            Title = "Parallel.Foreach";
            WebSites.FillCollection(this);
        }

        private void GetData(object prop)
        {
            PercentageCompleted = 0;
            cancellationToken = new CancellationTokenSource();
            Stopwatch topwatch = Stopwatch.StartNew();
            int i = 1;

            try
            {
                Parallel.ForEach<WebsiteData>(WebsiteDatas, (s) =>
                {
                    WebSites.Download(s, cancellationToken.Token);
                    PercentageCompleted = (i * 100) / WebsiteDatas.Count;
                    i++;
                });
            }
            catch (OperationCanceledException)
            {
                //Do nothing
            }

            topwatch.Stop();
            TotalTime = topwatch.ElapsedMilliseconds;
            cancellationToken = null;
        }
    }
}