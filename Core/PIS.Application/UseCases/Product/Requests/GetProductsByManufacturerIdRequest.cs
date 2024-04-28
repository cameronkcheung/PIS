namespace PIS.Application.UseCases.Product.Requests
{
    public sealed class GetProductsByManufacturerIdRequest
    {
        public required string ManufacturerId { get; set; }
    }
}
