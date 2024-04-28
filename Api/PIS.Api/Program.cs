using PIS.Api.Swagger;
using PIS.Application.Extensions;
using PIS.Common.Extensions;
using PIS.Infrastructure.Extensions;
using Pollr2.API.Middleware;

namespace PIS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.OperationFilter<TenantHeaderOperationFilter>();
            });

            builder.Services.AddTenants(builder.Configuration);
            builder.Services.AddDataServices(builder.Configuration);
            builder.Services.AddUseCases();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<TenantMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}