using DSM.UI.Api.Models.Company;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DSM.UI.Api.Helpers.CompanyComparer
{
    public class DetailsServerComparer : IEqualityComparer<DetailsServers>
    {
        private static DetailsServerComparer _instance;
        protected DetailsServerComparer()
        {

        }
        public static DetailsServerComparer Instance
        {
            get
            {
                if (_instance == null) _instance = new DetailsServerComparer();
                return _instance;
            }
        }
        public bool Equals([AllowNull] DetailsServers x, [AllowNull] DetailsServers y)
        {
            return x.ServerName == y.ServerName;
        }

        public int GetHashCode([DisallowNull] DetailsServers obj)
        {
            return obj.ServerName.GetHashCode();
        }
    }
}