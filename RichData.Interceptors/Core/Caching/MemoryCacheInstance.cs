using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace RichData.Interceptors.Core.Caching
{
    internal class MemoryCacheInstance
    {
        private static MemoryCacheInstance _instance;
        private static readonly object _lock = new object();

        public MemoryCache MemoryCache { get; }

        private MemoryCacheInstance()
        {
            MemoryCache = new MemoryCache("RichData.Interceptors.Core.Caching");
        }

        public static MemoryCacheInstance Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MemoryCacheInstance();
                    }
                }

                return _instance;
            }
        }
    }
}
