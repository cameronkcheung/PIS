namespace PIS.Application.UseCases.Manufacturer.Responses
{
    public sealed class GetManufacturerByIdResponse
    {
        public required GetManufacturerByIdViewModel Manufacturer { get; set; }
        public class GetManufacturerByIdViewModel
        {
            public required string Id { get; set; }
            public required string Name { get; set; }
            public required int ProductCount { get; set; }
        }
    }
}
