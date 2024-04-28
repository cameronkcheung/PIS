using PIS.Domain.Models;
using PIS.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static PIS.Application.UseCases.Manufacturer.Responses.GetManufacturerByIdResponse;
using PIS.Application.Repositories;

namespace PIS.Infrastructure.Data.Repositories
{
    internal sealed class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IProductInformationDbContext _productInformationDbContext;

        public ManufacturerRepository(IProductInformationDbContext productInformationDbContext)
        {
            _productInformationDbContext = productInformationDbContext;
        }

        public void Add(Manufacturer manufacturer)
        {
            ManufacturerEntity manufacturerEntity = new ManufacturerEntity()
            {
                Id = new Guid(manufacturer.Id),
                Name = manufacturer.Name
            };

            _productInformationDbContext.Manufacturers.Add(manufacturerEntity);
        }

        public async Task<Manufacturer?> GetAsync(string id)
        {
            return await GetAsync(new Guid(id));
        }

        public async Task<Manufacturer?> GetAsync(Guid id)
        {
            ManufacturerEntity manufacturerEntity = await _productInformationDbContext.Manufacturers.SingleOrDefaultAsync(x => x.Id == id);

            if (manufacturerEntity == null) return null;

            Manufacturer manufacturer = new Manufacturer()
            {
                Id = manufacturerEntity.Id.ToString(),
                Name = manufacturerEntity.Name
            };

            return manufacturer;
        }

        public async Task<Manufacturer?> GetByNameAsync(string name)
        {
            ManufacturerEntity manufacturerEntity = await _productInformationDbContext.Manufacturers.SingleOrDefaultAsync(x => x.Name == name);

            if (manufacturerEntity == null) return null;

            Manufacturer manufacturer = new Manufacturer()
            {
                Id = manufacturerEntity.Id.ToString(),
                Name = manufacturerEntity.Name
            };

            return manufacturer;
        }

        #region Views
        public async Task<GetManufacturerByIdViewModel?> GetManufacturerByIdView(string id)
        {
            GetManufacturerByIdViewModel? getManufacturerByIdViewModel = await _productInformationDbContext.Manufacturers
                .Where(x => x.Id == new Guid(id))
                .Select(x => new GetManufacturerByIdViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    ProductCount = x.Products.Count()
                }).AsNoTracking().SingleOrDefaultAsync();

            return getManufacturerByIdViewModel;
        }
        #endregion
    }
}
