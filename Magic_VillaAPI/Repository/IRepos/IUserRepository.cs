using Magic_VillaAPI.Models;
using Magic_VillaAPI.Models.Dto;

namespace Magic_VillaAPI.Repository.IRepos
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO model);
        Task<UserDTO> Register(RegisterRequestDTO model);
    }
}
