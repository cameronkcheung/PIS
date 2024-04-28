using PIS.Infrastructure.Data.Abstractions;

namespace PIS.Infrastructure.Data.Entities
{
    public sealed class ManufacturerEntity : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
