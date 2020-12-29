using DoubleListViewItemTemplates.Helpers;
using DoubleListViewItemTemplates.PageViewModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DoubleListViewItemTemplates
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //our second vertical listview
            AppListView.Loaded += (sender, e) =>
            {
                //getting scrollview
                ScrollViewer scrollViewer = AppListView.GetScrollViewer(); //Extension method
                if (scrollViewer != null)
                {
                    scrollViewer.ViewChanging += ScrollViewerListView_ViewChanging;
                }
            };
        }

        private void ScrollViewerListView_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            //because our first item has a height of 200
            double AdditionOffSetToAdd = -200;

            MyItemPositions = AppListView.GetAllItemsPositions();


            if (MyItemPositions != null)
            {
                var currentVerticalPosition = e.FinalView.VerticalOffset + AdditionOffSetToAdd;

                double itemIndex = MyItemPositions.Values.Where(a => a >= currentVerticalPosition).FirstOrDefault();

                CurrentVisibleItemIndex = MyItemPositions.FirstOrDefault(x => x.Value == itemIndex).Key;

                //Debug.WriteLine($"CurrentVisibleItemIndex :{CurrentVisibleItemIndex}");
                //Debug.WriteLine($"previousItemIndex :{previousItemIndex}");

                if (previousItemIndex != CurrentVisibleItemIndex)
                {
                    //update previous
                    previousItemIndex = CurrentVisibleItemIndex;

                    //Debug.WriteLine("VerticalOffset :{0}", e.FinalView.VerticalOffset);
                    //Debug.WriteLine("possible visible item {0}", CurrentVisibleItemIndex);

                    CurrentItemIndexChangedCommand();
                }
            }
        }

        public event EventHandler CurrentItemIndexChanged;
        private void CurrentItemIndexChangedCommand()
        {
            CurrentItemIndexChanged?.Invoke(this, new EventArgs());
        }

        public MainPageViewModel ViewModel => this.DataContext as MainPageViewModel;

        public Dictionary<int, double> MyItemPositions { get;  set; }
        public int CurrentVisibleItemIndex { get;  set; }
        public int previousItemIndex { get;  set; }
    }
}
