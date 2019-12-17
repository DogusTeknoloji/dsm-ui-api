using System;
using System.Collections.Generic;
using System.Linq;

namespace DSM.UI.Api.Helpers
{
    public static class MapHelper
    {
        public static TOut Map<TOut, TIn>(TIn obj)
                                            where TOut : IMappable<TIn>
                                            where TIn : class
        {
            TOut instance = Activator.CreateInstance<TOut>();
            instance.Map(obj);
            return instance;
        }

        public static IEnumerable<TOut> Map<TOut, TIn>(IEnumerable<TIn> objs)
                                                                        where TOut : IMappable<TIn>
                                                                        where TIn : class
        {
            List<TOut> outs = new List<TOut>();
            List<TIn> ins = objs.ToList();
            ins.ForEach(i => outs.Add((TOut)Activator.CreateInstance<TOut>().Map(i)));
            return outs;
        }
    }
}
