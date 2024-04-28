using FluentValidation;
using FluentValidation.Results;
using PIS.Application.Data;
using PIS.Application.Exceptions;
using PIS.Application.UseCases.Product.Requests;
using PIS.Application.UseCases.Product.Responses;
using PIS.Domain.Exceptions;
using static PIS.Application.UseCases.Product.Responses.GetProductByIdResponse;

namespace PIS.Application.UseCases.Product
{
    public sealed class GetProductByIdUseCase : IUseCase<GetProductByIdRequest, GetProductByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetProductByIdRequest> _getProductByIdRequestValidator;

        public GetProductByIdUseCase(IUnitOfWork unitOfWork, IValidator<GetProductByIdRequest> getProductByIdRequestValidator)
        {
            _unitOfWork = unitOfWork;
            _getProductByIdRequestValidator = getProductByIdRequestValidator;
        }

        public async Task<GetProductByIdResponse> HandleAsync(GetProductByIdRequest request)
        {
            #region Validate request
            ValidationResult validationResult = _getProductByIdRequestValidator.Validate(request);
            if (!validationResult.IsValid) throw new RequestValidationException(validationResult.Errors.First().ErrorMessage);
            #endregion

            GetProductByIdViewModel? getProductByIdViewModel = await _unitOfWork.ProductRepository.GetProductByIdView(request.ProductId);

            if (getProductByIdViewModel is null) throw new ProductNotFoundException($"Product {request.ProductId} not found");

            return new GetProductByIdResponse() { Product = getProductByIdViewModel };
        }
    }
}
