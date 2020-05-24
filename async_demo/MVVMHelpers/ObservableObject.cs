using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

//Event Design: http://msdn.microsoft.com/en-us/library/ms229011.aspx

namespace async.MVVMHelpers
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            else
            {
                field = value;
                NotifyPropertyChanged(propertyName);
                return true;
            }
        }

        public ObservableObject()
        {

        }
    }
}