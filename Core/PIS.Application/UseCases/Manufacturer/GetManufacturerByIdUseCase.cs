using FluentValidation;
using FluentValidation.Results;
using PIS.Application.Data;
using PIS.Application.Exceptions;
using PIS.Application.UseCases.Manufacturer.Requests;
using PIS.Application.UseCases.Manufacturer.Responses;
using PIS.Domain.Exceptions;
using static PIS.Application.UseCases.Manufacturer.Responses.GetManufacturerByIdResponse;

namespace PIS.Application.UseCases.Manufacturer
{
    public class GetManufacturerByIdUseCase : IUseCase<GetManufacturerByIdRequest, GetManufacturerByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetManufacturerByIdRequest> _getManufacturerByIdRequestValidator;

        public GetManufacturerByIdUseCase(IUnitOfWork unitOfWork, IValidator<GetManufacturerByIdRequest> getManufacturerByIdRequestValidator)
        {
            _unitOfWork = unitOfWork;
            _getManufacturerByIdRequestValidator = getManufacturerByIdRequestValidator;
        }
        public async Task<GetManufacturerByIdResponse> HandleAsync(GetManufacturerByIdRequest request)
        {
            #region Validate request
            ValidationResult validationResult = _getManufacturerByIdRequestValidator.Validate(request);
            if (!validationResult.IsValid) throw new RequestValidationException(validationResult.Errors.First().ErrorMessage);
            #endregion

            GetManufacturerByIdViewModel? getManufacturerByIdViewModel = await _unitOfWork.ManufacturerRepository.GetManufacturerByIdView(request.ManufacturerId);

            if (getManufacturerByIdViewModel is null) throw new ManufacturerNotFoundException($"Manufacturer {request.ManufacturerId} not found");

            return new GetManufacturerByIdResponse() { Manufacturer = getManufacturerByIdViewModel };
        }
    }
}
