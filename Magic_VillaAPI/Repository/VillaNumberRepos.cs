using Magic_VillaAPI.Data;
using Magic_VillaAPI.Models;
using Magic_VillaAPI.Repository.IRepos;

namespace Magic_VillaAPI.Repository
{
    public class VillaNumberRepos : RepoServices<VillaNumber>, IVillaNumberRepo
    {
        private readonly VillaContext _context;
        public VillaNumberRepos(VillaContext context) : base(context)
        {
            _context = context;
        }
        public async Task<VillaNumber> Update(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
