using Admin.Application.Contracts;
using Enyim.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Infrastructure.Cache
{
    public class CacheRepository: ICacheRepository
    {
        private readonly IMemcachedClient memcachedClient;

        public CacheRepository(IMemcachedClient memcachedClient)
        {
            this.memcachedClient = memcachedClient;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            // Setting cache expiration for an hour
            await memcachedClient.SetAsync(key, value, 60 * 60);
        }
    }
}
