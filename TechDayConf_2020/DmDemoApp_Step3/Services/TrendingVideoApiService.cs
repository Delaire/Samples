using DmDemoApp.Models;
using DmDemoApp.Services.HttpService;
using DmDemoApp.ViewModels.EntityViewModels;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmDemoApp.Services
{
    public class TrendingVideoApiService
    {
        private const string trendingApiCAll = "https://api.dailymotion.com/videos?fields=channel.name,id,thumbnail_url,title,&sort=trending&page={0}";
        public async Task<ObservableCollection<VideoItemViewModel>> GetTrendingVideos(int page = 1)
        {
            //url
            string url = string.Format(trendingApiCAll, page);

            //http call
            var result = await SimpleIoc.Default.GetInstance<IHttpService>().MakeHttpRequest<RootResponseObject<VideoItems>>(url);

            //object
            var ocVideo = new ObservableCollection<VideoItemViewModel>();

            //converting
            result.list.ForEach(a => ocVideo.Add(new VideoItemViewModel(a)));

            //return
            return ocVideo;
        }
    }
}
