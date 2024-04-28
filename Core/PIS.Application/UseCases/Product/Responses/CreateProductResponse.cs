namespace PIS.Application.UseCases.Product.Responses
{
    public class CreateProductResponse
    {
        public required CreateProductViewModel Product { get; set; }
        public sealed class CreateProductViewModel
        {
            public required string Id { get; set; }
            public required string Name { get; set; }
        }
    }
}
