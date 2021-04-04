using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Logging;
using ServiceContract;
using JKang.IpcServiceFramework.Hosting;
using Topshelf;
using Topshelf.Extensions.Hosting;

namespace DellThermalSettingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureLogging(options => options.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information))
                .ConfigureServices(services => services.AddScoped<IThermalSettingService, InterProcessService>())
                .ConfigureIpcHost(builder => builder.AddNamedPipeEndpoint<IThermalSettingService>(pipeName: PIPE_NAME));

            builder.RunAsTopshelfService(hc =>
            {
                hc.SetServiceName("DellThermalSettingService");
                hc.SetDisplayName("DellThermalSettingService");
                hc.SetDescription("This service enables the control of the Thermal profile of a Dell laptop.");
            });
        }

        const string PIPE_NAME = "DellThermalSettingService";

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureLogging(options => options.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information))
            .ConfigureServices(services => services.AddScoped<IThermalSettingService, InterProcessService>())
            .ConfigureIpcHost(builder => builder.AddNamedPipeEndpoint<IThermalSettingService>(pipeName: PIPE_NAME))
            .UseWindowsService();

    }
}
