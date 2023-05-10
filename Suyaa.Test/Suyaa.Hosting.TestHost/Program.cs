using Microsoft.Extensions.Hosting;
using Suyaa.Hosting.TestHost;

namespace Suyaa.Hosting.TestHost
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Suyaa.Hosting.WebHost.CreateHostBuilder<Startup>().Build().Run();
        }
    }
}


