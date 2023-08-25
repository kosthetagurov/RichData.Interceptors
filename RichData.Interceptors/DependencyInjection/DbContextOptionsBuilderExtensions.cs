using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using RichData.Interceptors.Core.Caching;
using RichData.Interceptors.Core.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichData.Interceptors.DependencyInjection
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder AddLoggingInterceptor(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new LoggingInterceptor());
            return optionsBuilder;
        }

        public static DbContextOptionsBuilder AddCachingInterceptor(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new CachingInterceptor());
            return optionsBuilder;
        }
    }
}
