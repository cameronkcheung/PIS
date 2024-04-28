using PIS.Application.Data;
using PIS.Application.Repositories;
using PIS.Infrastructure.Data.Repositories;

namespace PIS.Infrastructure.Data
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IProductInformationDbContext _productInformationDbContext;
        public IManufacturerRepository ManufacturerRepository => new ManufacturerRepository(_productInformationDbContext);
        public IProductRepository ProductRepository => new ProductRepository(_productInformationDbContext);

        public UnitOfWork(IProductInformationDbContext productInformationDbContext)
        {
            _productInformationDbContext = productInformationDbContext;
        }

        public void Initilize()
        {
            _productInformationDbContext.ClearChanges();
        }

        public async Task CommitAsync()
        {
            await _productInformationDbContext.SaveChangesAsync();
        }
    }
}
