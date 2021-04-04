using JKang.IpcServiceFramework.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using ServiceContract;

namespace DellFanManagementTrayAppWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static RoutedCommand MyCommand = new RoutedCommand("MyCommand", typeof(ContextMenu));

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Window w)) return;

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

            
        private async Task<String> ContextMenu_OpenedAsync()
        {
            // register IPC clients
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddNamedPipeIpcClient<IThermalSettingService>("client1", pipeName: "pipeinternal")
                .BuildServiceProvider();

            // resolve IPC client factory
            IIpcClientFactory<IThermalSettingService> clientFactory = serviceProvider
                .GetRequiredService<IIpcClientFactory<IThermalSettingService>>();

            // create client
            IIpcClient<IThermalSettingService> client = clientFactory.CreateClient("client1");

            DellFanManagement.SmmIo.ThermalSetting thermalSetting = await client.InvokeAsync(x => x.GetThermalSetting());

            return thermalSetting.ToString();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu_OpenedAsync();

        }

        private void menuItems_Opened(object sender, RoutedEventArgs e)
        {

                //Task<string> task = Task.Run(async () => await ContextMenu_OpenedAsync());
                //task.Wait();
                //string termalSettingId = task.Result;

                //foreach (var item in menuItems.Items)
                //{
                //    if (item is MenuItem)
                //    {
                //        ((MenuItem)item).IsChecked = (item is MenuItem && (string)((MenuItem)item).CommandParameter == termalSettingId);
                //    }                
                //}
        }

        private void myNotifyIcon_MouseEnter(object sender, MouseEventArgs e)
        {
     
        }

        private void myNotifyIcon_TrayMouseMove(object sender, RoutedEventArgs e)
        {
        }

        private void myNotifyIcon_PreviewTrayToolTipOpen(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
