namespace async.ViewModels
{
    public class ParallelForeachViewModel : NormalViewModel
    {
        public ParallelForeachViewModel()
        {
            Title = "Parallel.Foreach";
        }

        protected override void GetData(object prop)
        {
            ResultDataViewModel rdvm = new ResultDataViewModel();
            ResultDataViewModels.Add(rdvm);
            rdvm.GetResultsParallelForeach(Urls);
        }
    }
}