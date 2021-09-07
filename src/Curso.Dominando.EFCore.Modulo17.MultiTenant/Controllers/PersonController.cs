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
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get([FromServices] ApplicationContext context) => context.Persons.ToArray();
    }
}
