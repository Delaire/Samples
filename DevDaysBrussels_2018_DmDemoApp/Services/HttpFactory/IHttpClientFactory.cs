using System.Net.Http;

namespace DmDemoApp.Services
{
    /// <summary>
    /// Interface defining a factory for creating HttpClients for use in Http services. For use with unit testing.
    /// </summary>
    public interface IHttpClientFactory
    {
        /// <summary>
        /// Create an HTTP client with default settings
        /// </summary>
        /// <returns></returns>
        HttpClient CreateHttpClient();
    }

}
