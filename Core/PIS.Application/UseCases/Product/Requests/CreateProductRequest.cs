namespace PIS.Application.UseCases.Product.Requests
{
    public sealed class CreateProductRequest
    {
        public required string ManufacturerId { get; set; }
        public required string Name { get; set; }
    }
}
