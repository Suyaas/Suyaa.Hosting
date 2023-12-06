using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Suyaa.Configure;
using Suyaa.Hosting.Common.Configures.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Suyaa.Hosting.Common.Configures
{
    /// <summary>
    /// 服务配置供应商
    /// </summary>
    public class ConfigurationProvider<TConfig> : ConfigurationProvider
        where TConfig : class, IConfig, new()
    {
        private readonly ConfigurationReloadToken _changeToken;
        private readonly IConfigurationBuilder _builder;
        private readonly string _name;
        private readonly Type _type;
        private readonly Dictionary<string, string> _values;

        /// <summary>
        /// 服务配置供应商
        /// </summary>
        public ConfigurationProvider(IConfigurationBuilder builder)
        {
            _changeToken = new ConfigurationReloadToken();
            _builder = builder;
            _type = typeof(TConfig);
            var attr = _type.GetCustomAttribute<ConfigurationAttribute>();
            _name = attr is null ? _type.Name : attr.Name;
            _values = new Dictionary<string, string>();
        }

        //// 根据类型获取子键集合
        //private IEnumerable<string> GetChildKeys(Type type)
        //{
        //    var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(d => d.CanWrite);
        //    return GetChildKeys(pros);
        //}

        //// 根据属性集合获取子键集合
        //private List<string> GetChildKeys(IEnumerable<PropertyInfo> properties)
        //{
        //    List<string> keys = new List<string>();
        //    foreach (var property in properties)
        //    {
        //        var attr = property.GetCustomAttribute<ConfigurationAttribute>();
        //        var name = attr is null ? property.Name : attr.Name;
        //        keys.Add(name);
        //    }
        //    return keys;
        //}

        ///// <summary>
        ///// 获取子键集合
        ///// </summary>
        ///// <param name="earlierKeys"></param>
        ///// <param name="parentPath"></param>
        ///// <returns></returns>
        //public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string? parentPath)
        //{
        //    if (parentPath is null) return Enumerable.Empty<string>();
        //    if (parentPath.StartsWith(_name))
        //    {
        //        var lv = parentPath.Split(':').Length;
        //        List<string> list = new List<string>();
        //        var keys = _values.Keys.Where(d => d.StartsWith(parentPath) && d != parentPath);
        //        foreach (var key in keys)
        //        {
        //            var strs = key.Split(':');
        //            list.Add(strs[lv]);
        //        }
        //        return list;
        //    }
        //    //if (parentPath == "Hosting")
        //    return Enumerable.Empty<string>();
        //}

        ///// <summary>
        ///// 获取变化令牌
        ///// </summary>
        ///// <returns></returns>
        //public IChangeToken GetReloadToken()
        //{
        //    return _changeToken;
        //}

        // 添加内容
        private void AddValue(object? entity, string path)
        {
            // 处理空对象
            if (entity is null) return;
            // 获取对象类型
            var type = entity.GetType();
            // 处理值
            if (type.IsValueType)
            {
                //_values.Add(path, Convert.ToString(entity) ?? string.Empty);
                Set(path, Convert.ToString(entity));
                return;
            }
            // 处理字符串
            if (entity is string str)
            {
                //_values.Add(path, str);
                Set(path, str);
                return;
            }
            // 处理列表
            if (type.IsBased(typeof(List<>)))
            {
                var propertyCount = type.GetProperty("Count");
                if (propertyCount is null) return;
                var count = (int)(propertyCount.GetValue(entity) ?? 0);
                var methodGet = type.GetMethod("get_Item");
                if (methodGet is null) return;
                for (int i = 0; i < count; i++)
                {
                    var value = methodGet.Invoke(entity, new object[] { i });
                    AddValue(value, path + ":" + (i + 1));
                }
                return;
            }
            // 处理字典
            if (type.IsBased(typeof(Dictionary<,>)))
            {
                var propertyKeys = type.GetProperty("Keys");
                if (propertyKeys is null) return;
                var keys = (IEnumerable<string>)(propertyKeys.GetValue(entity) ?? Enumerable.Empty<string>());
                var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                var methodGet = type.GetMethod("get_Item");
                if (methodGet is null) return;
                foreach (var key in keys)
                {
                    var value = methodGet.Invoke(entity, new object[] { key });
                    AddValue(value, path + ":" + key);
                }
                return;
            }
            // 遍历所有属性
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(d => d.CanWrite);
            foreach (var property in properties)
            {
                var attr = property.GetCustomAttribute<ConfigurationAttribute>();
                var name = attr is null ? property.Name : attr.Name;
                var value = property.GetValue(entity);
                AddValue(value, path + ":" + name);
            }
        }

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            var fileProvider = (PhysicalFileProvider)_builder.GetFileProvider();
            var path = sy.IO.CombinePath(fileProvider.Root, _name + ".json");
            // 不存在则创建一个默认对象
            if (!sy.IO.FileExists(path))
            {
                var defaultValue = sy.Assembly.Create<TConfig>();
                defaultValue.Default();
                var json = sy.Json.Serialize(defaultValue);
                sy.IO.WriteUtf8FileContent(path, json);
            }
            var content = sy.IO.ReadUtf8FileContent(path);
            var config = sy.Json.Deserialize<TConfig>(content);
            // 添加值
            AddValue(config, _name);
        }

        ///// <summary>
        ///// 设置值
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        ///// <exception cref="NotImplementedException"></exception>
        //public void Set(string key, string? value)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// 获取值
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool TryGet(string key, out string? value)
        //{
        //    if (_values.ContainsKey(key))
        //    {
        //        value = _values[key];
        //        return true;
        //    }
        //    value = null;
        //    return false;
        //    //throw new NotImplementedException();
        //}
    }
}
