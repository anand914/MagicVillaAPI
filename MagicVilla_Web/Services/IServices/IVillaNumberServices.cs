using Magic_Villa_Web.Models.Dto;

namespace Magic_Villa_Web.Services.IServices
{
    public interface IVillaNumberServices
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaNoCreatedDto dto, string token);
        Task<T> UpdateAsync<T>(VillNoUpdatedDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
