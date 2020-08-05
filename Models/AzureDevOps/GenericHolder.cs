using System.Collections.Generic;

namespace DSM.UI.Api.Models.AzureDevOps
{
    public class GenericHolder<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> Value { get; set; }
    }
}
