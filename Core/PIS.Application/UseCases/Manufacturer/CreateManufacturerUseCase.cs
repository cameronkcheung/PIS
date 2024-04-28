using FluentValidation;
using FluentValidation.Results;
using PIS.Application.Data;
using PIS.Application.Exceptions;
using PIS.Application.UseCases.Manufacturer.Requests;
using PIS.Application.UseCases.Manufacturer.Responses;
using PIS.Domain.Exceptions;

namespace PIS.Application.UseCases.Manufacturer
{
    public sealed class CreateManufacturerUseCase : IUseCase<CreateManufacturerRequest, CreateManufacturerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateManufacturerRequest> _createManufacturerRequestValidator;

        public CreateManufacturerUseCase(IUnitOfWork unitOfWork, IValidator<CreateManufacturerRequest> createManufacturerRequestValidator)
        {
            _unitOfWork = unitOfWork;
            _createManufacturerRequestValidator = createManufacturerRequestValidator;
        }

        public async Task<CreateManufacturerResponse> HandleAsync(CreateManufacturerRequest request)
        {
            #region Validate request
            ValidationResult validationResult = _createManufacturerRequestValidator.Validate(request);
            if (!validationResult.IsValid) throw new RequestValidationException(validationResult.Errors.First().ErrorMessage);
            #endregion

            _unitOfWork.Initilize();
            #region Check if manufacturer with name already exists
            var existingManufacturer = await _unitOfWork.ManufacturerRepository.GetByNameAsync(request.Name);

            if (existingManufacturer is not null)
            {
                throw new ManufacturerAlreadyExistsException(existingManufacturer.Id);
            }
            #endregion

            Domain.Models.Manufacturer manufacturer = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name
            };

            _unitOfWork.ManufacturerRepository.Add(manufacturer);

            await _unitOfWork.CommitAsync();

            return new CreateManufacturerResponse
            {
                Manufacturer = new()
                {
                    Id = manufacturer.Id,
                    Name = manufacturer.Name
                }
            };
        }
    }
}
