using Magic_VillaAPI.Models;

namespace Magic_VillaAPI.Repository.IRepository
{
    public interface IVillaRepos:IRepository<Villa>
    {
        Task<Villa> Update(Villa entity);
    }
}
