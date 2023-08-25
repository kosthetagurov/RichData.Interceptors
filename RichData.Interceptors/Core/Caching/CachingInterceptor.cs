using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichData.Interceptors.Core.Caching
{
    public class CachingInterceptor : InterceptorBase
    {
        public CachingInterceptor()
        {
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            return GetOrSetCommandCache(command, result);
        }

        public override async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            return await ValueTask.FromResult(GetOrSetCommandCache(command, result));
        }

        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            ClearCache(command.CommandText);
            return base.NonQueryExecuted(command, eventData, result);
        }

        public override async ValueTask<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            ClearCache(command.CommandText);
            return await base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
        }
    }
}
