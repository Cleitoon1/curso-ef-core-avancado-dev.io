using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Curso.Dominando.EFCore.Modulo2122.ComportamentosEDiagnostics
{
    public class MyInterceptor : IObserver<KeyValuePair<string, object>>
    {
        private static readonly Regex _tableAliasRegex =
            new Regex(@"(?<tableAlias>FROM +(\[.*\]\.)?(\[.*\]) AS (\[.*\])(?! WITH \(NOLOCK\)))",
                RegexOptions.Multiline |
                RegexOptions.IgnoreCase |
                RegexOptions.Compiled);

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            if(value.Key == RelationalEventId.CommandExecuting.Name)
            {
                var command = ((CommandEventData)value.Value).Command;
                if (!command.CommandText.Contains("WITH (NO LOCK)"))
                {
                    command.CommandText = _tableAliasRegex.Replace(command.CommandText, "${tableAlias} WITH (NO LOCK)");
                    Console.WriteLine(command.CommandText);
                }
            }
        }
    }

    public class MyInterceptorListener : IObserver<DiagnosticListener>
    {
        private MyInterceptor _interceptor = new MyInterceptor();

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DiagnosticListener listener)
        {
            if (listener.Name == DbLoggerCategory.Name)
                listener.Subscribe(_interceptor);
            
        }
    }
}
