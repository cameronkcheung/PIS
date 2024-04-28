using PIS.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PIS.Application.UseCases;
using PIS.Application.UseCases.Product.Requests;
using PIS.Application.UseCases.Product.Responses;
using PIS.Application.Exceptions;

namespace PIS.Api.Controllers
{
    public class ProductController : BaseController
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IUseCase<CreateProductRequest, CreateProductResponse> _createProductUseCase;
        private readonly IUseCase<GetProductsByManufacturerIdRequest, GetProductsByManufacturerIdResponse> _getProductsByManufacturerIdUseCase;
        private readonly IUseCase<GetProductByIdRequest, GetProductByIdResponse> _getProductByIdUseCase;

        public ProductController(
            ILogger<ProductController> logger,
            IUseCase<CreateProductRequest, CreateProductResponse> createProductUseCase,
            IUseCase<GetProductsByManufacturerIdRequest, GetProductsByManufacturerIdResponse> getProductsByManufacturerIdUseCase,
            IUseCase<GetProductByIdRequest, GetProductByIdResponse> getProductByIdUseCase)
        {
            _logger = logger;
            _createProductUseCase = createProductUseCase;
            _getProductsByManufacturerIdUseCase = getProductsByManufacturerIdUseCase;
            _getProductByIdUseCase = getProductByIdUseCase;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateProductRequest createProductRequest)
        {
            try
            {
                CreateProductResponse createProductResponse = await _createProductUseCase.HandleAsync(createProductRequest);
                return StatusCode((int)HttpStatusCode.Accepted, createProductResponse);
            }
            catch (RequestValidationException rve)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, rve.Message);
            }
            catch (ManufacturerNotFoundException mnfe)
            {
                return StatusCode((int)HttpStatusCode.NotFound, $"Manufacturer {createProductRequest.ManufacturerId} not found");
            }
            catch (ProductAlreadyExistsException paee)
            {
                return StatusCode((int)HttpStatusCode.Conflict, $"Product {createProductRequest.Name} already exists for manufacturer {createProductRequest.ManufacturerId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing {Controller}.{Method} for {ProductName}", nameof(ProductController), nameof(Create), createProductRequest.Name);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred");
            }
        }

        [HttpGet("manufacturer/{manufacturerId}")]
        public async Task<IActionResult> GetByManufacturerId(string manufacturerId)
        {
            try
            {
                GetProductsByManufacturerIdRequest getProductsByManufacturerIdRequest = new() { ManufacturerId = manufacturerId };
                GetProductsByManufacturerIdResponse getProductsByManufacturerIdResponse = await _getProductsByManufacturerIdUseCase.HandleAsync(getProductsByManufacturerIdRequest);
                return StatusCode((int)HttpStatusCode.OK, getProductsByManufacturerIdResponse);
            }
            catch (RequestValidationException rve)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, rve.Message);
            }
            catch (ManufacturerNotFoundException mnfe)
            {
                return StatusCode((int)HttpStatusCode.NotFound, $"Manufacturer {manufacturerId} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing {Controller}.{Method} for {ManufacturerId}", nameof(ProductController), nameof(GetByManufacturerId), manufacturerId);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred");
            }
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(string productId)
        {
            try
            {
                GetProductByIdRequest getProductByIdRequest = new() { ProductId = productId };
                GetProductByIdResponse getProductByIdResponse = await _getProductByIdUseCase.HandleAsync(getProductByIdRequest);
                return StatusCode((int)HttpStatusCode.OK, getProductByIdResponse);
            }
            catch (RequestValidationException rve)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, rve.Message);
            }
            catch (ProductNotFoundException pnfe)
            {
                return StatusCode((int)HttpStatusCode.NotFound, $"Product {productId} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing {Controller}.{Method} for {productId}", nameof(ProductController), nameof(Get), productId);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred");
            }
        }
    }
}
