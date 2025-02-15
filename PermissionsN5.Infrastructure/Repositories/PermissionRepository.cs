using PermissionsN5.Domain.Entity;
using PermissionsN5.Domain.Interfaces;
using PermissionsN5.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Infrastructure.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(PermissionDbContext permissionDbContext):base(permissionDbContext) 
        {
        }
    }
}
