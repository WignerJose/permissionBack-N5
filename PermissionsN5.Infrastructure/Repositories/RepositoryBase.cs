using Microsoft.EntityFrameworkCore;
using PermissionsN5.Domain.Interfaces;
using PermissionsN5.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly PermissionDbContext _permissionDbContext;

        public RepositoryBase(PermissionDbContext permissionDbContext)
        {
            _permissionDbContext = permissionDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _permissionDbContext.Set<T>().AddAsync(entity);
            await _permissionDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _permissionDbContext.Set<T>().Remove(entity);
            await _permissionDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _permissionDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _permissionDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _permissionDbContext.Set<T>().Attach(entity);
            _permissionDbContext.Entry(entity).State = EntityState.Modified;
            await _permissionDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
