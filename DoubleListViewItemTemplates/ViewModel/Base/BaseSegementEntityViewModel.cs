using DoubleListViewItemTemplates.Annotations;
using DoubleListViewItemTemplates.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;


namespace DoubleListViewItemTemplates
{
    public class BaseSegementEntityViewModel : INotifyPropertyChanged
    {
        public SegmentType ItemType { get; set; }
        public string title { get; set; }

        private ObservableCollection<BaseItemEntityViewModel> _dmItemSource;
        public ObservableCollection<BaseItemEntityViewModel> MyItemSource
        {
            get
            {
                return _dmItemSource;
            }
            set
            {
                _dmItemSource = value;
                RaisePropertyChanged(() => MyItemSource);
            }
        }         

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var data = GetPropertyName(property);
            RaisePropertyChanged(data);
        }

        public void RaisePropertyChanged(string whichProperty)
        {
            var changedArgs = new PropertyChangedEventArgs(whichProperty);

            if (PropertyChanged != null)
            {
                PropertyChanged(this, changedArgs);
            }
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}