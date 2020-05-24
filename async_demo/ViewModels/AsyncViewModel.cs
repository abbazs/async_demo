using async.Helpers;
using async.Models;
using async.MVVMHelpers;
using async.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace async.ViewModels
{
    public class AsyncViewModel : ObservableObject, IBaseViewModel
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

        public AsyncViewModel()
        {
            Title = "Async";
            WebSites.FillCollection(this);
        }

        private async Task GetData(object prop)
        {
            await WebSites.DownloadWebsiteAsync(this);
        }

        //public async Task GetData(object prop)
        //{

        //}
    }
}
