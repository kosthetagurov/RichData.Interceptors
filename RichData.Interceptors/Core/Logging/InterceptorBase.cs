using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichData.Interceptors.Core.Logging
{
    public abstract class InterceptorBase : DbCommandInterceptor
    {
        public InterceptorBase()
        {

        }

        protected virtual object GetTrace(DbCommand command, CommandEventData eventData)
        {
            return new Trace(command, eventData);
        }

        protected virtual void Trace(object log, Exception exception = null)
        {
            Console.WriteLine(log.ToString());
        }
    }
}
