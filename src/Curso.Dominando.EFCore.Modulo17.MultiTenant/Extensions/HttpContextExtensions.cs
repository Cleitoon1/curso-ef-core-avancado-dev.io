using Microsoft.AspNetCore.Http;

namespace Curso.Dominando.EFCore.Modulo17.MultiTenant.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetTenantId(this HttpContext httpContext)
        {
            //desenvolvedor.io/tenant-1/product => " " / "tentant-1" / "product"
            var tenant = httpContext.Request.Path.Value.Split('/', System.StringSplitOptions.RemoveEmptyEntries)[0];
            return tenant;
        }
    }
}
