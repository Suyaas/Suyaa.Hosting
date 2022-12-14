namespace Suyaa.Microservice
{
    /// <summary>
    /// Web服务
    /// </summary>
    public class WebHost
    {
        /// <summary>
        /// 创建WehHost
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder<TStartup>(string[]? args = null)
            where TStartup : class
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
                webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });
    }
}