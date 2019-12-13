using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleChat.App.Mobile.Models
{
    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Set(ref field, value, null, propertyName);
        }

        protected void Set<T>(ref T field, T value, Action callback, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                callback?.Invoke();
            }
        }
    }
}