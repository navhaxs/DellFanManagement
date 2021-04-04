using JKang.IpcServiceFramework.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceContract;

namespace ConsoleServerApp
{
    class Program
    {
        const string PIPE_NAME = "DellThermalSettingService";

        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //Console.WriteLine("Hello World!");
            //InterProcessService interProcessService = new InterProcessService();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
           .ConfigureServices(services =>
           {
               services.AddScoped<IThermalSettingService, InterProcessService>();
           })
           .ConfigureIpcHost(builder =>
           {
                // configure IPC endpoints
                builder.AddNamedPipeEndpoint<IThermalSettingService>(pipeName: PIPE_NAME);
           })
           .ConfigureLogging(builder =>
           {
                // optionally configure logging
                builder.SetMinimumLevel(LogLevel.Information);
           });
    }
}
