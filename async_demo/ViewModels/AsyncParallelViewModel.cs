namespace async.ViewModels
{
    public class AsyncParallelViewModel : NormalViewModel
    {
        public AsyncParallelViewModel()
        {
            Title = "Async Parallel";
        }

        protected override void GetData(object prop)
        {
            ResultDataViewModel rdvm = new ResultDataViewModel();
            ResultDataViewModels.Add(rdvm);
            _ = rdvm.GetResultsParallelAsync(Urls);
        }
    }
}
