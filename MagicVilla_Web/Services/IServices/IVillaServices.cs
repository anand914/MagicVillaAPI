﻿using Magic_Villa_Web.Models.Dto;

namespace Magic_Villa_Web.Services.IServices
{
    public interface IVillaServices
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaCreateDto dto, string token);
        Task<T> UpdateAsync<T>(VillaUpdateDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);


    }
}
