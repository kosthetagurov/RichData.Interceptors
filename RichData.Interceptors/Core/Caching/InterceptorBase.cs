using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Runtime.Caching;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RichData.Interceptors.Core.Caching
{
    public abstract class InterceptorBase : DbCommandInterceptor
    {
        protected InterceptionResult<DbDataReader> GetOrSetCommandCache(DbCommand command, InterceptionResult<DbDataReader> defaultResult)
        {
            if (!IsSelectQuery(command.CommandText))
            {
                return defaultResult;
            }

            if (TryGetCacheValue(command.CommandText, out DataTable table))
            {
                return InterceptionResult<DbDataReader>.SuppressWithResult(new DataTableReader(table));
            }
            else
            {
                var data = ExecuteCommand(command);
                SetCache(command.CommandText, data);
                return InterceptionResult<DbDataReader>.SuppressWithResult(new DataTableReader(data));
            }            
        }

        protected void ClearCache(string cacheKey)
        {
            MemoryCacheInstance.Instance.MemoryCache.Remove(cacheKey);
        }

        DataTable ExecuteCommand(DbCommand command)
        {
            using (DbDataReader reader = command.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
        }

        bool IsSelectQuery(string sqlQuery)
        {
            return sqlQuery.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase);
        }

        bool SetCache(string key, DataTable data)
        {
            var cacheItem = new CacheItem(key, data);
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30)
            };
            var result = MemoryCacheInstance.Instance.MemoryCache.Add(cacheItem, cacheItemPolicy);
            return result;
        }

        bool TryGetCacheValue(string key, out DataTable table)
        {
            var isExists = MemoryCacheInstance.Instance.MemoryCache.Contains(key);
            if (isExists)
            {
                table = (DataTable)MemoryCacheInstance.Instance.MemoryCache.Get(key);
            }
            else
            {
                table = null;
            }

            return isExists;
        }        
    }
}
