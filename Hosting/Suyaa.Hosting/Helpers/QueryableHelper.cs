using Suyaa.Hosting.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting;
using Suyaa.Hosting.Kernel;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 查询对象助手
    /// </summary>
    public static class QueryableHelper
    {
        /// <summary>
        /// 设置分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IQueryable<T> Paged<T>(this IQueryable<T> query, IPagedInput input)
        {
            int page = input.Page;
            int pageSize = input.PageSize;
            if (page <= 0) throw new UserFriendlyException($"Page number must be greater than 0");
            if (pageSize <= 0) throw new UserFriendlyException($"Page size must be greater than 0");
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
