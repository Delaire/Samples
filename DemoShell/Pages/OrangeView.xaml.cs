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

namespace NewShellDm.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrangeView : Page
    {
        public OrangeView()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_1OnClick(object sender, RoutedEventArgs e)
        {
            Shell.Current.NavigateToViewV2("page 1");
        }
        private void ButtonBase_2OnClick(object sender, RoutedEventArgs e)
        {
            Shell.Current.NavigateToViewV2("page 2");
        }
        private void ButtonBase_3OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
