using PIS.Infrastructure.Data.Abstractions;

namespace PIS.Infrastructure.Data.Entities
{
    public sealed class ProductEntity : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ManufacturerId { get; set; }
        public ManufacturerEntity Manufacturer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
