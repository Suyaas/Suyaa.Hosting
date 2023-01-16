namespace Suyaa.Microservice.Exceptions
{
    /// <summary>
    /// 舒雅服务专用错误
    /// </summary>
    public class SuyaaException : System.Exception
    {
        /// <summary>
        /// 舒雅服务专用错误
        /// </summary>
        /// <param name="message"></param>
        public SuyaaException(string message) : base(message) { }

        /// <summary>
        /// 舒雅服务专用错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public SuyaaException(string message, Exception exception) : base(message, exception) { }

    }
}
