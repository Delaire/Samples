using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DmDemoApp.Services
{


    public abstract class HttpServiceBase
    {
        private static HttpClient _myHttpClient;
        internal static HttpClient MyHttpClient
        {
            get
            {
                if (_myHttpClient == null)
                {
                    _myHttpClient = CreateNewHttpClient();
                }

                return _myHttpClient;
            }
            set
            {
                _myHttpClient = value;
            }
        }

        /// <summary>
        /// Creates a new HTTP client with default settings
        /// </summary>
        /// <returns>The newly created client</returns>
        protected static HttpClient CreateNewHttpClient()
        {
            var factory = SimpleIoc.Default.GetInstance<IHttpClientFactory>();

            return factory.CreateHttpClient();

        }
    }

}
