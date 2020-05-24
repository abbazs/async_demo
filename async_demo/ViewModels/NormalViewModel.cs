﻿using async.Helpers;
using async.Models;
using async.MVVMHelpers;
using async.ViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;

namespace async.ViewModels
{
    public class NormalViewModel : ObservableObject, IBaseViewModel
    {
        private CancellationTokenSource cancellationToken = new CancellationTokenSource();
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

        public ICommand GetDataCommand => new RelayCommand(prop => GetData(prop));

        public ICommand CancelCommand => new RelayCommand(prop => Cancel(prop));

        private void Cancel(object prop)
        {
            cancellationToken.Cancel();
        }

        private void GetData(object prop)
        {
            Stopwatch topwatch = Stopwatch.StartNew();
            int i = 1;

            try
            {
                foreach (WebsiteData s in WebsiteDatas)
                {
                    WebSites.Download(s, cancellationToken.Token);
                    PercentageCompleted = (i * 100) / WebsiteDatas.Count;
                    i++;
                }
            }
            catch (OperationCanceledException)
            {
                //Do nothing
            }
            
            topwatch.Stop();
            TotalTime = topwatch.ElapsedMilliseconds;
        }

        public NormalViewModel()
        {
            Title = "Normal";
            WebSites.FillCollection(this);
        }
    }
}
