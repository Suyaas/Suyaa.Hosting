using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Implementations
{
    /// <summary>
    /// 内存Session
    /// </summary>
    public class AsyncLocalSession : ISession, IDependencyTransient
    {
        // 异步数据对象
        private static readonly AsyncLocal<AsyncLocalSessionWrapper> _asyncLocal = new AsyncLocal<AsyncLocalSessionWrapper>();

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string? Uid { get => GetCurrentData().Uid; set => GetCurrentData().Uid = value; }
        /// <summary>
        /// 租户Id
        /// </summary>
        public int? TenantId { get => GetCurrentData().TenantId; set => GetCurrentData().TenantId = value; }
        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime? InvalidTime { get => GetCurrentData().InvalidTime; set => GetCurrentData().InvalidTime = value; }

        /// <summary>
        /// 数据
        /// </summary>
        private AsyncLocalSessionWrapper GetCurrentData()
        {
            // 为空则返回空
            if (_asyncLocal.Value is null)
            {
                lock (_asyncLocal)
                {
                    _asyncLocal.Value = new AsyncLocalSessionWrapper();
                }
            }
            return _asyncLocal.Value;
        }
    }

    /// <summary>
    /// 内存Session包裹层
    /// </summary>
    public class AsyncLocalSessionWrapper
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string? Uid { get; set; }
        /// <summary>
        /// 租户Id
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime? InvalidTime { get; set; }
    }
}
