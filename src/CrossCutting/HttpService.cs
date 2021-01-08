using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crawler.CrossCutting
{
    public static class HttpService
    {
        private static HttpClient _httpClient;
        public static HttpClient GethttpClient()
        {
            if (_httpClient == null)
                _httpClient = new HttpClient();
            return _httpClient;
        }
        public static async Task<string> HttpPostWithParameters(string url, Dictionary<string, string> parameters)
        {
            var client = GethttpClient();
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(url, encodedContent);
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
        public static async Task<string> HttpGet(string url)
        {
            var client = GethttpClient();
            var response = await client.GetAsync(url);
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}
