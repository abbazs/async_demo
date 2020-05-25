using async.Helpers;
using async.Models;
using async.MVVMHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace async.ViewModels
{
    public class ResultDataViewModel : ObservableObject
    {
        private CancellationTokenSource cancellationToken;

        public ObservableCollection<ResultData> ResultDatas { get; set; }

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

        private int total_count = 0;

        public ICommand CancelCommand => new RelayCommand(prop => Cancel(), cp => cancellationToken != null);

        private void Cancel()
        {
            cancellationToken?.Cancel();
        }

        public ResultDataViewModel()
        {
            ResultDatas = new ObservableCollection<ResultData>();
        }

        public void GetResults(ObservableCollection<string> vs)
        {
            total_count = vs.Count;
            cancellationToken = new CancellationTokenSource();
            Stopwatch topwatch = Stopwatch.StartNew();
            Progress<ResultData> progress = new Progress<ResultData>();
            progress.ProgressChanged += Progress_ProgressChanged;
            try
            {
                foreach (string s in vs)
                {
                    WebSites.Download(progress, s, cancellationToken.Token);
                }
            }
            catch (OperationCanceledException)
            {
                //Do nothing
            }

            topwatch.Stop();
            TotalTime = topwatch.ElapsedMilliseconds;
            cancellationToken = null;
        }

        public void GetResultsParallelForeach(ObservableCollection<string> vs)
        {
            total_count = vs.Count;
            cancellationToken = new CancellationTokenSource();
            Stopwatch topwatch = Stopwatch.StartNew();
            Progress<ResultData> progress = new Progress<ResultData>();
            progress.ProgressChanged += Progress_ProgressChanged;
            try
            {
                Parallel.ForEach<string>(vs, (s) =>
                {
                    WebSites.Download(progress, s, cancellationToken.Token);
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

        public async Task GetResultsParallelForeachAsync(ObservableCollection<string> vs)
        {
            total_count = vs.Count;
            cancellationToken = new CancellationTokenSource();
            Stopwatch topwatch = Stopwatch.StartNew();
            Progress<ResultData> progress = new Progress<ResultData>();
            progress.ProgressChanged += Progress_ProgressChanged;

            await Task.Run(() =>
            {
                Parallel.ForEach<string>(vs, (s, state) =>
                {
                    try
                    {
                        WebSites.Download(progress, s, cancellationToken.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        state.Break();
                    }
                });
            });


            topwatch.Stop();
            TotalTime = topwatch.ElapsedMilliseconds;
            cancellationToken = null;
        }

        public async Task GetResultsAsync(ObservableCollection<string> vs)
        {
            total_count = vs.Count;
            cancellationToken = new CancellationTokenSource();
            Stopwatch topwatch = Stopwatch.StartNew();
            Progress<ResultData> progress = new Progress<ResultData>();
            progress.ProgressChanged += Progress_ProgressChanged;
            try
            {
                foreach (string s in vs)
                {
                    await Task.Run(() => WebSites.Download(progress, s, cancellationToken.Token));
                }
            }
            catch (OperationCanceledException)
            {
                //Do nothing
            }

            topwatch.Stop();
            TotalTime = topwatch.ElapsedMilliseconds;
            cancellationToken = null;
        }

        public async Task GetResultsParallelAsync(ObservableCollection<string> vs)
        {
            total_count = vs.Count;
            cancellationToken = new CancellationTokenSource();
            Stopwatch topwatch = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();
            Progress<ResultData> progress = new Progress<ResultData>();
            progress.ProgressChanged += Progress_ProgressChanged;

            foreach (string s in vs)
            {
                tasks.Add(Task.Run(() => WebSites.Download(progress, s, cancellationToken.Token)));
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException)
            {
                // Do nothing
            }

            TotalTime = topwatch.ElapsedMilliseconds;
            cancellationToken = null;
        }

        private void Progress_ProgressChanged(object sender, ResultData e)
        {
            ResultDatas.Add(e);
            PercentageCompleted = (ResultDatas.Count * 100) / total_count;
        }
    }
}
