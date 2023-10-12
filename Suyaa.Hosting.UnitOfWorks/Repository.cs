using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.UnitOfWorks
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public class Repository<TClass, TId> : IRepository<TClass, TId>
        where TClass : class, IEntity<TId>
        where TId : notnull
    {

        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private readonly Dictionary<Type, object> _providers;

        public Repository(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
            _providers = new Dictionary<Type, object>();
        }

        #endregion

        /// <summary>
        /// 获取仓库供应商
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetRepositoryProvider<T>() where T : IRepositoryProvider
        {
            var type = typeof(T);
            if (_providers.ContainsKey(type)) return (T)_providers[type];
            var provider = _dependencyManager.Resolve<T>();
            _providers[type] = provider;
            return provider;
        }
    }
}