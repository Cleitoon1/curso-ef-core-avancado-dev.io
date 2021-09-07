using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Domain;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Curso.Dominando.EFCore.Modulo18.UOWRepository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly ILogger<DepartamentoController> _logger;
        //private readonly IDepartamentoRepository _repository;
        private readonly IUnitOfWork _uow;
        public DepartamentoController(ILogger<DepartamentoController> logger, /*IDepartamentoRepository repository,*/ IUnitOfWork uow)
        {
            _logger = logger;
            //_repository = repository;
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _uow.DepartamentoRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetByDescricaoAsync([FromQuery] string descricao)
        {
            var deparamentos = await _uow.DepartamentoRepository.GetDataAsync(
                _ => _.Descricao.Contains(descricao),
                _ => _.Include(__ => __.Colaboradores), 
                2);
            return Ok(deparamentos);
        }

        [HttpPost]
        public IActionResult GetByIdAsync(Departamento departamento)
        {
            _uow.DepartamentoRepository.Add(departamento);
            _uow.Commit();
            return Ok(departamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverDeparamento(int id)
        {
            var departamento = await _uow.DepartamentoRepository.GetByIdAsync(id);
            if (departamento == null)
                return NotFound();

            _uow.DepartamentoRepository.Remove(departamento);
            _uow.Commit();
            return Ok();
        }
    }
}
