using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Kernel.Dependency
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IRepository<TClass, TId>
        where TClass : class, IEntity<TId>
        where TId : notnull
    {

        /// <summary>
        /// 获取仓库供应商
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetRepositoryProvider<T>() where T : IRepositoryProvider;

        //#region [=====插入数据=====]

        ///// <summary>
        ///// 添加数据
        ///// </summary>
        ///// <param name="entity">数据实例</param>
        ///// <returns></returns>
        //void Insert(TClass entity);

        ///// <summary>
        ///// 添加数据
        ///// </summary>
        ///// <param name="entity">数据实例</param>
        ///// <returns></returns>
        //Task InsertAsync(TClass entity);

        ///// <summary>
        ///// 添加数据列表
        ///// </summary>
        ///// <param name="entities">数据实例集合</param>
        ///// <returns></returns>
        //void InsertList(IEnumerable<TClass> entities);

        ///// <summary>
        ///// 添加数据列表
        ///// </summary>
        ///// <param name="entities">数据实例集合</param>
        ///// <returns></returns>
        //Task InsertListAsync(IEnumerable<TClass> entities);

        //#endregion

        //#region [=====删除数据=====]

        ///// <summary>
        ///// 删除数据
        ///// </summary>
        ///// <param name="id">Id</param>
        ///// <returns></returns>
        //void Delete(TId id);

        ///// <summary>
        ///// 删除数据
        ///// </summary>
        ///// <param name="id">Id</param>
        ///// <returns></returns>
        //Task DeleteAsync(TId id);

        ///// <summary>
        ///// 删除数据
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //void Delete(Expression<Func<TClass, bool>> predicate);

        ///// <summary>
        ///// 删除数据
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //Task DeleteAsync(Expression<Func<TClass, bool>> predicate);

        //#endregion

        //#region [=====更新数据=====]

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <returns></returns>
        //void Update(TClass entity);

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <returns></returns>
        //Task UpdateAsync(TClass entity);

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <returns></returns>
        //void UpdateList(IEnumerable<TClass> entities);

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <returns></returns>
        //Task UpdateListAsync(IEnumerable<TClass> entities);

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="selector"></param>
        ///// <returns></returns>
        //void Update(TClass entity, Expression<Func<TClass, object?>> selector);

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="selector"></param>
        ///// <returns></returns>
        //Task UpdateAsync(TClass entity, Expression<Func<TClass, object?>> selector);

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="selector"></param>
        ///// <returns></returns>
        //void UpdateList(IEnumerable<TClass> entities, Expression<Func<TClass, object?>> selector);

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="selector"></param>
        ///// <returns></returns>
        //Task UpdateListAsync(IEnumerable<TClass> entities, Expression<Func<TClass, object?>> selector);

        //#endregion

        //#region [=====查询数据=====]

        //IQueryable<TClass> Query();

        ///// <summary>
        ///// 获取单行数据
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //TClass? Get(TId id);

        ///// <summary>
        ///// 获取单行数据
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<TClass?> GetAsync(TId id);

        ///// <summary>
        ///// 获取单行数据
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //TClass? GetData(Expression<Func<TClass, bool>>? predicate = null);

        ///// <summary>
        ///// 获取单行数据
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //Task<TClass?> GetDataAsync(Expression<Func<TClass, bool>>? predicate = null);

        ///// <summary>
        ///// 获取多行数据
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //List<TClass> GetDatas(Expression<Func<TClass, bool>>? predicate = null);

        ///// <summary>
        ///// 获取多行数据
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //Task<List<TClass>> GetDatasAsync(Expression<Func<TClass, bool>>? predicate = null);

        //#endregion
    }
}
