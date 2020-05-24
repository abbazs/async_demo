
using async.MVVMHelpers;

namespace async.Models
{
    public class WebsiteData : ObservableObject
    {
        private string webSiteUrl;

        public string WebsiteUrl
        {
            get => webSiteUrl;
            set => SetProperty(ref webSiteUrl, value);
        }

        private string webSiteData;

        public string WebSiteData
        {
            get => webSiteData;
            set => SetProperty(ref webSiteData, value, nameof(DataLength));
        }

        public int DataLength => WebSiteData?.Length ?? 0;

        private long timeTaken;

        public long TimeTaken
        {
            get { return timeTaken; }
            set => SetProperty(ref timeTaken, value);
        }
    }
}