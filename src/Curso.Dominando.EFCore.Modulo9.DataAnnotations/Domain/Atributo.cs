using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso.Dominando.EFCore.Modulo9.DataAnnotations.Domain
{
    [Table("TabelaAtributos")]
    [Index(nameof(Id), nameof(Descricao), IsUnique = true, Name = "IDX_DESCRICAO")]
    [Comment("comentário de teste de minha tabela")]
    public class Atributo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("MinhaDescricao", TypeName = "VARCHAR(100)")]
        [Comment("comentário de teste de meu campo")]
        public string Descricao { get; set; }

        [Required]
        [MaxLength]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Observacao { get; set; }
    }
}
