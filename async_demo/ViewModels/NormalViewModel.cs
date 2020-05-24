using async.Helpers;
using async.Models;
using async.MVVMHelpers;
using async.ViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
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

        private long totalTime;

        public long TotalTime
        {
            get => totalTime;
            set => SetProperty(ref totalTime, value);
        }

        public ObservableCollection<WebsiteData> WebsiteDatas { get; set; }

        public ICommand GetDataCommand => new RelayCommand(prop => GetData(prop));

        private void GetData(object prop)
        {
            WebSites.DownloadWebsite(this);
        }

        public NormalViewModel()
        {
            Title = "Normal";
            WebSites.FillCollection(this);
        }
    }
}
