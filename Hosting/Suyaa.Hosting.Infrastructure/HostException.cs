namespace Suyaa.Hosting.Kernel
{
    /// <summary>
    /// 主机专用异常
    /// </summary>
    public class HostException : KeyException
    {
        /// <summary>
        /// 主机
        /// </summary>
        public const string KEY_HOST = "Host";

        /// <summary>
        /// 主机专用异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public HostException(string message, params string[] parameters) : base(KEY_HOST, message, parameters) { }

        /// <summary>
        /// 主机专用异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// <param name="innerException"></param>
        public HostException(Exception innerException, string message, params string[] parameters) : base(KEY_HOST, innerException, message, parameters) { }

    }
}
