namespace Suyaa.Microservice.Exceptions
{
    /// <summary>
    /// 友好错误
    /// </summary>
    public class SuyaaFriendlyException : SuyaaException
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public SuyaaFriendlyException(string message, int errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
