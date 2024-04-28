using FluentValidation;
using PIS.Application.UseCases.Product.Requests;

namespace PIS.Application.UseCases.Product.Validators
{
    public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
    {
        public GetProductByIdRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductId).Must(x => Guid.TryParse(x, out _))
                .WithMessage(x => $"{nameof(x.ProductId)} is not a valid GUID");
        }
    }
}
