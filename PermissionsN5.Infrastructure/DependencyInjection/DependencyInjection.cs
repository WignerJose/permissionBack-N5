using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionsN5.Domain.Entities;
using PermissionsN5.Domain.Interfaces;
using PermissionsN5.Domain.Interfaces.ElasticSearch;
using PermissionsN5.Infrastructure.Config;
using PermissionsN5.Infrastructure.ElasticSearch;
using PermissionsN5.Infrastructure.Persistence;
using PermissionsN5.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PermissionDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionStrings")));

            services.AddOptions<ElasticSearchConfig>()
                .Bind(configuration.GetSection("ElasticSearchSettings"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryBase<>));
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IPermissionTypeRepository, PermissionTypeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IElasticSearchService<Permission>, ElasticSearchService>();
            return services;
        }
    }
}
