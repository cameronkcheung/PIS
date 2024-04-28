namespace PIS.Application.UseCases.Product.Responses
{
    public sealed class GetProductsByManufacturerIdResponse
    {
        public required IEnumerable<GetProductsByManufacturerIdViewModel> Products { get; set; }

        public sealed class GetProductsByManufacturerIdViewModel
        {
            public required string Id { get; set; }
            public required string Name { get; set; }
        }
    }
}