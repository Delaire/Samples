using DmDemoApp.ViewModels.EntityViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DmDemoApp.ViewModels.PageViewModels
{
    public class MainPageViewModel : AppViewModelBase
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

        public async Task LoadVideoEntites()
        {
            DataVideoItemsSource = await DmTrendingService.GetTrendingVideos();
        }

    }
}
