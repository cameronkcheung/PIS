using PIS.Application.UseCases;
using PIS.Application.UseCases.Manufacturer.Requests;
using PIS.Application.UseCases.Manufacturer.Responses;
using PIS.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PIS.Application.Exceptions;

namespace PIS.Api.Controllers
{
    public class ManufacturerController : BaseController
    {
        private readonly ILogger<ManufacturerController> _logger;
        private readonly IUseCase<CreateManufacturerRequest, CreateManufacturerResponse> _createManufacturerUseCase;
        private readonly IUseCase<GetManufacturerByIdRequest, GetManufacturerByIdResponse> _getManufacturerByIdUseCase;

        public ManufacturerController(
            ILogger<ManufacturerController> logger,
            IUseCase<CreateManufacturerRequest, CreateManufacturerResponse> createManufacturerUseCase,
            IUseCase<GetManufacturerByIdRequest, GetManufacturerByIdResponse> getManufacturerByIdUseCase)
        {
            _logger = logger;
            _createManufacturerUseCase = createManufacturerUseCase;
            _getManufacturerByIdUseCase = getManufacturerByIdUseCase;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateManufacturerRequest createManufacturerRequest)
        {
            try
            {
                CreateManufacturerResponse createManufacturerResponse = await _createManufacturerUseCase.HandleAsync(createManufacturerRequest);
                return StatusCode((int)HttpStatusCode.Accepted, createManufacturerResponse);
            }
            catch (RequestValidationException rve)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, rve.Message);
            }
            catch (ManufacturerAlreadyExistsException maee)
            {
                return StatusCode((int)HttpStatusCode.Conflict, $"Manufacturer {createManufacturerRequest.Name} already exists");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing {Controller}.{Method} for {ManufacturerName}", nameof(ManufacturerController), nameof(Create), createManufacturerRequest.Name);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred");
            }
        }

        [HttpGet("{manufacturerId}")]
        public async Task<IActionResult> GetManufacturerById(string manufacturerId)
        {
            try
            {
                GetManufacturerByIdResponse getManufacturerByIdResponse = await _getManufacturerByIdUseCase.HandleAsync(new GetManufacturerByIdRequest() { ManufacturerId = manufacturerId });
                return StatusCode((int)HttpStatusCode.Accepted, getManufacturerByIdResponse);
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
                _logger.LogError(ex, "Error occurred executing {Controller}.{Method} for {ManufacturerId}", nameof(ManufacturerController), nameof(GetManufacturerById), manufacturerId);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred");
            }
        }
    }
}
