using Suyaa.Hosting.Infrastructure.Enums;
using Suyaa.Hosting.Kernel;

namespace Suyaa.Hosting.Common.Exceptions
{
    /// <summary>
    /// 用户友好异常
    /// </summary>
    public class UserFriendlyException : KeyException
    {
        /// <summary>
        /// 友好的
        /// </summary>
        public const string KEY_FRIENDLY = "Friendly";

        /// <summary>
        /// 错误码
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// 主机友好错误
        /// </summary>
        /// <param name="message"></param>
        public UserFriendlyException(string message, params string[] parameters) : base(KEY_FRIENDLY, message, parameters)
        {
            ErrorCode = ErrorCode.Undefined;
        }

        /// <summary>
        /// 主机友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// <param name="errorCode"></param>
        public UserFriendlyException(ErrorCode errorCode, string message, params string[] parameters) : base(KEY_FRIENDLY, message, parameters)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 主机友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="innerException"></param>
        /// <param name="parameters"></param>
        public UserFriendlyException(Exception innerException, ErrorCode errorCode, string message, params string[] parameters) : base(KEY_FRIENDLY, innerException, message, parameters)
        {
            ErrorCode = errorCode;
        }
    }
}
