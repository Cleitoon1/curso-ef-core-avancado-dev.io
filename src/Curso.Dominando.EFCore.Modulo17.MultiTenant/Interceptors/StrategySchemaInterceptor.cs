using Curso.Dominando.EFCore.Modulo17.MultiTenant.Provider;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Curso.Dominando.EFCore.Modulo17.MultiTenant.Interceptors
{
    public class StrategySchemaInterceptor : DbCommandInterceptor
    {
        private readonly TenantData tenantData;

        public StrategySchemaInterceptor(TenantData tenantData)
        {
            this.tenantData = tenantData;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            ReplaceSchema(command);
            return base.ReaderExecuting(command, eventData, result);
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            ReplaceSchema(command);
            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        private void ReplaceSchema(DbCommand command)
        {
            //FROM PRODUCTS -> FROM tenant1.Products
            command.CommandText = command.CommandText.Replace("FROM", $" FROM [{tenantData.TenantId}].")
                .Replace("JOIN", $" JOIN [{tenantData.TenantId}].");
        }
    }
}
