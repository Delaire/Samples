using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.System;
using Windows.System.Profile;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;
using NewShellDm.Pages;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewShellDm
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Page
    {
        public static Shell Current { get; private set; }

        private bool _isPaneOpen;
        private NavViewModel _currentSample;

        private bool _hamburgerMenuClosing = false;


        private float _defaultShowAnimationDuration = 300;
        //  private XamlRenderService _xamlRenderer = new XamlRenderService();
        private bool _lastRenderedProperties = true;
        private ThreadPoolTimer _autocompileTimer;
        private bool _xamlCodeRendererSupported = false;
        private List<NavViewModel> NavigationViews { get; set; }


        public Shell()
        {
            this.InitializeComponent();

            Current = this;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationFrame.Navigating += NavigationFrame_Navigating;

            NavigationViews = new List<NavViewModel>();

            NavigationViews.Add(new NavViewModel() { Name = "Red view", PageView = "RedView" });
            NavigationViews.Add(new NavViewModel() { Name = "Blue view", PageView = "BlueView" });
            NavigationViews.Add(new NavViewModel() { Name = "Orange view", PageView = "OrangeView" });

            HamburgerMenu.ItemsSource = NavigationViews;

            NavigationFrame.Navigate(typeof(BlueView));
        }

        private async void NavigationFrame_Navigating(object sender, NavigatingCancelEventArgs navigationEventArgs)
        {
            if (navigationEventArgs.Parameter != null && NavigationViews != null)
            {
                _currentSample = NavigationViews.Where(a => a.Name == navigationEventArgs.Parameter).FirstOrDefault();
            }
            else
            {
                _currentSample = null;
            }

            await SetHamburgerMenuSelection();
        }



        private async Task SetHamburgerMenuSelection()
        {
            if (_currentSample != null)
            {
                if (HamburgerMenu.Items.Contains(_currentSample))
                {
                    HamburgerMenu.SelectedItem = _currentSample;
                    HamburgerMenu.SelectedOptionsItem = null;
                }
            }
            else
            {
                try
                {
                    HamburgerMenu.SelectedItem = null;
                    HamburgerMenu.SelectedOptionsIndex = 0;
                }
                catch (Exception e)
                {
                    //fail in silence
                }

            }
        }



        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is NavViewModel category)
            {
                var sel = e.ClickedItem as NavViewModel;

                NavigateToView(sel);
            }
        }

        private void NavigateToView(NavViewModel sample)
        {
            var pageType = sample.Name;

            if (pageType != null)
            {
                // InfoAreaPivot.Items.Clear();
                if (sample.PageView.Equals("BlueView"))
                {
                    NavigationFrame.Navigate(typeof(BlueView), sample.Name);
                }

                if (sample.PageView.Equals("RedView"))
                {
                    NavigationFrame.Navigate(typeof(RedView), sample.Name);
                }

                if (sample.PageView.Equals("OrangeView"))
                {
                    NavigationFrame.Navigate(typeof(OrangeView), sample.Name);
                }

            }
        }

        public void NavigateToViewV2(string Name)
        {
            if (Name != null)
            {
                if (Name == "page 1")
                {
                    NavigationFrame.Navigate(typeof(Page1));
                }
                if (Name == "page 2")
                {
                    NavigationFrame.Navigate(typeof(Page2));
                }
            }
        }



        private void VerticalSamplePickerListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            HamburgerMenu.IsPaneOpen = false;
            //NavigateToSample(e.ClickedItem as Sample);
        }

        private void Expander_Expanded(object sender, EventArgs e)
        {
            var expanders = HamburgerMenu.FindDescendants<Expander>();
            foreach (var expander in expanders)
            {
                if (expander != sender)
                {
                    expander.IsExpanded = false;
                }
            }
        }

        private void HamburgerButtonClicked(object sender, RoutedEventArgs e)
        {
            HamburgerMenu.IsPaneOpen = !HamburgerMenu.IsPaneOpen;
        }



    }

}
