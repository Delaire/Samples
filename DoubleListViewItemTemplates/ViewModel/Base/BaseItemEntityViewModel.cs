using DoubleListViewItemTemplates.Annotations;
using DoubleListViewItemTemplates.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
 

namespace DoubleListViewItemTemplates
{
    public abstract class BaseItemEntityViewModel : INotifyPropertyChanged
    {
        private ItemTemplateType _itemTemplate;
        public ItemTemplateType ItemTemplate
        {
            get
            {
                return _itemTemplate;
            }
            set
            {
                _itemTemplate = value;
                RaisePropertyChanged(() => ItemTemplate);
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            //TODO:IMPROVE
            var name = property.Name;
            OnPropertyChanged(name);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}