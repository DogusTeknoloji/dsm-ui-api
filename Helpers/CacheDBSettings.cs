using System;

namespace DSM.UI.Api.Helpers
{
    public interface ICacheDBSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
    public class CacheDBSettings : ICacheDBSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
