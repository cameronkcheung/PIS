namespace PIS.Application.UseCases.Manufacturer.Responses
{
    public sealed class CreateManufacturerResponse
    {
        public required CreateManufacturerViewModel Manufacturer { get; set; }
        public sealed class CreateManufacturerViewModel
        {
            public required string Id { get; set; }
            public required string Name { get; set; }
        }
    }
}
