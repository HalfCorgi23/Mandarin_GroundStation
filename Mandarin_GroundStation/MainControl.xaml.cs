using System;
using System.IO;
using System.IO.Ports;
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
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;

namespace Mandarin_GroundStation
{
    public partial class MainControl : UserControl
    {
        public MainControl(string dictionarypath)
        {
            InitializeComponent();
            _json = new JSON_Unserial(dictionarypath);
            ViewModel = new MainControlViewModel(_json.Read("Dictionary", "MainControl"));
            _vehiclestatuecontrol = new VehicleStatue(_json.Read("Dictionary", "VehicleStatue")) {Margin=new Thickness(-1) };
            _vehiclestatuecontrol.ControlsDictionary = _json.Read("Dictionary", "VehicleControl");
            //_routemanagementcontrol = new RouteManagement(_json.Read("Dictionary", "RouteManagement"));
            //_locolsettingscontrol = new LocalSettingsControl(_json.Read("Dictionary", "LocalSettingsControl"));
            _helpcentercontrol = new HelpCenter(_json.Read("Dictionary", "HelpCenter"));

            DeviceStatueGrid.Children.Add(_vehiclestatuecontrol);
            //LocalSettingsGrid.Children.Add(_locolsettingscontrol);
            //RouteManagementGrid.Children.Add(_routemanagementcontrol);
            HelpCenterGrid.Children.Add(_helpcentercontrol);
            _vehiclestatuecontrol.ShowConnectionError += _vehiclestatuecontrol_ShowConnectionError;
        }
        public event Action ShowConnectionError;
        private void _vehiclestatuecontrol_ShowConnectionError()
        {
            ShowConnectionError();
        }

        public MainControlViewModel ViewModel
        {
            get
            {
                return DataContext as MainControlViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
        private void MenuItem_Selected(object sender, RoutedEventArgs e)
        {
            HamburgerMenu.HamburgerMenuItem menuitem = (HamburgerMenu.HamburgerMenuItem)sender;
            ((TabItem)this.FindName(menuitem.Name + "Tab")).IsSelected = true;
            MainMenu.IsOpen = false;
        }
        private void MainControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainMenu.IsOpen = false;
        }
        private static JSON_Unserial _json;
        private static VehicleStatue _vehiclestatuecontrol;
        //private static RouteManagement _routemanagementcontrol;
        //private static LocalSettingsControl _locolsettingscontrol;
        private static HelpCenter _helpcentercontrol;
    }
    public class MainControlModel
    {
        public string DeviceStatueLabel { get; set; }
        public string RouteManagementLabel { get; set; }
        public string LocalSettingLabel { get; set; }
        public string HelpCenterLabel { get; set; }
        public string[] Connection { get; set; }
        public string[] ConnectionErrorMessageLabel { get; set; }
    }
    public class MainControlViewModel
    {
        public MainControlViewModel(string dictionary)
        {
            ControlModel= JsonConvert.DeserializeObject<MainControlModel>(dictionary);
        }
        public MainControlModel ControlModel { get; set; }
    }

}
