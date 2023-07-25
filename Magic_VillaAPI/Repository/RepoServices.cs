using Magic_VillaAPI.Data;
using Magic_VillaAPI.Models;
using Magic_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Magic_VillaAPI.Repository
{
    public class RepoServices<T> : IRepository<T> where T : class
    {
        private readonly VillaContext _context;
        internal DbSet<T> dbSet;
        public RepoServices(VillaContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await dbSet.AddAsync(entity);
            await Save();
        }
        //*Villa,VillaSpeacial //
        public async Task<List<T>> GetAll(Expression<Func<T,bool>> filter = null,string? includeProperties=null,int pageSize=0,int pageNumber=1)
        {
            IQueryable<T> query = dbSet;
            if(includeProperties!=null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(pageSize >0)
            {
                if(pageSize>100)
                {
                    pageSize = 100;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetVilla(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter!= null)
            {
                query = query.Where(filter);
            }
           return await query.FirstOrDefaultAsync();
        }

        public async Task Remove(T entity)
        {
             dbSet.Remove(entity);
             await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
