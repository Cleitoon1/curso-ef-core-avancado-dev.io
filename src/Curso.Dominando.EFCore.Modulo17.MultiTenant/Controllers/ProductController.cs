using Curso.Dominando.EFCore.Modulo17.MultiTenant.Data;
using Curso.Dominando.EFCore.Modulo17.MultiTenant.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo17.MultiTenant.Controllers
{
    [ApiController]
    [Route("{tenant}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get([FromServices] ApplicationContext context) => context.Products.ToArray();
    }
}
