using Suyaa.Hosting.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 分页入参助手
    /// </summary>
    public static class PagedInputHelper
    {
        /// <summary>
        /// 创建一个分页输出对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="datas"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static PagedOutput<T> CreatePagedOutput<T>(this PagedInput input, List<T> datas, int rowCount)
            where T : class, new()
        {
            return new PagedOutput<T>(datas, rowCount)
            {
                Page = input.Page,
                PageSize = input.PageSize,
            };
        }
    }
}
