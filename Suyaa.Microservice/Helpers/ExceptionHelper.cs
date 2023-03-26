using Suyaa.Microservice.Exceptions;
using Suyaa.Microservice.Results;

namespace Suyaa.Microservice.Helpers
{
    /// <summary>
    /// 异常助手
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// 转化为可输出的Api返回结果
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static IApiResult ToApiResult(this Exception ex)
        {
            switch (ex)
            {
                case FriendlyException friendlyException:
                    return new ApiErrorResult()
                    {
                        Message = friendlyException.Message,
                        ErrorCode = friendlyException.ErrorCode,
                    };
                default:
                    if (ex.InnerException is null) return new ApiErrorResult()
                    {
                        Message = $"An error occurred while executing.",
                        ErrorCode = 0,
                    };
                    return ex.InnerException.ToApiResult();
            }
        }
    }
}
