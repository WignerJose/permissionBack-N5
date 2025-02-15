using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PermissionsN5.Domain.Entity;
using PermissionsN5.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Infrastructure.Persistence
{
    public class PermissionDbContext : DbContext
    {
        private readonly string _connectionString;

        public PermissionDbContext(IOptions<DataBaseConfig> dataBaseConfig)
        {
            _connectionString = dataBaseConfig.Value.DbConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
    }
}
