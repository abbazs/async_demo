using async.Models;
using async.MVVMHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace async.ViewModels.Interfaces
{
    public interface IBaseViewModel
    {
        public string Title { get; set; }
        public ObservableCollection<string> Urls { get; set; }
    }
}
