﻿using Magic_VillaAPI.Models;
using System.Linq.Expressions;

namespace Magic_VillaAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T,bool>>? filter=null,string? includeProperties=null, int pageSize=0, int pageNumber=1);
        Task<T> GetVilla(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
        Task Create(T entity);
        Task Remove(T entity);
        Task Save();

    }
}
