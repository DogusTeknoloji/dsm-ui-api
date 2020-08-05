using DSM.UI.Api.Helpers.Caching;
using DSM.UI.Api.Models.Caching;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DSM.UI.Api.Services
{
    public class CacheDBService<T> where T : ICachable, new()
    {
        private readonly IMongoCollection<T> _cachedCollection;
        public CacheDBService(string collectionName)
        {
            IMongoDatabase database = CacheHelper.CacheDatabase;
            this._cachedCollection = database.GetCollection<T>(collectionName);
        }

        public List<T> Get()
        {
            List<T> items = this._cachedCollection.Find(x => true).ToList();
            foreach (T item in items)
            {
                bool expiry = item.IsExpired(this);
            }
            items = this._cachedCollection.Find(x => true).ToList();
            return items;
        }
        public T GetById(string id)
        {
            T item = this._cachedCollection.Find(x => x.CacheId == id).FirstOrDefault();
            if (item.IsExpired(this))
            {
                return default;
            }
            else
            {
                return item;
            }
        }
        public T Create(T item)
        {
            DeleteResult result = this._cachedCollection.DeleteOne(x => x.IsExpired(this));
            if (this.GetById(item.CacheId) == null)
            {
                this._cachedCollection.InsertOne(item);
            }
            return item;
        }
        public IEnumerable<T> CreateMultiple(IEnumerable<T> items, bool overwrite)
        {
            if (overwrite)
            {
                DeleteResult result = this._cachedCollection.DeleteMany(x => true);
            }
            else
            {
                this._cachedCollection.DeleteMany(x => x.IsExpired(this));
            }
            this._cachedCollection.InsertMany(items);
            return items;
        }
        public void UpdateById(string id, T item)
        {
            if (!item.IsExpired(this))
            {
                ReplaceOneResult result = this._cachedCollection.ReplaceOne(x => x.CacheId == id, item);
            }
        }
        public void DeleteByItem(T item)
        {
            DeleteResult result = this._cachedCollection.DeleteOne(x => x.CacheId == item.CacheId);
        }
        public void DeleteById(string id) => this._cachedCollection.DeleteOne(x => x.CacheId == id);
    }
}
