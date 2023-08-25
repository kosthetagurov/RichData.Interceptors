using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichData.Interceptors.Core.Logging
{
    public class Trace
    {
        public Trace(DbCommand command, CommandEventData eventData)
        {
            EventData = eventData;
            Command = command;
            Date = DateTime.Now;
        }

        public DateTime Date { get; }
        public DbCommand Command { get; }
        public CommandEventData EventData { get; }
        public string SenderMethod { get; }

        public override string ToString()
        {
            return $"[{Date}] [{Command.CommandType}] [{Command.CommandText}]";
        }

        public string ToString(Exception exception = null)
        {
            var trace = ToString();

            if (exception != null)
            {
                trace += $"[Error: {exception.Message} {exception.StackTrace} {exception.InnerException?.Message} {exception.InnerException?.StackTrace}]";
            }

            return trace;
        }
    }
}
