using async.MVVMHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace async.Models
{
    public class ResultData : ObservableObject
    {
        private string url;

        public string Url
        {
            get => url;
            set => SetProperty(ref url, value);
        }

        public int dataLength;

        public int DataLength
        {
            get => dataLength;
            set => SetProperty(ref dataLength, value);
        }

        private long timeTaken;

        public long TimeTaken
        {
            get { return timeTaken; }
            set => SetProperty(ref timeTaken, value);
        }

        private string status;

        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
    }
}
