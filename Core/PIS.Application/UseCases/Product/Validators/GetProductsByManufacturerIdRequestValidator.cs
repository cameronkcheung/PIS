using FluentValidation;
using PIS.Application.UseCases.Product.Requests;

namespace PIS.Application.UseCases.Product.Validators
{
    public class GetProductsByManufacturerIdRequestValidator : AbstractValidator<GetProductsByManufacturerIdRequest>
    {
        public GetProductsByManufacturerIdRequestValidator()
        {
            RuleFor(x => x.ManufacturerId).NotEmpty();
            RuleFor(x => x.ManufacturerId).Must(x => Guid.TryParse(x, out _))
                .WithMessage(x => $"{nameof(x.ManufacturerId)} is not a valid GUID");
        }
    }
}
