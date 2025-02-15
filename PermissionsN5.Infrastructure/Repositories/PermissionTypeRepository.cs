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
    public class PermissionTypeRepository : RepositoryBase<PermissionType>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(PermissionDbContext permissionDbContext) : base(permissionDbContext)
        {
        }
    }
}
