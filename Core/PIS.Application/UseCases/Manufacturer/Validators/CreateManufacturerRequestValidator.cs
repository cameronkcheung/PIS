using FluentValidation;
using PIS.Application.UseCases.Manufacturer.Requests;

namespace PIS.Application.UseCases.Manufacturer.Validators
{
    public class CreateManufacturerRequestValidator : AbstractValidator<CreateManufacturerRequest>
    {
        public CreateManufacturerRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name.Length).LessThanOrEqualTo(256);
        }
    }
}
