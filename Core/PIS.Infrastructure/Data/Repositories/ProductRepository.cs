using PIS.Domain.Models;
using PIS.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static PIS.Application.UseCases.Product.Responses.GetProductsByManufacturerIdResponse;
using static PIS.Application.UseCases.Product.Responses.GetProductByIdResponse;
using PIS.Application.Repositories;

namespace PIS.Infrastructure.Data.Repositories
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly IProductInformationDbContext _productInformationDbContext;

        public ProductRepository(IProductInformationDbContext productInformationDbContext)
        {
            _productInformationDbContext = productInformationDbContext;
        }

        public void Add(string manufacturerId, Product product)
        {
            ProductEntity productEntiy = new ProductEntity()
            {
                Id = new Guid(product.Id),
                Name = product.Name,
                ManufacturerId = new Guid(manufacturerId)
            };

            _productInformationDbContext.Products.Add(productEntiy);
        }

        public IEnumerable<Product> GetByManufacturerId(string manufacturerId)
        {
            var products = _productInformationDbContext.Products.Select(x => new Product()
            {
                Id = x.Id.ToString(),
                Name = x.Name
            });

            return products;
        }

        #region Views
        public async Task<GetProductByIdViewModel?> GetProductByIdView(string productId)
        {
            GetProductByIdViewModel? getProductByIdViewModels = await _productInformationDbContext.Products
                .Where(x => x.Id == new Guid(productId))
                .Select(x => new GetProductByIdViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    ManufacturerName = x.Manufacturer.Name
                }).AsNoTracking().SingleOrDefaultAsync();

            return getProductByIdViewModels;
        }

        public IEnumerable<GetProductsByManufacturerIdViewModel> GetProductsByManufacturerIdView(string manufacturerId)
        {
            IEnumerable<GetProductsByManufacturerIdViewModel>? getProductsByManufacturerIdViewModel = _productInformationDbContext.Products
                .Where(x => x.ManufacturerId == new Guid(manufacturerId))
                .Select(x => new GetProductsByManufacturerIdViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name
                }).AsNoTracking();

            return getProductsByManufacturerIdViewModel;
        }
        #endregion
    }
}
