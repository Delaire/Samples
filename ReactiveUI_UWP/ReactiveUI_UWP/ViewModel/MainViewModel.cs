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
        //My item loader
        public ReactiveCommand<int, IEnumerable<MyItem>> LoadMyItems
        {
            get;
        }

        //my selected item
        MyItem m_selectedItem;
        public MyItem MySelectedItem
        {
            get { return m_selectedItem; }
            set { RaisePropertyChanged("MySelectedItem"); }
        }

        //my service
        private MyItemService myItemService;

        public MainViewModel()
        {
            //init the serivce
            myItemService = new MyItemService();

            //getting items using the CreateFromObservable
            LoadMyItems = ReactiveCommand
                .CreateFromObservable((int index) =>
                    myItemService.GetUpcomingItems(index));

            //when the vm is activated bind the properties
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                //init
                SelectedItem = null;

                //when SelectedItem has a value go to page
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
