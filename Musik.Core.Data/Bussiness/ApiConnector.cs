using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Musik.Core.Portable.Bussiness
{
    internal class ApiConnector
    {
        public T Get<T>(string url)
        {
            T returnedData;
            var client = GetHttpClient();
            var response = client.GetAsync(url).Result;            

            if (response.IsSuccessStatusCode)
            {
                var yourcustomobjects = response.Content.ReadAsStringAsync().Result;
                returnedData = JsonConvert.DeserializeObject<T>(yourcustomobjects);
            }
            else
            {
                var message = response.StatusCode.ToString() + " : " + response.ReasonPhrase;
                throw new Exception(message);
            }

            return returnedData;
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
