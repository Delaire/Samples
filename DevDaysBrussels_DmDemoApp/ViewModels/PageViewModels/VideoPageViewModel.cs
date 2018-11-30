using DmDemoApp.ViewModels.EntityViewModels;
using DMVideoPlayer.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DmDemoApp.ViewModels.PageViewModels
{ 

    public class VideoPageViewModel : AppViewModelBase
    {
        private ObservableCollection<VideoItemViewModel> _dataVideoItemsSource;
        public ObservableCollection<VideoItemViewModel> DataVideoItemsSource
        {
            get
            {
                return _dataVideoItemsSource;
            }
            set
            {
                _dataVideoItemsSource = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadRelatedVideoEntites(string videoId)
        {
            DataVideoItemsSource = await DmRelatedService.GetRelatedVideosByVideoId(videoId);
        }
    }
}
