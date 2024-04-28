using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using PIS.Api.Constants;
using PIS.Common.Context;
using PIS.Common.Models;
using PIS.Common.Services;
using System.Net;
using System.Net.Mime;

namespace Pollr2.API.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, IOptionsSnapshot<List<Tenant>> tenants, ITenantContextAccessor tenantContextAccessor)
        {
            if (!context.Request.Headers.TryGetValue(HeaderConstants.Tenant, out var value))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = MediaTypeNames.Text.Plain;
                await context.Response.WriteAsync("Tenant was not provided");
                return;
            }

            var tenant = tenants.Value.FirstOrDefault(x => x.Name == value);

            if (tenant is null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = MediaTypeNames.Text.Plain;
                await context.Response.WriteAsync($"Tenant {value} not found");
                return;
            }

            tenantContextAccessor.TenantContext = new TenantContext { Tenant = tenant };

            await _next(context);
        }
    }
}
