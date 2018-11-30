using DmDemoApp.Models;
using DmDemoApp.Views;
using DMVideoPlayer.Annotations;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DmDemoApp.ViewModels.EntityViewModels
{
    public class VideoItemViewModel : INotifyPropertyChanged
    {
        public VideoItems Video { get; set; }
        public VideoItemViewModel(VideoItems video)
        {
            this.Video = video;
        }

        public string VideoId => Video?.id;
        //public string ThumbnailUri => Video?.thumbnail_url;
        public string Title => Video?.title;
        public string By => Video?.channelName;


        public Uri ThumbnailUri
        {
            get
            {
                if (Video?.thumbnail_url != null)
                {
                    return new Uri(Video?.thumbnail_url);
                }

                return null;
            }
        }

        public ICommand OnItemClickNavigation
        {
            get
            {
                return new RelayCommand(NavigateToItemObject);
            }
        }

        private void NavigateToItemObject()
        {
            Frame rootFrame = Window.Current.Content as Frame;

            var Arguments = VideoId;

            rootFrame.Navigate(typeof(VideoView), Arguments);
        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
