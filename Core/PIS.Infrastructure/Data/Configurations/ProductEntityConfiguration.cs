using PIS.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PIS.Infrastructure.Data.Configurations
{
    internal sealed class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(product => product.Id);

            builder.Property(product => product.Name).IsRequired();

            //Should product names by unique by manufacturer?
            //builder.HasIndex(product => new { product.ManufacturerId, product.Name }).IsUnique();
        }
    }
}
