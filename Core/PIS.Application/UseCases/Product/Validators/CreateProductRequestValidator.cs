using FluentValidation;
using PIS.Application.UseCases.Product.Requests;

namespace PIS.Application.UseCases.Product.Validators
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name.Length).LessThanOrEqualTo(256);
            RuleFor(x => x.ManufacturerId).NotEmpty();
            RuleFor(x => x.ManufacturerId).Must(x => Guid.TryParse(x, out _))
                .WithMessage(x => $"{nameof(x.ManufacturerId)} is not a valid GUID");
        }
    }
}
