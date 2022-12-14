namespace Suyaa.Microservice.Exceptions
{
    /// <summary>
    /// 舒雅服务专用错误
    /// </summary>
    public class SuyaaException : System.Exception
    {
        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="message"></param>
        public SuyaaException(string message) : base(message) { }

    }
}
