using DellFanManagement.SmmIo;
using ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellThermalSettingService
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
