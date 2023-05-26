using System.Linq.Expressions;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 可查询助手
    /// </summary>
    public static class QueryableHelper
    {
        /// <summary>
        /// 满足条件时增加查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>>? expression)
        {
            if (expression is null) return query;
            if (condition) query.Where(expression);
            return query;
        }
    }
}
