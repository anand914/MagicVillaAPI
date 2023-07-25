using Magic_Villa_Web.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace Magic_Villa_Web.Services.IServices
{
    public interface IAuthServices
    {
        Task<T>LoginAsync<T>(LoginRequestDTO model);
        Task<T>RegisterAsync<T>(RegistrationRequestDTO model);

    }
}
