using DellFanManagement.SmmIo;
using System;
using System.Windows.Input;

namespace DellFanManagementTrayAppWpf
{
    public class SetThermalSettingCommand : ICommand
    {
        public static ICommand Instance { get; } = new SetThermalSettingCommand();

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ThermalSetting parsed = (ThermalSetting) Enum.Parse(typeof(ThermalSetting), (String) parameter);
            ThermalSettingUtil.SetThermalSetting(parsed);
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        
    }
}
