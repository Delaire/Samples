using GalaSoft.MvvmLight;
using ReactiveUI;
using ReactiveUI_UWP.Model;
using ReactiveUI_UWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI_UWP.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public ReactiveCommand<int, IEnumerable<MyItem>> LoadMyItems
        {
            get;
        }

        MyItem m_selectedItem;
        public MyItem MySelectedItem
        {
            get { return m_selectedItem; }
            set { RaisePropertyChanged("MySelectedItem"); }
        }

        private MyItemService myItemService;

        public MainViewModel()
        {
            //getting items 
            LoadMyItems = ReactiveCommand
                .CreateFromObservable((int index) =>
                    myItemService.GetUpcomingItems(index));

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                SelectedItem = null;

                this
                    .WhenAnyValue(x => x.SelectedItem)
                    .Where(x => x != null)
                    .Subscribe(x => LoadSelectedPage(x))
                    .DisposeWith(disposables);             

            });

        }


        public void LoadSelectedPage(MyItem item)
        {
            //load this page
        }
    }
}
