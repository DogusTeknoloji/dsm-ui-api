using DSM.UI.Api.Models.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Helpers.ServerComparer
{
    public class DetailsSiteComparer : IEqualityComparer<DetailsSites>
    {
        private static DetailsSiteComparer _instance;
        protected DetailsSiteComparer()
        {

        }
        public static DetailsSiteComparer Instance
        {
            get
            {
                if (_instance == null) _instance = new DetailsSiteComparer();
                return _instance;
            }
        }
        public bool Equals([AllowNull] DetailsSites x, [AllowNull] DetailsSites y)
        {
            return x.SiteName == y.SiteName;
        }

        public int GetHashCode([DisallowNull] DetailsSites obj)
        {
            return obj.GetHashCode();
        }
    }
}
