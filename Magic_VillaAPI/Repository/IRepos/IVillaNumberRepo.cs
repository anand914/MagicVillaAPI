using Magic_VillaAPI.Models;
using Magic_VillaAPI.Repository.IRepository;

namespace Magic_VillaAPI.Repository.IRepos
{
    public interface IVillaNumberRepo:IRepository<VillaNumber>
    {
        Task<VillaNumber> Update(VillaNumber entity);
    }
}
