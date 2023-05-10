namespace Suyaa.Hosting
{
    /// <summary>
    /// 主机专用异常
    /// </summary>
    public class HostException : Exception
    {
        /// <summary>
        /// 主机专用异常
        /// </summary>
        /// <param name="message"></param>
        public HostException(string message) : base(message) { }

        /// <summary>
        /// 主机专用异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public HostException(string message, Exception exception) : base(message, exception) { }

    }
}
