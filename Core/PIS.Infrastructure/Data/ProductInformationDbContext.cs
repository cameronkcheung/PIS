using PIS.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PIS.Common.Services;
using PIS.Infrastructure.Data.Abstractions;

namespace PIS.Infrastructure.Data
{
    public sealed class ProductInformationDbContext : DbContext, IProductInformationDbContext
    {
        private readonly ITenantContextAccessor _tenantContextAccessor;

        public DbSet<ManufacturerEntity> Manufacturers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public ProductInformationDbContext(DbContextOptions options, ITenantContextAccessor tenantContextAccessor) : base(options)
        {
            _tenantContextAccessor = tenantContextAccessor;
        }

        // Required for Entity Framework
        public ProductInformationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ProductInformationDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringSetting = _tenantContextAccessor?.TenantContext.Tenant.Settings.First(x => x.Name == "DatabaseConnectionString");

            // This is required to allow EF CLI tools to function properly without setting tenant
            string connectionString = connectionStringSetting is null ? string.Empty : connectionStringSetting.Value;

            optionsBuilder.UseNpgsql(connectionString);
        }

        public override int SaveChanges()
        {
            UpdateEntityAudits();
            return base.SaveChanges();
        }

        public void ClearChanges()
        {
            ChangeTracker.Clear();
        }

        public Task<int> SaveChangesAsync()
        {
            UpdateEntityAudits();
            return base.SaveChangesAsync();
        }

        /// <summary>
        /// Automatically updates entity CreatedAt and UpdatedAt of entities based on EntityState.
        /// </summary>
        private void UpdateEntityAudits()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((IAuditableEntity)entity.Entity).CreatedAt = now;
                }
                ((IAuditableEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}