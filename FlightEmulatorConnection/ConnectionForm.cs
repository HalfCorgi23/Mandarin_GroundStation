using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace FlightEmulatorConnection
{
    public partial class ConnectionForm : Form
    {
        private MAVLink.MavlinkParse mavlink = new MAVLink.MavlinkParse();
        private VehicleModel _vehiclemodel;
        public ConnectionForm()
        {
            InitializeComponent();
        }
        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            SerialPorts.DataSource = SerialPort.GetPortNames();
            SerialPorts.Text = SerialPort.GetPortNames()[SerialPort.GetPortNames().GetLength(0)-1];
            
        }
        private void ConnectionButton_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                return;
            }
            serialPort1.PortName = SerialPorts.Text;
            serialPort1.BaudRate = int.Parse(BuadRateChoose.Text);
            serialPort1.Open();
            label3.Text = "串口已连接！";
            serialPort1.ReadTimeout = 2000;
            _vehiclemodel = new VehicleModel(serialPort1.BaseStream);
            try
            {
                _vehiclemodel.Connect();
            }
            catch
            {
                throw new Exception("Vehicle Connect Failed");
            }
        }
        private void DisarmButton_Click(object sender, EventArgs e)
        {
            try
            {
                _vehiclemodel.Disarm();
                Console.WriteLine(_vehiclemodel.HeartBeating.ToString());
            }
            catch
            {
                throw new Exception("Vehicle Connect Failed");
            }
            Task _task = Task.Run(() =>
            {
                _vehiclemodel.UpdateData();
                Console.WriteLine("Connection Time Out!");
            });
            float buffer=0;
            Task t2 = Task.Run(() =>
            {
                while (true)
                {
                    if (_vehiclemodel.Pitch != buffer)
                    {
                        Console.Write("Type:" + _vehiclemodel.Type);
                        Console.WriteLine(" P:" + _vehiclemodel.Pitch.ToString() + " Y:" + _vehiclemodel.Yaw.ToString() + " R:" + _vehiclemodel.Roll.ToString());
                        buffer = _vehiclemodel.Pitch;
                    }
                }
            });
        }
        private void SerialPorts_Click(object sender, EventArgs e)
        {
            SerialPorts.DataSource = SerialPort.GetPortNames();
            SerialPorts.Text = SerialPort.GetPortNames()[SerialPort.GetPortNames().GetLength(0) - 1];
        }
    }
}
