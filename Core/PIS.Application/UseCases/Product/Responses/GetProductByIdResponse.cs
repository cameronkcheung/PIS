namespace PIS.Application.UseCases.Product.Responses
{
    public sealed class GetProductByIdResponse
    {
        public required GetProductByIdViewModel Product {  get; set; }
        public sealed class GetProductByIdViewModel
        {
            public required string Id { get; set;}
            public required string Name { get; set; }
            public required string ManufacturerName { get; set; }
        }
    }
}
