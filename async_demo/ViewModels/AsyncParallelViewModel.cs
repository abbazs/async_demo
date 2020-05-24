﻿using async.Helpers;
using async.Models;
using async.MVVMHelpers;
using async.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace async.ViewModels
{
    public class AsyncParallelViewModel : ObservableObject, IBaseViewModel
    {
        CancellationTokenSource cancellationToken;

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

        private int counter;

        public ObservableCollection<WebsiteData> WebsiteDatas { get; set; }

        public ICommand GetDataCommand => new RelayCommand(async prop => await GetData(prop), cp => cancellationToken == null);

        public ICommand CancelCommand => new RelayCommand(prop => Cancel(prop), cp => cancellationToken != null);

        private void Cancel(object prop)
        {
            cancellationToken.Cancel();
        }

        private async Task GetData(object prop)
        {
            cancellationToken = new CancellationTokenSource();
            Stopwatch topwatch = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();
            counter = 1;

            foreach (WebsiteData s in WebsiteDatas)
            {
                tasks.Add(Task.Run(() => ParallelDownload(s)));
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch(OperationCanceledException)
            {
                // Do nothing
            }
            
            TotalTime = topwatch.ElapsedMilliseconds;
            cancellationToken = null;
        }

        private void ParallelDownload(WebsiteData s)
        {
            WebSites.Download(s, cancellationToken.Token);
            PercentageCompleted = (counter * 100) / WebsiteDatas.Count;
            counter++;
        }

        public AsyncParallelViewModel()
        {
            Title = "Async Parallel";
            WebSites.FillCollection(this);
        }   
    }
}