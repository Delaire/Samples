using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PopupAnimationsSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //show
            PopupFromBottom.IsOpen = true;
        }

        private async void OpenAndClose_Click(object sender, RoutedEventArgs e)
        {
            //show
            PopupFromBottom.IsOpen = true;


            //wait 3 sec
            await Task.Delay(TimeSpan.FromSeconds(3));


            //close 
            PopupFromBottom.IsOpen = false;
        }

        private void OpenStoryboard_Click(object sender, RoutedEventArgs e)
        {
            PopInStoryboard.Begin();
        }

        private void CloseStoryboard_Click(object sender, RoutedEventArgs e)
        {
            PopOutStoryboard.Begin();
        }
    }
}
