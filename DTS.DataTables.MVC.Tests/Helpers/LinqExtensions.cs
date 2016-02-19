using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTS.DataTables.MVC.Tests.Helpers
{
    public static class LinqExtensions
    {

        public static TSource Set<TSource>(this TSource input, Action<TSource> updater)
        {
            updater(input);
            return input;
        }

        #region OrderBy
        // Can't take credit for this extension but it's brilliant
        // http://aonnull.blogspot.com/2010/08/dynamic-sql-like-linq-orderby-extension.html

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        /// <remarks>USAGE: persons.OrderBy("lastName ASC, firstName DESC")</remarks>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable, string orderBy)
        {
            return enumerable.AsQueryable().OrderBy(orderBy).AsEnumerable();
        }


        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> collection, string orderBy)
        {
            foreach (OrderByInfo orderByInfo in ParseOrderBy(orderBy))
                collection = ApplyOrderBy<T>(collection, orderByInfo);

            return collection;
        }




        /// <summary>
        /// Applies the order by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="orderByInfo">The order by information.</param>
        /// <returns></returns>
        private static IQueryable<T> ApplyOrderBy<T>(IQueryable<T> collection, OrderByInfo orderByInfo)
        {
            string[] props = orderByInfo.PropertyName.Split('.');
            Type type = typeof(T);

            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                //PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
            string methodName = String.Empty;

            if (!orderByInfo.Initial && collection is IOrderedQueryable<T>)
            {
                if (orderByInfo.Direction == SortDirection.Ascending)
                    methodName = "ThenBy";
                else
                    methodName = "ThenByDescending";
            }
            else
            {
                if (orderByInfo.Direction == SortDirection.Ascending)
                    methodName = "OrderBy";
                else
                    methodName = "OrderByDescending";
            }

            //TODO: apply caching to the generic methodsinfos?
            return (IOrderedQueryable<T>)typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { collection, lambda });

        }

        /// <summary>
        /// Parses the order by.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Invalid Property. Order By Format: Property, Property2 ASC, Property2 DESC
        /// </exception>
        private static IEnumerable<OrderByInfo> ParseOrderBy(string orderBy)
        {
            if (String.IsNullOrEmpty(orderBy))
                yield break;

            string[] items = orderBy.Split(',');
            bool initial = true;
            foreach (string item in items)
            {
                string[] pair = item.Trim().Split(' ');

                if (pair.Length > 2)
                    throw new ArgumentException(String.Format("Invalid OrderBy string '{0}'. Order By Format: Property, Property2 ASC, Property2 DESC", item));

                string prop = pair[0].Trim();

                if (String.IsNullOrEmpty(prop))
                    throw new ArgumentException("Invalid Property. Order By Format: Property, Property2 ASC, Property2 DESC");

                SortDirection dir = SortDirection.Ascending;

                if (pair.Length == 2)
                    dir = ("desc".Equals(pair[1].Trim(), StringComparison.OrdinalIgnoreCase) ? SortDirection.Descending : SortDirection.Ascending);

                yield return new OrderByInfo() { PropertyName = prop, Direction = dir, Initial = initial };

                initial = false;
            }

        }

        /// <summary>
        /// OrderByInfo
        /// </summary>
        private class OrderByInfo
        {
            /// <summary>
            /// Gets or sets the name of the property.
            /// </summary>
            /// <value>
            /// The name of the property.
            /// </value>
            public string PropertyName { get; set; }

            /// <summary>
            /// Gets or sets the direction.
            /// </summary>
            /// <value>
            /// The direction.
            /// </value>
            public SortDirection Direction { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="OrderByInfo"/> is initial.
            /// </summary>
            /// <value>
            ///   <c>true</c> if initial; otherwise, <c>false</c>.
            /// </value>
            public bool Initial { get; set; }
        }

        /// <summary>
        /// Sort Direction enum
        /// </summary>
        private enum SortDirection
        {
            /// <summary>
            /// The ascending
            /// </summary>
            Ascending = 0,
            /// <summary>
            /// The descending
            /// </summary>
            Descending = 1
        }



        #endregion

    }
}
