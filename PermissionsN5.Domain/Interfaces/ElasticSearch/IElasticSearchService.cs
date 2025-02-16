using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Domain.Interfaces.ElasticSearch
{
    public interface IElasticSearchService<T> where T : class
    {
        Task<List<T>> GetPermissionsAsync();
        Task<IndexResponse> CreatePermissionAsync(T model);
        Task<UpdateResponse<T>> UpdatePermissionAsync(T model);
    }
}
