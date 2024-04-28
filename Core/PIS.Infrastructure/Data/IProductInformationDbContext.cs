
using PIS.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PIS.Infrastructure.Data
{
    public interface IProductInformationDbContext
    {
        DbSet<ManufacturerEntity> Manufacturers { get; set; }
        DbSet<ProductEntity> Products { get; set; }

        void ClearChanges();
        Task<int> SaveChangesAsync();
    }
}
