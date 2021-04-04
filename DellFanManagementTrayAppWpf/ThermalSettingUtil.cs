using DellFanManagement.SmmIo;
using JKang.IpcServiceFramework.Client;
using Microsoft.Extensions.DependencyInjection;
using ServiceContract;
using System.Threading.Tasks;

namespace DellFanManagementTrayAppWpf
{
    public class ThermalSettingUtil
    {

        const string CLIENT_NAME = "DellThermalSettingTrayApp";
        const string PIPE_NAME = "DellThermalSettingService";

        public static IIpcClient<IThermalSettingService> CreateClient()
        {
            // register IPC clients
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddNamedPipeIpcClient<IThermalSettingService>(CLIENT_NAME, pipeName: PIPE_NAME)
                .BuildServiceProvider();

            // resolve IPC client factory
            IIpcClientFactory<IThermalSettingService> clientFactory = serviceProvider
                .GetRequiredService<IIpcClientFactory<IThermalSettingService>>();

            // create client
            IIpcClient<IThermalSettingService> client = clientFactory.CreateClient(CLIENT_NAME);

            return client;
        }

        public static void SetThermalSetting(ThermalSetting thermalSetting)
        {
            CreateClient().InvokeAsync(service => service.SetThermalSetting(thermalSetting));
        }

        public async static Task<string> GetThermalSetting()
        {
            ThermalSetting thermalSetting = await CreateClient().InvokeAsync(service => service.GetThermalSetting());
            return thermalSetting.ToString();
        }
    }
}
