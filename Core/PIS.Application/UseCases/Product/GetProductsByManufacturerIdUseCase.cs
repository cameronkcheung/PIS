using FluentValidation;
using FluentValidation.Results;
using PIS.Application.Data;
using PIS.Application.Exceptions;
using PIS.Application.UseCases.Product.Requests;
using PIS.Application.UseCases.Product.Responses;
using PIS.Domain.Exceptions;
using static PIS.Application.UseCases.Product.Responses.GetProductsByManufacturerIdResponse;

namespace PIS.Application.UseCases.Product
{
    public sealed class GetProductsByManufacturerIdUseCase : IUseCase<GetProductsByManufacturerIdRequest, GetProductsByManufacturerIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetProductsByManufacturerIdRequest> _getProductsByManufacturerIdRequestValidator;

        public GetProductsByManufacturerIdUseCase(IUnitOfWork unitOfWork, IValidator<GetProductsByManufacturerIdRequest> getProductsByManufacturerIdRequestValidator)
        {
            _unitOfWork = unitOfWork;
            _getProductsByManufacturerIdRequestValidator = getProductsByManufacturerIdRequestValidator;
        }
        public async Task<GetProductsByManufacturerIdResponse> HandleAsync(GetProductsByManufacturerIdRequest request)
        {
            #region Validate request
            ValidationResult validationResult = _getProductsByManufacturerIdRequestValidator.Validate(request);
            if (!validationResult.IsValid) throw new RequestValidationException(validationResult.Errors.First().ErrorMessage);
            #endregion

            #region Check if manufacturer exists
            Domain.Models.Manufacturer? manufacturer = await _unitOfWork.ManufacturerRepository.GetAsync(request.ManufacturerId);

            if (manufacturer is null) throw new ManufacturerNotFoundException($"Manufacturer {request.ManufacturerId} not found");
            #endregion

            IEnumerable<GetProductsByManufacturerIdViewModel> getProductsByManufacturerIdViewModel = _unitOfWork.ProductRepository.GetProductsByManufacturerIdView(request.ManufacturerId);

            return new GetProductsByManufacturerIdResponse() { Products = getProductsByManufacturerIdViewModel };
        }
    }
}
