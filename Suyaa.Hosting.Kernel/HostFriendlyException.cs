namespace Suyaa.Hosting.Kernel
{
    /// <summary>
    /// 主机友好错误
    /// </summary>
    public class HostFriendlyException : HostException
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 主机友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public HostFriendlyException(string message, int errorCode = 0) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 主机友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="exception"></param>
        public HostFriendlyException(string message, int errorCode, Exception exception) : base(message, exception)
        {
            ErrorCode = errorCode;
        }
    }
}
