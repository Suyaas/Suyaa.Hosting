namespace Suyaa.Hosting.Multilingual.Dependency
{
    /// <summary>
    /// 多语言支持
    /// </summary>
    public interface IMultilingualManager
    {
        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        string Content(string content, params object?[] args);
    }
}
