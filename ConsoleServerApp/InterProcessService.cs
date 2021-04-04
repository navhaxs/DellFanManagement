using DellFanManagement.SmmIo;
using ServiceContract;

namespace ConsoleServerApp
{
    class InterProcessService : IThermalSettingService
    {
        public ThermalSetting GetThermalSetting()
        {
            return DellSmmIoLib.GetThermalSetting();
        }

        public bool SetThermalSetting(ThermalSetting thermalSetting)
        {
            return DellSmmIoLib.SetThermalSetting(thermalSetting);
        }
    }
}
