using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace RichData.Interceptors.Core.Logging
{
    public class LoggingInterceptor : InterceptorBase
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            var log = GetTrace(command, eventData);
            Trace(log);

            return base.ReaderExecuting(command, eventData, result);
        }

        public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            var log = GetTrace(command, eventData);
            Trace(log, eventData.Exception);

            base.CommandFailed(command, eventData);
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            var log = GetTrace(command, eventData);
            Trace(log);

            return base.ReaderExecuted(command, eventData, result);
        }        
    }
}
