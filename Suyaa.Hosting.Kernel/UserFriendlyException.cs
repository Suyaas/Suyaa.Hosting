using Suyaa.Hosting.Kernel.Enums;

namespace Suyaa.Hosting.Kernel
{
    /// <summary>
    /// 用户友好错误
    /// </summary>
    public class UserFriendlyException : HostException
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// 消息参数
        /// </summary>
        public object?[] MessageParams { get; }

        /// <summary>
        /// 主机友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public UserFriendlyException(string message) : base(message)
        {
            ErrorCode = 0;
            MessageParams = new object?[0];
        }

        /// <summary>
        /// 主机友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public UserFriendlyException(ErrorCode errorCode, string message, params object?[] args) : base(message)
        {
            ErrorCode = errorCode;
            MessageParams = args;
        }

        /// <summary>
        /// 主机友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public UserFriendlyException(string message, params object?[] args) : base(message)
        {
            ErrorCode = 0;
            MessageParams = new object?[0];
        }

        /// <summary>
        /// 主机友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="exception"></param>
        public UserFriendlyException(Exception exception, ErrorCode errorCode, string message) : base(message, exception)
        {
            ErrorCode = errorCode;
            MessageParams = new object?[0];
        }
    }
}
