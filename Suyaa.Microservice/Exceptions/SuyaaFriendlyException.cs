namespace Suyaa.Microservice.Exceptions
{
    /// <summary>
    /// 舒雅服务专用友好错误
    /// </summary>
    public class SuyaaFriendlyException : SuyaaException
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 舒雅服务专用友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public SuyaaFriendlyException(string message, int errorCode = 0) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// 舒雅服务专用友好错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="exception"></param>
        public SuyaaFriendlyException(string message, int errorCode, Exception exception) : base(message, exception)
        {
            this.ErrorCode = errorCode;
        }
    }
}
