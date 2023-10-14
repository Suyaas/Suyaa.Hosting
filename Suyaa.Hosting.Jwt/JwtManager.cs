using Suyaa.DependencyInjection;
using Suyaa.Hosting.Jwt.Dependency;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.Jwt
{
    /// <summary>
    /// Jwt管理器
    /// </summary>
    public class JwtManager : IJwtManager
    {
        // 异步数据对象
        private static readonly AsyncLocal<JwtDataWrapper> _asyncLocal = new AsyncLocal<JwtDataWrapper>();

        #region DI注入

        private readonly IDependencyManager _dependency;

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
        public IJwtData Current => GetCurrentData();

        /// <summary>
        /// 数据
        /// </summary>
        public IJwtData GetCurrentData()
        {
            // 为空则返回空
            if (_asyncLocal.Value is null)
            {
                lock (_asyncLocal)
                {
                    var jwtDataProvider = _dependency.Resolve<IJwtDataProvider>();
                    _asyncLocal.Value = new JwtDataWrapper(jwtDataProvider.CreateJwtData());
                }
            }
            return _asyncLocal.Value.JwtData;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public void SetCurrentData(IJwtData jwtData)
        {
            // 设置数据
            lock (_asyncLocal)
            {
                _asyncLocal.Value = new JwtDataWrapper(jwtData);
            }
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string? Uid => this.Current?.Uid;
    }
}
