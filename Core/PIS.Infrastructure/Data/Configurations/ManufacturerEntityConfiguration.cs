using PIS.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PIS.Infrastructure.Data.Configurations
{
    internal sealed class ManufacturerEntityConfiguration : IEntityTypeConfiguration<ManufacturerEntity>
    {
        public void Configure(EntityTypeBuilder<ManufacturerEntity> builder)
        {
            builder.HasKey(manufacturer => manufacturer.Id);

            builder.HasMany(manufacturer => manufacturer.Products)
                .WithOne(product => product.Manufacturer);

            builder.HasIndex(manufacturer => manufacturer.Name);
        }
    }
}
