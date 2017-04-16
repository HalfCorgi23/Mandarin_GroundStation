using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Mandarin_GroundStation
{
    public partial class VehicleStatue : UserControl
    {
        public VehicleStatueViewModel ViewModel
        {
            get
            {
                return DataContext as VehicleStatueViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
        public VehicleStatue(string dictionary)
        {
            InitializeComponent();
            ViewModel = new VehicleStatueViewModel(dictionary);
            MapBrowser.Address = "http://restapi.amap.com/v3/staticmap?location=116.481485,39.990464&zoom=10&size=850*1000&markers=mid,,A:116.481485,39.990464&key=6553a740542c24197fdf5452ee75e2d0";
        }
        private List<VehicleControl> Vehicles;
        public string ControlsDictionary { get; set; }
        public event Action ShowConnectionError;
        private void DeviceStatueControl_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionMethodComboBox.Items.Clear();
            for (int i = 0; i < SerialPort.GetPortNames().Length; i++)
            {
                ConnectionMethodComboBox.Items.Add(SerialPort.GetPortNames()[i]);
            }
            ConnectionMethodComboBox.Items.Add("UDP");
            ConnectionMethodComboBox.Text = ConnectionMethodComboBox.Items[0].ToString();
            BaultRateComboBox.Text = "115200";
            Vehicles = new List<VehicleControl>();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionMethodComboBox.Text == null) { }
            else if (ConnectionMethodComboBox.Text == "UDP") { }
            else
            {
                SerialPort serialport;
                serialport = new SerialPort(ConnectionMethodComboBox.Text);
                try
                {
                    serialport.BaudRate = int.Parse(BaultRateComboBox.Text);
                    serialport.ReadTimeout = 5000;
                    serialport.WriteTimeout = 5000;
                    serialport.Open();
                    VehicleControl _vctr = new VehicleControl(serialport.BaseStream, ControlsDictionary)
                    {
                        Height = 150,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(5, (VehicleControlsView.Children.Count * 155), 5, 5)
                    };
                    Vehicles.Add(_vctr);
                    VehicleControlsView.Children.Add(Vehicles[Vehicles.Count - 1]);
                }
                catch
                {
                    ShowConnectionError();
                    //throw new Exception("Parameter Value Fault");
                }
            } 
        }

    }
    public class VehicleStatueModel
    {
        public string CreateConnectionLabel { get; set; }
        public string AllVehicleOprationLabel { get; set; }
        public string ConnectionMethodLabel { get; set; }
        public string BaudRateLabel { get; set; }
        public string IPAddressLabel { get; set; }
        public string LoadWaypointLabel { get; set; }
        public string VoyageStartLabel { get; set; }
        public string TakeoffLabel { get; set; }
        public string LandLabel { get; set; }
        public string ReversalLabel { get; set; }
        public string CreateConnectionTipLabel { get; set; }
        public string[] ConnectionErrorMessageLabel { get; set; }
    }
    public class VehicleStatueViewModel
    {
        public VehicleStatueModel Model { get; set; }
        public VehicleStatueViewModel(string dictionary)
        {
            Model = JsonConvert.DeserializeObject<VehicleStatueModel>(dictionary);
        }
    }
}
