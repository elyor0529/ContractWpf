using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Contract.Core.Extensions
{
    public static class MaybeExtensions
    {

        public static T FindOrDefault<T>(this IEnumerable<T> query, Func<T, bool> predicate = null)
        {
            var t = predicate == null
                ? query.FirstOrDefault()
                : query.FirstOrDefault(predicate);

            if (t == null)
                return Activator.CreateInstance<T>();

            return t;
        }

        public static TResult With<T, TResult>(this T input, Func<T, TResult> expr)
        {
            if (input == null)
                return default(TResult);
            return expr(input);
        }

        public static TResult Return<T, TResult>(this T input, Func<T, TResult> expr, TResult fallBackValue)
        {
            if (input == null)
                return fallBackValue;
            var val = expr(input);
            if (val == null)
                return fallBackValue;
            return val;
        }

        public static T Try<T>(this T input, Action<T> action)
        {
            if (input != null)
                action(input);
            return input;
        }

        public static void TryAs<T>(this object obj, Action<T> action) where T : class
        {
            Guard.NotNull(action, "action");
            var casted = obj as T;
            if (casted != null)
                action(casted);
        }

        public static string OrFollowingIfNull(this string value, string alt, params string[] alts)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return value;
            if (!string.IsNullOrWhiteSpace(alt))
                return alt;
            return alts.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
        }

        public static Exception TrySafe<T>(this T obj, Action<T> action)
        {
            if (obj != null)
                try
                {
                    action(obj);
                }
                catch (Exception ex)
                {
                    return ex;
                }
            return null;
        }

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
            params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            return query;
        }
    }
}