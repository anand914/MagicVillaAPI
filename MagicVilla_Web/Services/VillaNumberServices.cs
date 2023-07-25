using Magic_Villa_Utility;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Models.Dto;
using Magic_Villa_Web.Services.IServices;

namespace Magic_Villa_Web.Services
{
    public class VillaNumberServices : BaseServices, IVillaNumberServices
    {
        private readonly IHttpClientFactory _clientFactory;
        private string VillaUrls;

        public VillaNumberServices(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            VillaUrls = configuration.GetValue<string>("ServiceUrls:villaApi");
        }

        public Task<T> CreateAsync<T>(VillaNoCreatedDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = VillaUrls + "/api/v1/VillaNumberAPI/CreateVillNumber",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = VillaUrls + "/api/v1/VillaNumberAPI/DeleteVillaNumber/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = VillaUrls + "/api/v1/VillaNumberAPI/GetAllVillNumber",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = VillaUrls + "/api/v1/VillaNumberAPI/GetVillNumber/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillNoUpdatedDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = VillaUrls + "/api/v1/VillaNumberAPI/UpdateVillaNumber/" + dto.VillaNo,
                Token = token
            });
        }
    }
}
