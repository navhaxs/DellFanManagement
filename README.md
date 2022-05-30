# Dell Thermal Setting Tray App

A no frills tray app (backed by a Windows service) to adjust the thermal settings of Dell laptops.

![screenshot](https://user-images.githubusercontent.com/5162718/113504987-4e2eb080-957f-11eb-90f0-0f6d82a2732a.PNG)

Compared to official Dell Power Manager app which takes ~28 seconds to start up, my trap app gives me **immediate and easy access** to switching thermal profiles!!

> **This is a fork of [AaronKelley/DellFanManagement](https://github.com/AaronKelley/DellFanManagement). Full credit and kudos to Aaron for the Dell Smbios library code. All I did was create a GUI and Windows service.**

# Free Download

See [Releases](https://github.com/navhaxs/DellFanManagement/releases/) for download

Requires Dell Power Manager and Windows 10. Tested on Dell XPS 15 9570 (2018) and Dell Latitude 7390 2-in-1 (2018) models. 

Tested on Windows 10 x64

> The app is published as a "self-contained" net6.0 app so you don't need any frameworks etc installed locally at all (pretty cool!)

# Instructions

Unzip to somwehere e.g. `C:\opt\DellFanManagement\`

Run as Administrator:
```
DellThermalSettingService.exe install
DellThermalSettingService.exe start
```

Then launch the GUI tray app `DellFanManagementTrayAppWpf.exe`.

Choose 'Autostart' from tray menu so it auto-starts on startup

# How to uninstall

Run as Administrator:
```
DellThermalSettingService.exe uninstall
```

From the tray menu, unselect 'Autostart', then 'Exit'

Finally, delete `C:\opt\DellFanManagement\` :)

# Misc

If building yourself, the default EXE build paths are:

```
DellThermalSettingService\bin\Release\net6.0-windows\win-x64\
DellFanManagementTrayAppWpf\bin\Release\net6.0-windows\win-x64\
```