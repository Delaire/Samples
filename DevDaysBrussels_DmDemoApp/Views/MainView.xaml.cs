using DmDemoApp.ViewModels.EntityViewModels;
using DmDemoApp.ViewModels.PageViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DmDemoApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainPageViewModel ViewModel => this.DataContext as MainPageViewModel;
        public MainView()
        {
            this.InitializeComponent();
            Loaded += MainView_Loaded;
        }

        private async void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadVideoEntites();
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e?.ClickedItem.GetType() == typeof(VideoItemViewModel))
            {
                var item = (VideoItemViewModel)e.ClickedItem;

                item.OnItemClickNavigation.Execute(null);
            }

        }



    }
}
