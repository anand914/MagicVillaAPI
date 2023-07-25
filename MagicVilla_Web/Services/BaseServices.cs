using AutoMapper.Internal;
using Magic_Villa_Utility;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Magic_Villa_Web.Services
{
    public class BaseServices : IBaseServices
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpclient { get; set; }
        public BaseServices(IHttpClientFactory httpclient)
        {
            this.httpclient = httpclient;
            this.responseModel = new();
        }
        public async Task<T> SendAsync<T>(APIRequest apirequest)
        {
            try
            {
                var client = httpclient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apirequest.Url);
                if (apirequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apirequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apirequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiresponse = null;
                if (!string.IsNullOrEmpty(apirequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apirequest.Token);
                }
                apiresponse = await client.SendAsync(message);

                var apiContent = await apiresponse.Content.ReadAsStringAsync();
                try
                {
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (ApiResponse != null && (apiresponse.StatusCode == System.Net.HttpStatusCode.BadRequest
                        || apiresponse.StatusCode == System.Net.HttpStatusCode.NotFound))
                    {
                        ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        ApiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                }
                catch (Exception ex)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse()
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
