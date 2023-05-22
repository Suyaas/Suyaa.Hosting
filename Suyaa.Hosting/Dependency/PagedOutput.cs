﻿using System.ComponentModel.DataAnnotations;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 分页输出
    /// </summary>
    public class PagedOutput<T> : IPagedOutput
        where T : class, new()
    {
        /// <summary>
        /// 行数总和
        /// </summary>
        public int RowCount { get; set; } = 0;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; } = 0;
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 0;
        /// <summary>
        /// 数据集合
        /// </summary>
        public T Datas { get; set; } = new T();
    }
}
