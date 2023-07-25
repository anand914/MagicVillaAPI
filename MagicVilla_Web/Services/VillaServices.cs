using Magic_Villa_Utility;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Models.Dto;
using Magic_Villa_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Magic_Villa_Web.Services
{
    public class VillaServices: BaseServices, IVillaServices
    {
        private readonly IHttpClientFactory _clientFactory;
        private string VillaUrls;
        public VillaServices(IHttpClientFactory clientFactory,IConfiguration configuration): base(clientFactory)
        {
            _clientFactory = clientFactory;
            VillaUrls = configuration.GetValue<string>("ServiceUrls:villaApi");
        }
        public Task<T> CreateAsync<T>(VillaCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = VillaUrls + "/api/v1/VillaAPI/CreateVilla",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = VillaUrls + "/api/v1/VillaAPI/UpdateVilla/" + dto.Id,
                Token=token
            });
        }

        public  Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = VillaUrls + "/api/v1/VillaAPI/DeleteVilla/" + id,
                Token=token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = VillaUrls + "/api/v1/VillaAPI/GetVillas",
                Token=token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = VillaUrls + "/api/v1/VillaAPI/GetVilla/" + id,
                Token=token
            });
        }
    }
}
