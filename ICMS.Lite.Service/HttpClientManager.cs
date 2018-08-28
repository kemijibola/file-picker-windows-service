using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Lite.Service
{
    public class HttpClientManager
    {
        private HttpClient _client;
        static string _url;
        public HttpClientManager()
        {
            _client = new HttpClient();
            _url = FilePicker.GetConfigValue("APIPath");
            _client.BaseAddress = new Uri(_url);

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        private class InstanceHolder
        {
            public static HttpClientManager Instance = new HttpClientManager();
        }
        public static HttpClientManager GetInstance()
        {

            return InstanceHolder.Instance;
        }

        public HttpClient GetClient()
        {
            return _client;
        }
    }
}
