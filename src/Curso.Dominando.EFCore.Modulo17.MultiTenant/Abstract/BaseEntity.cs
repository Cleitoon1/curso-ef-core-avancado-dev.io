namespace Curso.Dominando.EFCore.Modulo17.MultiTenant.Abstract
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
    }
}
