using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository PermissionRepository { get; }
        IPermissionTypeRepository PermissionTypeRepository { get; }
        IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}
