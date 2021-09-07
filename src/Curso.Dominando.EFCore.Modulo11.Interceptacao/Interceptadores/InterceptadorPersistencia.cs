using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Curso.Dominando.EFCore.Modulo11.Interceptacao.Interceptadores
{
    public class InterceptadorPersistencia : SaveChangesInterceptor, ISaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            System.Console.WriteLine(eventData.Context.ChangeTracker.DebugView.LongView);
            return result;
        }
    }
}
