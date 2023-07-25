using Magic_VillaAPI.Data;
using Magic_VillaAPI.Models;
using Magic_VillaAPI.Repository.IRepository;

namespace Magic_VillaAPI.Repository
{
    public class VillaRepository : RepoServices<Villa>, IVillaRepos
    {
        private readonly VillaContext _context;
        public VillaRepository(VillaContext context): base(context)
        {
            _context = context;
        }
        public async Task<Villa> Update(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.Villas.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
