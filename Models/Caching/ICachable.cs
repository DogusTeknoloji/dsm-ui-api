using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DSM.UI.Api.Models.Caching
{
    public interface ICachable
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        string CacheId { get; set; }
        DateTime CreateDate { get; set; }
        DateTime ExpireDate { get; set; }
    }
}
