using FluentValidation;
using FluentValidation.Results;
using PIS.Application.Data;
using PIS.Application.Exceptions;
using PIS.Application.UseCases.Product.Requests;
using PIS.Application.UseCases.Product.Responses;
using PIS.Domain.Exceptions;
namespace PIS.Application.UseCases.Product
{
    public sealed class CreateProductUseCase : IUseCase<CreateProductRequest, CreateProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductRequest> _createProductRequestValidator;

        public CreateProductUseCase(IUnitOfWork unitOfWork, IValidator<CreateProductRequest> createProductRequestValidator)
        {
            _unitOfWork = unitOfWork;
            _createProductRequestValidator = createProductRequestValidator;
        }

        public async Task<CreateProductResponse> HandleAsync(CreateProductRequest request)
        {
            #region Validate request
            ValidationResult validationResult = _createProductRequestValidator.Validate(request);
            if (!validationResult.IsValid) throw new RequestValidationException(validationResult.Errors.First().ErrorMessage);
            #endregion

            _unitOfWork.Initilize();

            #region Check manufacturer exists
            Domain.Models.Manufacturer? manufacturer = await _unitOfWork.ManufacturerRepository.GetAsync(request.ManufacturerId);

            if (manufacturer is null) throw new ManufacturerNotFoundException($"Manufacturer {request.ManufacturerId} not found");
            #endregion

            #region Check there is no product already with the given name
            var products = _unitOfWork.ProductRepository.GetByManufacturerId(manufacturer.Id);

            if (products.Any(x => string.Equals(request.Name, x.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ProductAlreadyExistsException($"Product {request.Name} already exists for manufacturer {request.ManufacturerId}");
            }
            #endregion

            var product = new Domain.Models.Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name
            };

            _unitOfWork.ProductRepository.Add(request.ManufacturerId, product);

            await _unitOfWork.CommitAsync();

            return new CreateProductResponse()
            {
                Product = new() { Id = product.Id, Name = product.Name }
            };
        }
    }
}
