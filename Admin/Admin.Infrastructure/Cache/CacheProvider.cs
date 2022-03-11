using Admin.Application.Contracts;
using Enyim.Caching;
using System.Threading.Tasks;

namespace Admin.Infrastructure.Cache
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IMemcachedClient memcachedClient;

        public CacheProvider(IMemcachedClient memcachedClient)
        {
            this.memcachedClient = memcachedClient;
        }

        public T GetCache<T>(string key)
        {
            return memcachedClient.Get<T>(key);
        }
    }
}
