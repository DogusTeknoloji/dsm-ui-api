using DSM.UI.Api.Models.Server;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Helpers.ServerComparer
{
    public class SearchResultComparer : IEqualityComparer<SearchResult>
    {
        private static SearchResultComparer _instance;
        public SearchResultComparer()
        {

        }
        public static SearchResultComparer Instance
        {
            get
            {
                if (_instance == null) _instance = new SearchResultComparer();
                return _instance;
            }
        }
        public bool Equals(SearchResult x, SearchResult y)
        {
            return x.MachineName == y.MachineName;
        }

        public int GetHashCode([DisallowNull] SearchResult obj)
        {
            return obj.GetHashCode();
        }
    }
}
