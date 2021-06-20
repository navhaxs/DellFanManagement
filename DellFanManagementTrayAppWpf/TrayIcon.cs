using DellFanManagement.SmmIo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DellFanManagementTrayAppWpf
{
    public class TrayIcon
    {

        public static NotifyIcon notifyicon;
        /// <summary>
        /// Creates notify icon and menu.
        /// </summary>
        public static void CreateNotifyIcon()
        {
            notifyicon = new System.Windows.Forms.NotifyIcon();

            notifyicon.MouseUp += Notifyicon_MouseUp; ;

            var currentThermalSetting = ThermalSettingUtil.GetThermalSetting();

            //System.Windows.Forms.MenuItem mnuMonitorOff = new System.Windows.Forms.MenuItem("Power off display", new EventHandler(this.MonitorOffMenuEventHandler));
            //System.Windows.Forms.MenuItem mnuScreenSaver = new System.Windows.Forms.MenuItem("Start screen saver", new EventHandler(this.StartScreenSaverMenuEventHandler));
            //System.Windows.Forms.MenuItem mnuSleep = new System.Windows.Forms.MenuItem("Enter sleep mode", new EventHandler(this.SleepMenuEventHandler));
            //System.Windows.Forms.MenuItem mnuCaffeine = new System.Windows.Forms.MenuItem("Caffeine", new EventHandler(this.CaffeineMenuEventHandler));
            //mnuAutostart = new System.Windows.Forms.MenuItem("Autostart", new EventHandler(this.AutostartMenuEventHandler));
            //mnuAutostart.Checked = Autostart.CheckStartupFolderShortcutsExists();

            //System.Windows.Forms.MenuItem mnuExit = new System.Windows.Forms.MenuItem("Close", new EventHandler(this.ExitMenuEventHandler));
            //mnuLabel = new System.Windows.Forms.MenuItem("");
            //mnuLabel.Enabled = false; // greyed-out style label

            //System.Windows.Forms.MenuItem[] menuitems = new System.Windows.Forms.MenuItem[]
            //{
            //    mnuLabel, new System.Windows.Forms.MenuItem("-"), mnuMonitorOff, mnuScreenSaver, mnuSleep, new System.Windows.Forms.MenuItem("-"), mnuCaffeine, new System.Windows.Forms.MenuItem("-"), mnuAutostart, new System.Windows.Forms.MenuItem("-"), mnuExit
            //};

            ContextMenuStrip contextmenu = new ContextMenuStrip();
            contextmenu.Items.Add(new ToolStripMenuItem("Ultra Performance", null, new EventHandler(HandleThermalSettingClick), "UltraPerformance"));
            contextmenu.Items.Add(new ToolStripMenuItem("Optimized", null, new EventHandler(HandleThermalSettingClick), "Optimized"));
            contextmenu.Items.Add(new ToolStripMenuItem("Quiet", null, new EventHandler(HandleThermalSettingClick), "Quiet"));
            contextmenu.Items.Add(new ToolStripMenuItem("Cool", null, new EventHandler(HandleThermalSettingClick), "Cool"));
            contextmenu.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem autostartMenuItem = new ToolStripMenuItem("Autostart", null, new EventHandler(AutostartMenuEventHandler), "Autostart");
            contextmenu.Items.Add(autostartMenuItem);
            contextmenu.Items.Add(new ToolStripMenuItem("Exit", null, new EventHandler(ExitMenuEventHandler), "Exit"));
            contextmenu.Opening += Contextmenu_Opening;

            autostartMenuItem.Checked = Autostart.CheckStartupFolderShortcutsExists();

            notifyicon.ContextMenuStrip = contextmenu;


            Stream iconstream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/DellFanManagementTrayAppWpf;component/app.ico")).Stream;
            notifyicon.Icon = new System.Drawing.Icon(iconstream, SystemInformation.SmallIconSize);
            iconstream.Close();

            

            notifyicon.Visible = true;
        }

        private static void Notifyicon_MouseUp(object sender, MouseEventArgs e)
        {
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke((NotifyIcon)sender, null);
        }

        private static void Contextmenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var context = TaskScheduler.FromCurrentSynchronizationContext();

            ThermalSettingUtil.GetThermalSetting().ContinueWith(antecedent => {
                Task.Factory.StartNew(
                  () => {
                      foreach (var item in notifyicon.ContextMenuStrip.Items)
                      {
                          if (item is ToolStripMenuItem)
                          {
                              ToolStripMenuItem casted = (ToolStripMenuItem)item;
                              casted.Checked = casted.Name == antecedent.Result;
                          }
                          else if (item is ToolStripSeparator)
                          {
                              break;
                          }
                      }
                  },
                  CancellationToken.None,
                  TaskCreationOptions.None,
                  context);
            });
        }

        private static void HandleThermalSettingClick(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            ThermalSetting parsed = (ThermalSetting)Enum.Parse(typeof(ThermalSetting), item.Name);
            ThermalSettingUtil.SetThermalSetting(parsed);
        }

        private static void AutostartMenuEventHandler(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                // remove autostart entry
                Autostart.DeleteStartupFolderShortcut();
            }
            else
            {
                // create autostart entry
                Autostart.CreateStartupFolderShortcut();
            }
            bool next = Autostart.CheckStartupFolderShortcutsExists();
            ((ToolStripMenuItem)sender).Checked = next;
        }
        private static void ExitMenuEventHandler(object sender, EventArgs e)
        {
            notifyicon.Visible = false;
            System.Environment.Exit(0);
        }
    }
}
