using DellFanManagement.SmmIo;

namespace ServiceContract
{
    public interface IThermalSettingService
    {
        bool SetThermalSetting(ThermalSetting thermalSetting);

        ThermalSetting GetThermalSetting();
    }
}
