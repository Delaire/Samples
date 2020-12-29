using DmDemoApp.Services;
using DmDemoApp.Services.HttpService;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmDemoApp.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {

            if (!SimpleIoc.Default.IsRegistered<IHttpClientFactory>())
                SimpleIoc.Default.Register<IHttpClientFactory, HttpClientFactory>();

            if (!SimpleIoc.Default.IsRegistered<IHttpService>())
                SimpleIoc.Default.Register<IHttpService, HttpService>();

        }
    }
}
