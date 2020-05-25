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