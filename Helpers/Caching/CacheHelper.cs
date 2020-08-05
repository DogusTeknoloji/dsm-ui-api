using DSM.UI.Api.Models.Caching;
using DSM.UI.Api.Services;
using MongoDB.Driver;
using System;

namespace DSM.UI.Api.Helpers.Caching
{
    public static class CacheHelper
    {
        public static IMongoDatabase CacheDatabase { get; set; }

        public static bool IsExpired<T>(this T item, CacheDBService<T> cacheInstance) where T : ICachable, new()
        {
            bool expired = (item.ExpireDate - DateTime.UtcNow).TotalSeconds <= 0;
            if (expired)
            {
                cacheInstance.DeleteByItem(item);
            }
            return expired;
        }
    }
}
