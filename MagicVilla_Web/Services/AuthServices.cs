using Magic_Villa_Utility;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Models.Dto;
using Magic_Villa_Web.Services.IServices;
using Microsoft.Extensions.Configuration;

namespace Magic_Villa_Web.Services
{
    public class AuthServices : BaseServices, IAuthServices
    {
        private readonly IHttpClientFactory _clientFactory;
        private string VillaUrls;
        public AuthServices(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            VillaUrls = configuration.GetValue<string>("ServiceUrls:villaApi");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = VillaUrls + "/api/User/Login"
            });
        }

        public  Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = VillaUrls + "/api/User/RegisterUser"
            });
        }
    }
}
