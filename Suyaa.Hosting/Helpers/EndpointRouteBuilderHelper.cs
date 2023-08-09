using Suyaa.Hosting.Applications;
using System.Diagnostics;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 路由结点助手
    /// </summary>
    public static class EndpointRouteBuilderHelper
    {
        /// <summary>
        /// 映射所有应用
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IEndpointRouteBuilder MapApplications(this IEndpointRouteBuilder builder)
        {
            var dataSource = builder.DataSources.OfType<ApplicationEndPointDataSource>().FirstOrDefault();
            if (dataSource is null)
            {
                var factory = builder.ServiceProvider.GetRequiredService<ApplicationEndPointDataSourceFactory>();
                builder.DataSources.Add(factory.Create());
            }
            //var factory = builder.ServiceProvider.GetRequiredService<ApplicationEndPointDataSourceFactory>();
            //var dataSource = factory.Create();
            //foreach (var endpoint in dataSource.Endpoints)
            //{
            //    Debug.WriteLine(endpoint.DisplayName);
            //    builder.MapGet(endpoint.DisplayName!, endpoint.RequestDelegate!);
            //}
            return builder;
        }
    }
}
