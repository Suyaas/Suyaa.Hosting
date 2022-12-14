using Microsoft.Extensions.Hosting;
using Suyaa.Microservice.TestHost;

namespace Suyaa.Microservice.TestHost
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Suyaa.Microservice.WebHost.CreateHostBuilder<Startup>().Build().Run();
        }
    }
}


