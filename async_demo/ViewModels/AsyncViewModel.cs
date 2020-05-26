namespace async.ViewModels
{
    public class AsyncViewModel : NormalViewModel
    {
        public AsyncViewModel()
        {
            Title = "Async";
        }

        protected override void GetData(object prop)
        {
            ResultDataViewModel rdvm = new ResultDataViewModel();
            ResultDataViewModels.Add(rdvm);
            _ = rdvm.GetResultsAsync(Urls);
        }
    }
}
