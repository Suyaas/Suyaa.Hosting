using Microsoft.Extensions.Hosting;
using Suyaa.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogger = Suyaa.Logs.Dependency.ILogger;

namespace ServicesDemo
{
    /// <summary>
    /// 主机后台服务
    /// </summary>
    public class HostBackgroundService : BackgroundService
    {
        #region DI注入

        private readonly ILogger _logger;

        /// <summary>
        /// 主机后台服务
        /// </summary>
        public HostBackgroundService(
            ILogger logger
            )
        {
            _logger = logger;
        }

        #endregion

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Debug("HostBackgroundService is starting.");

            stoppingToken.Register(() => _logger.Debug("HostBackgroundService is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.Debug("HostBackgroundService is doing background work.");

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _logger.Debug("ServiceA has stopped.");
        }
    }
}
