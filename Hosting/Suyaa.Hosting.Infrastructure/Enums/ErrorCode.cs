using System.ComponentModel;

namespace Suyaa.Hosting.Infrastructure.Enums
{
    /// <summary>
    /// 错误码
    /// </summary>
    [Description("错误码")]
    public enum ErrorCode : int
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("未定义")]
        Undefined = 0,
        /// <summary>
        /// 必填
        /// </summary>
        [Description("必填")]
        Required = 0x11,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom = 0xffff,
    }
}
