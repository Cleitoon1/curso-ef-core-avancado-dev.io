using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Curso.Dominando.EFCore.Modulo2122.ComportamentosEDiagnostics
{
    public class MySqlServerQuerySqlGenerator : SqlServerQuerySqlGenerator
    {
        public MySqlServerQuerySqlGenerator([NotNull] QuerySqlGeneratorDependencies dependencies) : base(dependencies)
        {
        }

        protected override Expression VisitTable(TableExpression tableExpression)
        {
            var table = base.VisitTable(tableExpression);
            Sql.Append(" WITH (NO LOCK)");
            return table;
        }
    }

    public class MySqlServerQuerySqlGeneratorFactory : SqlServerQuerySqlGeneratorFactory
    {
        private readonly QuerySqlGeneratorDependencies _dependencies;
        public MySqlServerQuerySqlGeneratorFactory([NotNull] QuerySqlGeneratorDependencies dependencies) : base(dependencies)
        {
            _dependencies = dependencies;
        }

        public override QuerySqlGenerator Create()
        {
            return new MySqlServerQuerySqlGenerator(_dependencies);
        }
    }
}
