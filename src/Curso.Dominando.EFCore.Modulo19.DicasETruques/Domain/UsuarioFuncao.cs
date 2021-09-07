using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo19.DicasETruques.Domain
{
    public class UsuarioFuncao
    {
        public Guid UsuarioId { get; set; }
        public Guid FuncaoId { get; set; }
    }
}
