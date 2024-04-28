
using PIS.Application.Repositories;

namespace PIS.Application.Data
{
    public interface IUnitOfWork
    {
        IManufacturerRepository ManufacturerRepository { get; }
        IProductRepository ProductRepository { get; }
        void Initilize();
        Task CommitAsync();
    }
}
