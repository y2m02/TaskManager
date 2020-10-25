using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApp.Extensions;

namespace TaskManagerApp.ApiHelpers
{
    public interface IApiClientService
    {
        Task<TResponse> Get<TResponse>(string path);
        Task<TResponse> Post<TResponse>(string path, object body = null);
        Task Put<TResponse>(string path, object body = null);
        Task<TResponse> Delete<TResponse>(string path);
    }

    public class ApiClient : IApiClientService
    {
        private readonly string _baseApiUrl;

        public ApiClient(IConfiguration configuration)
        {
            _baseApiUrl = configuration.GetSection("API_URL").Value;
        }

        public async Task<TResponse> Get<TResponse>(string path)
        {
            return await Execute<TResponse>(PrepareRequest(HttpMethod.Get, path)).ConfigureAwait(false);
        }

        public async Task<TResponse> Post<TResponse>(string path, object body = null)
        {
            return await SetRequest<TResponse>(HttpMethod.Post, path, body).ConfigureAwait(false);
        }

        public async Task Put<TResponse>(string path, object body = null)
        {
            await SetRequest<TResponse>(HttpMethod.Put, path, body).ConfigureAwait(false);
        }

        public async Task<TResponse> Delete<TResponse>(string path)
        {
            return await Execute<TResponse>(PrepareRequest(HttpMethod.Delete, path)).ConfigureAwait(false);
        }

        private async Task<TResponse> SetRequest<TResponse>(HttpMethod method, string path, object body = null)
        {
            var request = PrepareRequest(method, path);

            AddContent(request, body);

            return await Execute<TResponse>(request).ConfigureAwait(false);
        }

        private static void AddContent(HttpRequestMessage request, object body)
        {
            if (body != null)
            {
                var content = new ByteArrayContent(Encoding.UTF8.GetBytes(body.ToJson()));

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                request.Content = content;
            }
        }

        private HttpRequestMessage PrepareRequest(HttpMethod method, string path)
        {
            var request = new HttpRequestMessage(method, new Uri($"{_baseApiUrl}{path}"));

            return AddHeaders(request);
        }

        private static HttpRequestMessage AddHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("ContentType", "application/json");
            request.Headers.Add("Accept", "application/json");

            return request;
        }

        private static async Task<TResponse> Execute<TResponse>(HttpRequestMessage request)
        {
            var response = await new HttpClient().SendAsync(request).ConfigureAwait(false);

            var rawResponse = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return rawResponse.DeserializeJson<TResponse>();
            }

            var statusCode = (int)response.StatusCode;

            throw statusCode switch
            {
                _ => new ValidationException(rawResponse)
            };
        }
    }
}