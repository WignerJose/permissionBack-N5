using PermissionsN5.Domain.Interfaces;
using PermissionsN5.Infrastructure.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly PermissionDbContext _permissionDbContext;
        private IPermissionRepository _permissionRepository;
        private IPermissionTypeRepository _permissionTypeRepository;

        public UnitOfWork(PermissionDbContext permissionDbContext)
        {
            _permissionDbContext = permissionDbContext;
        }
        public PermissionDbContext PermissionDbContext => _permissionDbContext;
        public IPermissionRepository PermissionRepository => _permissionRepository ??= new PermissionRepository(_permissionDbContext);

        public IPermissionTypeRepository PermissionTypeRepository => _permissionTypeRepository ??= new PermissionTypeRepository(_permissionDbContext);

        public async Task<int> Complete()
        {
            return await _permissionDbContext.SaveChangesAsync();
        }

        public IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories is null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _permissionDbContext);
                _repositories.Add(type, repositoryInstance);
            }

            return (IRepositoryAsync<TEntity>)_repositories[type];
        }

        public void Dispose()
        {
            _permissionDbContext.Dispose();
        }

    }
}
