using async.Helpers;
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
        private string title;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public ObservableCollection<string> Urls { get; set; }

        public ObservableCollection<ResultDataViewModel> ResultDataViewModels { get; set; }

        public ICommand GetDataCommand => new RelayCommand(prop => GetData(prop));
        public ICommand ClearResultsCommand => new RelayCommand(prop => ClearResults());
        public ICommand StopAllCommand => new RelayCommand(prop => StopAll());

        private void StopAll()
        {
            foreach (ResultDataViewModel rvm in ResultDataViewModels)
            {
                rvm.CancelCommand.Execute(null);
            }
        }

        private void ClearResults()
        {
            StopAll();
            ResultDataViewModels.Clear();
        }

        protected virtual void GetData(object prop)
        {
            ResultDataViewModel rdvm = new ResultDataViewModel();
            ResultDataViewModels.Add(rdvm);
            rdvm.GetResults(Urls);
        }

        public NormalViewModel()
        {
            Title = "Normal";
            WebSites.FillCollection(this);
            ResultDataViewModels = new ObservableCollection<ResultDataViewModel>();
        }
    }
}
