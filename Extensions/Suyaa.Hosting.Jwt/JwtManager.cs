using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Jwt.Dependency;

namespace Suyaa.Hosting.Jwt
{
    /// <summary>
    /// Jwt管理器
    /// </summary>
    public class JwtManager<TData> : IJwtManager<TData>
        where TData : class, IJwtData
    {
        // 异步数据对象
        private static readonly AsyncLocal<JwtDataWrapper> _asyncLocal = new AsyncLocal<JwtDataWrapper>();

        #region DI注入

        private readonly IDependencyManager _dependency;
        private IJwtDataProvider<TData>? _provider;

        /// <summary>
        /// Jwt管理器
        /// </summary>
        public JwtManager(
            IDependencyManager dependency
            )
        {
            _dependency = dependency;
        }

        #endregion

        /// <summary>
        /// 获取当前数据
        /// </summary>
        public TData Current => GetCurrentData();

        /// <summary>
        /// 数据
        /// </summary>
        public TData GetCurrentData()
        {
            // 为空则返回空
            if (_asyncLocal.Value is null)
            {
                lock (_asyncLocal)
                {
                    var jwtDataProvider = _dependency.ResolveRequired<IJwtDataProvider<TData>>();
                    _asyncLocal.Value = new JwtDataWrapper(jwtDataProvider.CreateJwtData());
                }
            }
            return (TData)_asyncLocal.Value.JwtData;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public void SetCurrentData(TData data)
        {
            // 设置数据
            lock (_asyncLocal)
            {
                _asyncLocal.Value = new JwtDataWrapper(data);
            }
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string? Uid => this.Current?.Uid;

        /// <summary>
        /// 数据供应商
        /// </summary>
        public IJwtDataProvider<TData> Provider => _provider ??= _dependency.ResolveRequired<IJwtDataProvider<TData>>();
    }
}
