using DmDemoApp.Models;
using DmDemoApp.Services.HttpService;
using DmDemoApp.ViewModels.EntityViewModels;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DmDemoApp.Services
{
    public class RelatedVideoApiService
    {
        private const string relatedApiCAll = "https://api.dailymotion.com/video/{0}/related?fields=channel.name,id,thumbnail_url,title,owner.username";
        public async Task<ObservableCollection<VideoItemViewModel>> GetRelatedVideosByVideoId(string xid)
        {
            //url
            string url = string.Format(relatedApiCAll, xid);           

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
