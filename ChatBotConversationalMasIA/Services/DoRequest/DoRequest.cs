using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Services.AuthenticationManager;

namespace Services.DoRequest
{
    public class DoRequest: IDoRequest{

        private readonly IHttpClientFactory _clientFactory;
        public DoRequest(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> Get<T>(string path, IDictionary<string, string> headers) {
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                    
                }
            }
            
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(responseStream);
        }

        public async Task<T> Post<T>(string path, IDictionary<string, string> headers, string body, string type = "application/json") {
            var content = new StringContent(body, Encoding.UTF8, type);

            foreach (var header in headers)
            {
                content.Headers.Add(header.Key, header.Value);
            }

            var client = _clientFactory.CreateClient();

            var response = await client.PostAsync(path, content);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                    
                }
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(responseStream);
        }

        public async Task<T> Post<T>(string path, IDictionary<string, string> headers, IDictionary<string, string> form)
        {
            var req = new FormUrlEncodedContent(form);
            foreach (var header in headers)
            {
                req.Headers.Add(header.Key, header.Value);
            }

            var client = _clientFactory.CreateClient();

            var response = await client.PostAsync(path, req);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                    
                }
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(responseStream);
        }
        
    }
}