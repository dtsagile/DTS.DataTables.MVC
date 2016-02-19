using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS.DataTables.MVC
{
    public static class LinqExtensions
    {
        public static TSource Set<TSource>(this TSource input, Action<TSource> updater)
        {
            updater(input);
            return input;
        }

    }
}
