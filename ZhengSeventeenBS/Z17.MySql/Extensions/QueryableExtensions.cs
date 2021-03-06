﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Z17.MySql.Extensions
{
    /// <summary>
    /// Some useful extension methods for <see cref="T:System.Linq.IQueryable`1" />.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
        /// </summary>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            bool flag = query == null;
            if (flag)
            {
                throw new ArgumentNullException("query");
            }
            return query.Skip(skipCount).Take(maxResultCount);
        }
        /// <summary>
        /// Filters a <see cref="T:System.Linq.IQueryable`1" /> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <see cref="!:condition" /></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }
        /// <summary>
        /// Filters a <see cref="T:System.Linq.IQueryable`1" /> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <see cref="!:condition" /></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }
    }
}
