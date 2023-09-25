using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Implementations
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    public class Repository<TClass, TId> : IRepository<TClass, TId>
        where TClass : class, IEntity<TId>
        where TId : notnull
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(TId id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(Expression<Func<TClass, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(TId id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Expression<Func<TClass, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TClass? Get(TId id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<TClass?> GetAsync(TId id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TClass? GetData(Expression<Func<TClass, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<TClass?> GetDataAsync(Expression<Func<TClass, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<TClass> GetDatas(Expression<Func<TClass, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<List<TClass>> GetDatasAsync(Expression<Func<TClass, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Insert(TClass entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task InsertAsync(TClass entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void InsertList(IEnumerable<TClass> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task InsertListAsync(IEnumerable<TClass> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable<TClass> Query()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(TClass entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(TClass entity, Expression<Func<TClass, object?>> selector)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task UpdateAsync(TClass entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task UpdateAsync(TClass entity, Expression<Func<TClass, object?>> selector)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateList(IEnumerable<TClass> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="selector"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateList(IEnumerable<TClass> entities, Expression<Func<TClass, object?>> selector)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task UpdateListAsync(IEnumerable<TClass> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task UpdateListAsync(IEnumerable<TClass> entities, Expression<Func<TClass, object?>> selector)
        {
            throw new NotImplementedException();
        }
    }
}
