using DmDemoApp.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DmDemoApp.Services.HttpService
{
    public interface IHttpService
    {
        Task<T> MakeHttpRequest<T>(string httpUrl);
    }

    public class HttpService : HttpServiceBase, IHttpService
    {
        public async Task<T> MakeHttpRequest<T>(string httpUrl)
        {
            if (httpUrl == null)
            {
                throw new ArgumentNullException("httpUrl");
            }

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, httpUrl);

            HttpResponseMessage response = null;
            try
            {
                response = await MyHttpClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw new ApiException("MyHttpClient has faild", ex, ApiExceptionType.InvalidServerResponse);
            }



            if (response == null)
            {
                throw new ApiException("The server did not return a response.", ApiExceptionType.NoServerResponse);
            }

            if (response.IsSuccessStatusCode)
            {
                if (response.Content == null)
                {
                    throw new ApiException("The server returned an empty response.", ApiExceptionType.NoServerResponse);
                }

                var responseJson = await response.Content.ReadAsStringAsync();

                if (String.IsNullOrEmpty(responseJson))
                {
                    throw new ApiException("The server returned an empty response.", ApiExceptionType.NoServerResponse);
                }

                object responseObject;
                try
                {
                    responseObject = JsonConvert.DeserializeObject<T>(responseJson);
                }
                catch (Exception ex)
                {
                    throw new ApiException("The server did not return a response in the expected format.", ex, ApiExceptionType.InvalidServerResponse);
                }

                //cast
                return (T)responseObject;
            }
            else
            {
                var errorMsg = String.Format("The server returned status code {0} with error {1}",
                    response.StatusCode, response.ReasonPhrase);

                throw new ApiException(errorMsg, ApiExceptionType.ServerError);
            }

            return default(T);
        }
    }
}
