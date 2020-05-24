using async.Models;
using async.MVVMHelpers;
using System.Collections.ObjectModel;

namespace async.ViewModels.Interfaces
{
    public interface IBaseViewModel
    {
        public long TotalTime { get; set; }
        public string Title { get; set; }
        public ObservableCollection<WebsiteData> WebsiteDatas { get; set; }
    }
}
