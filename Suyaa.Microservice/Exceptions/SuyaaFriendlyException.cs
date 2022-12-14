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
        /// 友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public SuyaaFriendlyException(string message, int errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
