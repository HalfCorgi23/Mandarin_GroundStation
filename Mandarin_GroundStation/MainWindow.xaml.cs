using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using CefSharp.Wpf;
using CefSharp.Wpf.Internals;


namespace Mandarin_GroundStation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Maincontrol_ShowConnectionError()
        {
            await this.ShowMessageAsync(maincontrol.ViewModel.ControlModel.ConnectionErrorMessageLabel[0],
                maincontrol.ViewModel.ControlModel.ConnectionErrorMessageLabel[1]);
        }

        private static MainControl maincontrol;
        private async void MandarinMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var controller = await this.ShowProgressAsync("Please Wait", "Loading Local Settings...");
            controller.SetProgress(0.2);
            maincontrol = new MainControl("Dictionary\\cn_dictionary.json");
            controller.SetProgress(0.5);
            MainControlGrid.Children.Add(maincontrol);
            controller.SetProgress(0.9);
            await Task.Delay(1000);
            controller.SetProgress(1);
            await controller.CloseAsync();
            maincontrol.ShowConnectionError += Maincontrol_ShowConnectionError;
        }
    }

}

