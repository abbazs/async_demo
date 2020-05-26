namespace async.ViewModels
{
    public class AsyncParallelForeachViewModel : NormalViewModel
    {
        public AsyncParallelForeachViewModel()
        {
            Title = "Async Parallel.Foreach";
            Urls.Add("https://www.reddit.com");
            Urls.Add("https://www.wikipedia.com");
            Urls.Add("https://www.similarweb.com");
        }

        protected override void GetData(object prop)
        {
            ResultDataViewModel rdvm = new ResultDataViewModel();
            ResultDataViewModels.Add(rdvm);
            _ = rdvm.GetResultsParallelForeachAsync(Urls);
        }
    }
}
