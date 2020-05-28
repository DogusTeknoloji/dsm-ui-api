using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Helpers
{
    public interface IConvertible<T> where T : class
    {
        T Convert();
    }
}
