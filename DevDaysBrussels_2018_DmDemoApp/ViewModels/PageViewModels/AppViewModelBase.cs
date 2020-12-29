using DmDemoApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DmDemoApp.ViewModels.PageViewModels
{
    public class AppViewModelBase : INotifyPropertyChanged
    {         
        private TrendingVideoApiService _dmTrendingService = new TrendingVideoApiService();
        public TrendingVideoApiService DmTrendingService
        {
            get
            {
                return _dmTrendingService;
            }
            set
            {
                _dmTrendingService = value;
                OnPropertyChanged("DmTrendingService");
            }
        }

        private RelatedVideoApiService _dmRelatedService = new RelatedVideoApiService();
        public RelatedVideoApiService DmRelatedService
        {
            get
            {
                return _dmRelatedService;
            }
            set
            {
                _dmRelatedService = value;
                OnPropertyChanged("DmRelatedService");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        protected virtual bool Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
    }
}
