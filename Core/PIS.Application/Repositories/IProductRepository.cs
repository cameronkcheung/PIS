using PIS.Application.UseCases.Product.Responses;
using PIS.Domain.Models;

namespace PIS.Application.Repositories
{
    public interface IProductRepository
    {
        void Add(string manufacturerId, Product product);
        IEnumerable<Product> GetByManufacturerId(string manufacturerId);
        Task<GetProductByIdResponse.GetProductByIdViewModel?> GetProductByIdView(string manufacturerId);
        IEnumerable<GetProductsByManufacturerIdResponse.GetProductsByManufacturerIdViewModel> GetProductsByManufacturerIdView(string manufacturerId);
    }
}
