using DmDemoApp.ViewModels.EntityViewModels;
using DmDemoApp.ViewModels.PageViewModels;
using DmVideoPlayer;
using DMVideoPlayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class VideoView : Page
    {
        public VideoPageViewModel ViewModel => this.DataContext as VideoPageViewModel;
        public string VideoId { get; set; }
        public static DmPlayerController dmPlayerController;
        public VideoView()
        {
            this.InitializeComponent();

            //init player
            InitHtml5();

            Loaded += VideoView_Loaded;

            //fixing size for grid in button
            var bounds = GetActualSize();
            MyPlayerGrid.Width = bounds.Width;
            MyPlayerGrid.Height = bounds.Height - 250;

            if (App.IsXbox)
            {
                //xbox
                Windows.UI.Xaml.Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            }
        }

        public Size GetActualSize()
        {
            if (Window.Current?.Content != null
                && Window.Current?.Bounds != null)
            {
                return new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height);
            }
            return new Size(0, 0);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            VideoId = e.Parameter as string;
        }

        private void VideoView_Loaded(object sender, RoutedEventArgs e)
        {
            //load html5 player
            LoadVideo(VideoId);

            //get related videos
            ViewModel.LoadRelatedVideoEntites(VideoId);
        }



        private void DmPlayerController_OnDmWebViewMessageUpdated()
        {
            if (dmPlayerController.DmWebViewMessage.Key == null)
            {
                return;
            }

            //Debug.WriteLine(dmPlayerController.DmWebViewMessage);

        }

        private void InitHtml5()
        {
            var parameters = new Dictionary<string, string>();

            parameters["auto"] = "true";

            if (App.IsXbox)
                parameters["controls"] = "0";
            else
                parameters["controls"] = "1";

            //init
            dmPlayerController = new DmPlayerController();

            var accessToken = "";// "myAccessToken";

            //init the DMVideoPlayer
            dmPlayerController.Init(accessToken, parameters);

            if (!MyPlayerGrid.Children.Contains(dmPlayerController.DmVideoPlayer))
            {
                //adding DmVideoPlayer to the page
                MyPlayerGrid.Children.Add(dmPlayerController.DmVideoPlayer);
            }

            dmPlayerController.OnDmWebViewMessageUpdated += DmPlayerController_OnDmWebViewMessageUpdated;
        }

        private async Task LoadVideo(string videoId = "xl1km0")
        {
            var parameters = new Dictionary<string, string>();

            parameters["autoplay"] = "true";

            //this will allow the player to auto next to the next related video
            parameters["queue-enable"] = "false";

            var accessToken = "";// "myAccessToken";

            //init the DMVideoPlayer
            dmPlayerController.Load(videoId, accessToken, parameters);
        }


        private void HorizontalGridView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e?.ClickedItem.GetType() == typeof(VideoItemViewModel))
            {
                var item = (VideoItemViewModel)e.ClickedItem;

                LoadVideo(item.VideoId);
            }

        }




        #region Key down

        private async void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            if (sender != null)
            {
                //Debug.WriteLine("CoreWindow_KeyDown : " + e.VirtualKey);
                switch (e.VirtualKey)
                {

                    //case VirtualKey.GamepadY:
                    //Y TO SHEARCH?
                    //break;

                    //case VirtualKey.GamepadX:
                    //perform an action
                    //    break;

                    case VirtualKey.GamepadRightThumbstickRight:
                        {
                            //PlayerPlaybackService.Instance.SeekInVideo(30);
                            dmPlayerController.Seek(30);
                        }
                        break;
                    case VirtualKey.GamepadRightThumbstickLeft:
                        {
                            //PlayerPlaybackService.Instance.SeekInVideo(-30);
                            dmPlayerController.Seek(-30);
                        }
                        break;

                    default:
                        break;
                }

                e.Handled = false;
            }
        }


        #endregion
    }
}
