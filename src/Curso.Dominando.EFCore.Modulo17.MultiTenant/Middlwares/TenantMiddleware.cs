using Curso.Dominando.EFCore.Modulo17.MultiTenant.Extensions;
using Curso.Dominando.EFCore.Modulo17.MultiTenant.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Curso.Dominando.EFCore.Modulo17.MultiTenant.Middlwares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var tenant = httpContext.RequestServices.GetRequiredService<TenantData>();
            tenant.TenantId = httpContext.GetTenantId();
            await _next(httpContext);
        }
    }
}
