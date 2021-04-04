using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DellFanManagementTrayAppWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            TrayIcon.CreateNotifyIcon();
        }

        private void CanDoSomething(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DoSomething(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.Print("Hi");
        }
    }
}
