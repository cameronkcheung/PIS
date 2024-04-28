using PIS.Domain.Models;
using static PIS.Application.UseCases.Manufacturer.Responses.GetManufacturerByIdResponse;

namespace PIS.Application.Repositories
{
    public interface IManufacturerRepository
    {
        void Add(Manufacturer manufacturer);
        Task<Manufacturer?> GetAsync(Guid Id);
        Task<Manufacturer?> GetAsync(string Id);
        Task<Manufacturer?> GetByNameAsync(string name);
        Task<GetManufacturerByIdViewModel?> GetManufacturerByIdView(string id);
    }
}
