using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Threading;


namespace Mandarin_GroundStation
{
    public partial class VehicleControl : UserControl
    {
        private DispatcherTimer dtimer;
        public VehicleViewModel VehicleData;
        public VehicleControlViewModel VehicleDataControl
        {
            get
            {
                return DataContext as VehicleControlViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
        public Stream DataStream;
        public VehicleControl(Stream _stream,string dictionary)
        {
            InitializeComponent();
            VehicleData = new VehicleViewModel(_stream);
            VehicleDataControl = new VehicleControlViewModel(dictionary);
            dtimer = new DispatcherTimer();
            dtimer.Interval = TimeSpan.FromSeconds(0.01);
            dtimer.Tick += dtimer_Tick;
            VehicleData.UpdateDataStart();
            dtimer.Start();
        }
        private void ConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionButton.Content = VehicleData.Vehicle.Roll.ToString();
        }

        private void VehicleStatueControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private void dtimer_Tick(object sender, EventArgs e)
        {
            AirSpeedDataLabel.Content = VehicleData.Vehicle.AirSpeed;
            GroundSpeedDataLabel.Content = VehicleData.Vehicle.GroungSpeed;
            ModeDisplayLabel.Content = VehicleDataControl.LabelContents.ModesLabel[VehicleData.Vehicle.AutoPilot];
            TypeDisplayLabel.Content= VehicleDataControl.LabelContents.TypesLabel[VehicleData.Vehicle.Type];
            LongitudeDataLabel.Content = VehicleData.Vehicle.Longtitude;
            LatitudeDataLabel.Content = VehicleData.Vehicle.Latitude;
            AltitudeDataLabel.Content = VehicleData.Vehicle.Attitude;
            if(VehicleData.Vehicle.IsConnecting)
            {
                //正在连接
            }
            else if(!VehicleData.Vehicle.HeartBeating)
            {
                //连接超时
                ConnectionGrid.Background = Brushes.OrangeRed;
                ConnectionLabel.Content = VehicleDataControl.LabelContents.ConnectionLabel[1];
            }
            else if(!VehicleData.Vehicle.Connected)
            {
                //连接断开
                ConnectionGrid.Background = Brushes.Red;
                ConnectionLabel.Content = VehicleDataControl.LabelContents.ConnectionLabel[2];
            }
            else
            {
                //已连接
                ConnectionGrid.Background = null;
                ConnectionLabel.Content = VehicleDataControl.LabelContents.ConnectionLabel[3];
            }
        }

    }
    public class VehicleModel : INotifyPropertyChanged
    {
        public VehicleModel()
        { }

        public bool IsArmed
        {
            get
            {
                return _isarmed;

            }

            set
            {
                _isarmed = value;
                NotifyPropertyChanged();
            }
        }

        public float Yaw
        {
            get
            {
                return _yaw;
            }

            set
            {
                _yaw = value;
                NotifyPropertyChanged();
            }
        }

        public float Pitch
        {
            get
            {
                return _pitch;
            }

            set
            {
                _pitch = value;
                NotifyPropertyChanged();
            }
        }

        public float Roll
        {
            get
            {
                return _roll;
            }

            set
            {
                _roll = value;
                NotifyPropertyChanged();
            }
        }

        public uint BootTime
        {
            get
            {
                return _boottime;
            }

            set
            {
                _boottime = value;
                NotifyPropertyChanged();
            }
        }

        public int Attitude
        {
            get
            {
                return _attitude;
            }

            set
            {
                _attitude = value;
                NotifyPropertyChanged();
            }
        }

        public int Longtitude
        {
            get
            {
                return _longtitude;
            }

            set
            {
                _longtitude = value;
                NotifyPropertyChanged();
            }
        }

        public int Latitude
        {
            get
            {
                return _latitude;
            }

            set
            {
                _latitude = value;
                NotifyPropertyChanged();
            }
        }

        public float RollSpeed
        {
            get
            {
                return _rollspeed;
            }

            set
            {
                _rollspeed = value;
                NotifyPropertyChanged();
            }
        }

        public float YawSpeed
        {
            get
            {
                return _yawspeed;
            }

            set
            {
                _yawspeed = value;
                NotifyPropertyChanged();
            }
        }

        public float PitchSpeed
        {
            get
            {
                return _pitchspeed;
            }

            set
            {
                _pitchspeed = value;
                NotifyPropertyChanged();
            }
        }

        public ushort BatteryVoltage
        {
            get
            {
                return _batteryvoltage;
            }

            set
            {
                _batteryvoltage = value;
                NotifyPropertyChanged();
            }
        }

        public short BatteryCurrent
        {
            get
            {
                return _batterycurrent;
            }

            set
            {
                _batterycurrent = value;
                NotifyPropertyChanged();
            }
        }

        public byte BatteryRemaining
        {
            get
            {
                return _batteryremaining;
            }

            set
            {
                _batteryremaining = value;
                NotifyPropertyChanged();
            }
        }

        public bool HeartBeating
        {
            get
            {
                return _heartbeating;
            }

            set
            {
                _heartbeating = value;
                NotifyPropertyChanged();
            }
        }

        public byte Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
                NotifyPropertyChanged();
            }
        }

        public byte State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = value;
                NotifyPropertyChanged();
            }
        }

        public byte AutoPilot
        {
            get
            {
                return _autopilot;
            }

            set
            {
                _autopilot = value;
            }
        }

        public bool IsConnecting
        {
            get
            {
                return _isconnecting;
            }

            set
            {
                _isconnecting = value;
            }
        }

        public bool Connected
        {
            get
            {
                return _connected;
            }

            set
            {
                _connected = value;
            }
        }

        public float GroungSpeed
        {
            get
            {
                return _groungspeed;
            }

            set
            {
                _groungspeed = value;
            }
        }

        public float AirSpeed
        {
            get
            {
                return _airspeed;
            }

            set
            {
                _airspeed = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isarmed;
        private float _yaw;
        private float _pitch;
        private float _roll;
        private uint _boottime;
        private int _attitude;
        private int _longtitude;
        private int _latitude;
        private float _rollspeed;
        private float _yawspeed;
        private float _pitchspeed;
        private ushort _batteryvoltage;
        private short _batterycurrent;
        private byte _batteryremaining;
        private bool _heartbeating;
        private byte _type;
        private byte _state;
        private byte _autopilot;
        private float _groungspeed;
        private float _airspeed;


        private bool _isconnecting;
        private bool _connected;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class VehicleViewModel
    {
        public VehicleModel Vehicle { get; set; }
        private Stream _datastream;
        private MAVLink.MavlinkParse _mavlinkparse;
        public VehicleViewModel(Stream _stream)
        {
            Vehicle = new VehicleModel();
            _mavlinkparse = new MAVLink.MavlinkParse();
            _datastream = _stream;
            Vehicle.IsConnecting = true;
        }
        public bool ReadHeartBeat(int timeout = 2000)
        {
            DateTime deadline = DateTime.Now.AddMilliseconds(timeout);
            while (DateTime.Now < deadline)
            {
                try
                {
                    var packet = _mavlinkparse.ReadPacket(_datastream);
                    if (packet == null)
                    {
                        continue;
                    }
                    else if (packet.GetType().ToString() == "MAVLink+mavlink_heartbeat_t")
                    {
                        MAVLink.mavlink_heartbeat_t _heartbeatpack = (MAVLink.mavlink_heartbeat_t)packet;
                        Vehicle.HeartBeating = true;
                        Vehicle.Type = _heartbeatpack.type;
                        Vehicle.State = _heartbeatpack.system_status;
                        Vehicle.AutoPilot = _heartbeatpack.autopilot;
                        return true;
                    }
                }
                catch
                {
                    Vehicle.HeartBeating = false;
                    Vehicle.Connected = false;
                }  
            }
            Vehicle.HeartBeating = false;
            return false;
        }
        private int updatedata(int timeout = 5000)
        {
            if (ReadHeartBeat())
            {
                DateTime deadline = DateTime.Now.AddMilliseconds(timeout);
                while (DateTime.Now < deadline)
                {
                    try
                    {
                        var data = _mavlinkparse.ReadPacket(_datastream);
                        if (data == null)
                        {
                            continue;
                        }
                        switch (data.GetType().ToString())
                        {
                            case "MAVLink+mavlink_attitude_t":
                                MAVLink.mavlink_attitude_t pack0 = (MAVLink.mavlink_attitude_t)data;
                                Vehicle.Roll = pack0.roll;
                                Vehicle.RollSpeed = pack0.rollspeed;
                                Vehicle.Pitch = pack0.pitch;
                                Vehicle.PitchSpeed = pack0.pitchspeed;
                                Vehicle.Yaw = pack0.yaw;
                                Vehicle.YawSpeed = pack0.yawspeed;
                                Vehicle.BootTime = pack0.time_boot_ms;
                                break;

                            case "MAVLink+_global_position_int_t":
                                MAVLink.mavlink_global_position_int_t pack1 = (MAVLink.mavlink_global_position_int_t)data;
                                Vehicle.Attitude = pack1.alt;
                                Vehicle.Latitude = pack1.lat;
                                Vehicle.Longtitude = pack1.lon;
                                break;

                            case "MAVLink+mavlink_sys_status_t":
                                MAVLink.mavlink_sys_status_t pack2 = (MAVLink.mavlink_sys_status_t)data;
                                Vehicle.BatteryVoltage = pack2.voltage_battery;
                                Vehicle.BatteryCurrent = pack2.current_battery;
                                Vehicle.BatteryRemaining = pack2.battery_remaining;
                                break;

                            case "MAVLink+mavlink_vfr_hud_t":
                                MAVLink.mavlink_vfr_hud_t pack3 = (MAVLink.mavlink_vfr_hud_t)data;
                                Vehicle.AirSpeed = pack3.airspeed;
                                Vehicle.GroungSpeed = pack3.groundspeed;
                                break;

                            case "MAVLink+mavlink_heartbeat_t":
                                ReadHeartBeat();
                                Vehicle.HeartBeating = true;
                                deadline = DateTime.Now.AddMilliseconds(timeout);
                                break;

                            default:
                                break;
                        }
                    }
                    catch
                    {
                        Vehicle.Connected = false;
                    }
                }
            }
            return 0;
        }
        public void Disarm()
        {
            MAVLink.mavlink_command_long_t req = new MAVLink.mavlink_command_long_t();
            req.target_system = 1;
            req.target_component = 1;
            req.command = (ushort)MAVLink.MAV_CMD.COMPONENT_ARM_DISARM;
            req.param1 = Vehicle.IsArmed ? 0 : 1;
            Vehicle.IsArmed = !Vehicle.IsArmed;
            byte[] packet = _mavlinkparse.GenerateMAVLinkPacket(MAVLink.MAVLINK_MSG_ID.COMMAND_LONG, req);
            _datastream.Write(packet, 0, packet.Length);
        }
        public int UpdateDataStart()
        {
            Task _task = Task.Run(() =>
              {
                  if(ReadHeartBeat())
                  {
                      Vehicle.IsConnecting = false;
                      Vehicle.HeartBeating = true;                 
                      Vehicle.Connected = true;
                      updatedata();
                  }
                  else
                  {
                      Vehicle.IsConnecting = false;
                      Vehicle.HeartBeating = false;
                      Vehicle.Connected = false;
                  }     
              }
                );
            Vehicle.HeartBeating = false;
            Vehicle.Connected = false;
            return 0;
        }
        ~VehicleViewModel()
        {
            //_datastream.Close();
        }
    }
    public class VehicleControlModel
    {

        public string TypeLabel { get; set; }
        public string LocationLabel { get; set; }
        public string LatitudeLabel { get; set; }
        public string LongitudeLabel { get; set; }
        public string AltitudeLabel { get; set; }
        public string ModeLabel { get; set; }
        public string LoadWaypointLabel { get; set; }
        public string TakeoffLabel { get; set; }
        public string LandLabel { get; set; }
        public string ReversalLabel { get; set; }
        public string DisconnectLabel { get; set; }
        public string VoyageStartLabel { get; set; }
        public string GroundSpeedLabel { get; set; }
        public string AirSpeedLabel { get; set; }
        public string WaypointDistanceLabel { get; set; }
        public string[] ModesLabel { get; set; }
        public string[] TypesLabel { get; set; }
        public string[] ConnectionLabel { get; set; }

    }
    public class VehicleControlViewModel
    {
        public VehicleControlModel LabelContents { get; set; }
        public VehicleControlViewModel(string dictionary)
        {
            LabelContents = JsonConvert.DeserializeObject<VehicleControlModel>(dictionary);
        }
    }
}
