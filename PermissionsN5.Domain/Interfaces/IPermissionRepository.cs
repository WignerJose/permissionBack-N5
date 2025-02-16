using PermissionsN5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Domain.Interfaces
{
    public interface IPermissionRepository:IRepositoryAsync<Permission>{}
}
