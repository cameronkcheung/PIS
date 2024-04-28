using PIS.Application.UseCases;
using PIS.Application.UseCases.Manufacturer;
using PIS.Application.UseCases.Manufacturer.Requests;
using PIS.Application.UseCases.Manufacturer.Responses;
using PIS.Application.UseCases.Product;
using PIS.Application.UseCases.Product.Requests;
using PIS.Application.UseCases.Product.Responses;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using PIS.Application.UseCases.Manufacturer.Validators;
using PIS.Application.UseCases.Product.Validators;

namespace PIS.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            #region Register validators
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            services.AddTransient<IValidator<CreateManufacturerRequest>, CreateManufacturerRequestValidator>();
            services.AddTransient<IValidator<GetManufacturerByIdRequest>, GetManufacturerByIdRequestValidator>();
            services.AddTransient<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
            services.AddTransient<IValidator<GetProductsByManufacturerIdRequest>, GetProductsByManufacturerIdRequestValidator>();
            services.AddTransient<IValidator<GetProductByIdRequest>, GetProductByIdRequestValidator>();
            #endregion

            services.AddTransient<IUseCase<CreateManufacturerRequest, CreateManufacturerResponse>, CreateManufacturerUseCase>();
            services.AddTransient<IUseCase<GetManufacturerByIdRequest, GetManufacturerByIdResponse>, GetManufacturerByIdUseCase>();
            services.AddTransient<IUseCase<CreateProductRequest, CreateProductResponse>, CreateProductUseCase>();
            services.AddTransient<IUseCase<GetProductsByManufacturerIdRequest, GetProductsByManufacturerIdResponse>, GetProductsByManufacturerIdUseCase>();
            services.AddTransient<IUseCase<GetProductByIdRequest, GetProductByIdResponse>, GetProductByIdUseCase>();

            return services;
        }
    }
}
