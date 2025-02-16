using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using PermissionsN5.Domain.Entities;
using PermissionsN5.Domain.Interfaces.ElasticSearch;
using PermissionsN5.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Infrastructure.ElasticSearch
{
    public class ElasticSearchService : IElasticSearchService<Permission>
    {
        private readonly ElasticClient _elasticClient;
        private readonly string _indexName;

        public ElasticSearchService(IOptions<ElasticSearchConfig> elasticsearchConfig)
        {
            var singleNodeConnection = new SingleNodeConnectionPool(new Uri(elasticsearchConfig.Value.Url));
            var settings = new ConnectionSettings(singleNodeConnection).DefaultIndex(elasticsearchConfig.Value.IndexName);
            _elasticClient = new ElasticClient(settings);
            _indexName = elasticsearchConfig.Value.IndexName;
        }
        public async Task<IndexResponse> CreatePermissionAsync(Permission model)
        {
            return await _elasticClient.IndexAsync(model, decriptor => decriptor.Index(_indexName));
        }

        public async Task<List<Permission>> GetPermissionsAsync()
        {
            var search = new SearchDescriptor<Permission>(_indexName);
            var response = await _elasticClient.SearchAsync<Permission>(search);

            if (!response.IsValid)
                return new List<Permission>();

            return response.Hits.Select(hit => hit.Source).ToList();
        }

        public async Task<UpdateResponse<Permission>> UpdatePermissionAsync(Permission model)
        {
            return await _elasticClient.UpdateAsync(DocumentPath<Permission>.Id(model.Id).Index(_indexName), p => p.Doc(model));
        }
    }
}
