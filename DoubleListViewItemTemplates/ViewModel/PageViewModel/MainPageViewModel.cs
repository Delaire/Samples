using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using GalaSoft.MvvmLight;
using DoubleListViewItemTemplates.Model;
using DoubleListViewItemTemplates.SegmentViewModel;

namespace DoubleListViewItemTemplates.PageViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool _isLoading = false;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");

            }
        }

        private string _title;

        public string Title
        {

            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        public MainPageViewModel()
        {
            Init();
        }

        private ObservableCollection<BaseSegementEntityViewModel> _data;
        public ObservableCollection<BaseSegementEntityViewModel> MyData
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged("MyData");
            }
        }

        private async Task Init()
        {
            //horizontal // vertical  
            MyData = new ObservableCollection<BaseSegementEntityViewModel>();

            //circle list
            var CircleList = new List<SmallItemViewModel>();

            for (int i = 0; i < 10; i++)
            {
                CircleList.Add(new SmallItemViewModel(string.Format("title circle {0}", i ), ItemTemplateType.CircleUserItem));
            }

            //square list
            var SquareList = new List<SmallItemViewModel>();

            for (int i = 0; i < 10; i++)
            {
                SquareList.Add(new SmallItemViewModel(string.Format("title square {0}", i), ItemTemplateType.SquareUserItem));
            }


            //data
            MyData.Add(NeonExploreTemplateEngine(SquareList, SegmentType.HorizontalSegment));        
            MyData.Add(NeonExploreTemplateEngine(CircleList.Take(3).ToList(), SegmentType.VerticalSegment));
            MyData.Add(NeonExploreTemplateEngine(CircleList, SegmentType.HorizontalSegment));
            MyData.Add(NeonExploreTemplateEngine(SquareList.Take(3).ToList(), SegmentType.VerticalSegment));
            MyData.Add(NeonExploreTemplateEngine(SquareList, SegmentType.HorizontalSegment));
            MyData.Add(NeonExploreTemplateEngine(CircleList, SegmentType.HorizontalSegment));
            MyData.Add(NeonExploreTemplateEngine(CircleList.Take(3).ToList(), SegmentType.VerticalSegment));

        }

        public BaseSegementEntityViewModel NeonExploreTemplateEngine(List<SmallItemViewModel> item, SegmentType type)
        {
            if (type == SegmentType.HorizontalSegment)
            {
                return new HorizontalSegmentViewModel(item);
            }
            else
            {
                return (new VerticalSegmentViewModel(item));
            }          
        }



    }

}
