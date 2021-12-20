using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MangoWeb.Models;
using MangoWeb.Services.IServices;
using Newtonsoft.Json;

namespace MangoWeb.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto ResponseModel { get; set; }
        public IHttpClientFactory HttpClientFactory { get; set; }

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            ResponseModel = new ResponseDto();
            HttpClientFactory = httpClientFactory;
        }
        
        
        
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClientFactory.CreateClient("MangoAPI");
                
                client.DefaultRequestHeaders.Clear();

                var apiResponse = await client.SendAsync(CreateHttpRequestMessage(apiRequest));
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(apiContent);
            }
            catch (Exception e)
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(GetErrorResponse(e)));
            }
        }

        private static HttpRequestMessage CreateHttpRequestMessage(ApiRequest apiRequest)
        {
            var message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);
            message.Content = GetMessageContent(apiRequest);
            message.Method = GetMessageMethod(apiRequest);
            return message;
        }

        private static ResponseDto GetErrorResponse(Exception e) =>
            new ResponseDto
            {
                DisplayMessage = "Error",
                ErrorMessages = new List<string>{ Convert.ToString(e.Message)},
                IsSuccess = false
            };

        private static StringContent GetMessageContent(ApiRequest apiRequest) =>
            apiRequest.Data == null ? 
                null
                : new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");

        private static HttpMethod GetMessageMethod(ApiRequest apiRequest)
        {
            switch (apiRequest.ApyType)
            {
                case SD.ApiType.POST:
                     return HttpMethod.Post;
                case SD.ApiType.DELETE:
                    return HttpMethod.Delete;
                case SD.ApiType.PUT:
                    return HttpMethod.Put;
                default:
                    return HttpMethod.Get;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}