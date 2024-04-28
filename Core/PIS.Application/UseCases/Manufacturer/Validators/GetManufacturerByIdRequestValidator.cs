using FluentValidation;
using PIS.Application.UseCases.Manufacturer.Requests;

namespace PIS.Application.UseCases.Manufacturer.Validators
{
    public class GetManufacturerByIdRequestValidator : AbstractValidator<GetManufacturerByIdRequest>
    {
        public GetManufacturerByIdRequestValidator()
        {
            RuleFor(x => x.ManufacturerId).NotEmpty();
            RuleFor(x => x.ManufacturerId).Must(x => Guid.TryParse(x, out _))
                .WithMessage(x => $"{nameof(x.ManufacturerId)} is not a valid GUID");
        }
    }
}
