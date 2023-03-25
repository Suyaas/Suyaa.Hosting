namespace Suyaa.Microservice.Exceptions
{
    /// <summary>
    /// 服务专用异常
    /// </summary>
    public class MicroserviceException : System.Exception
    {
        /// <summary>
        /// 舒雅服务专用错误
        /// </summary>
        /// <param name="message"></param>
        public MicroserviceException(string message) : base(message) { }

        /// <summary>
        /// 舒雅服务专用错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public MicroserviceException(string message, Exception exception) : base(message, exception) { }

    }
}
