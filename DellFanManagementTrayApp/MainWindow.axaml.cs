using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DellFanManagement.SmmIo;

namespace DellFanManagementTrayApp
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.Print(DellSmmIoLib.SetThermalSetting(ThermalSetting.Cool).ToString());

            // Change button text when button is clicked.
            var button = (Button)sender;
            button.Content = DellSmmIoLib.GetThermalSetting().ToString();

            
        }
    }
}
