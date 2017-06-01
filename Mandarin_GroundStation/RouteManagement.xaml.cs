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
using Newtonsoft.Json;
using System.ComponentModel;

namespace Mandarin_GroundStation
{
    /// <summary>
    /// RouteManagement.xaml 的交互逻辑
    /// </summary>
    public partial class RouteManagement : UserControl
    {
        public RouteManagement(string dictionary)
        {
            InitializeComponent();
            ViewModel = new RouteManagementViewModel(dictionary);
            MapBroswer.Address = "http://restapi.amap.com/v3/staticmap?location=116.481485,39.990464&zoom=10&size=1300*1300&markers=mid,,A:116.481485,39.990464&key=6553a740542c24197fdf5452ee75e2d0";
            DataGrid_Waypoints.ItemsSource = ViewModel.WayPoints;
            
        }
        private RouteManagementViewModel ViewModel
        {
            get
            {
                return DataContext as RouteManagementViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        private void DataGrid_Waypoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox_Latitude.Text = ViewModel.WayPoints[DataGrid_Waypoints.SelectedIndex].Latitude.ToString();
            TextBox_Longtitude.Text = ViewModel.WayPoints[DataGrid_Waypoints.SelectedIndex].Longtitude.ToString();
            TextBox_Altitude.Text = ViewModel.WayPoints[DataGrid_Waypoints.SelectedIndex].Altitude.ToString();
        }

        private void Button_OpenFile_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AddWaypoint()
        {
            WayPoint wp = new WayPoint((ViewModel.WayPoints.Count+1), float.Parse(TextBox_Latitude.Text), float.Parse(TextBox_Longtitude.Text), float.Parse(TextBox_Altitude.Text));
            ViewModel.WayPoints.Add(wp);
            //DataGrid_Waypoints.Items.Add(new Dictionary<string, string>
            //{
            //    { ViewModel.Model.Longtitude_Label, wp.Longtitude.ToString() },
            //    { ViewModel.Model.Latitude_Label, wp.Latitude.ToString() },
            //    { ViewModel.Model.Altitude_Label, wp.Altitude.ToString() },
            //    { ViewModel.Model.WayPointNumber_Label, DataGrid_Waypoints.Items.Count.ToString()}
            //});

        }


        private void Button_AddWaypoiint_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_Waypoints.Columns[0].Header = ViewModel.Model.WayPointNumber_Label;
            DataGrid_Waypoints.Columns[1].Header = ViewModel.Model.Latitude_Label;
            DataGrid_Waypoints.Columns[2].Header = ViewModel.Model.Longtitude_Label;
            DataGrid_Waypoints.Columns[3].Header = ViewModel.Model.Altitude_Label;
            AddWaypoint();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //DataGrid_Waypoints.Columns[0].Header = ViewModel.Model.WayPointNumber_Label;
            //DataGrid_Waypoints.Columns[1].Header = ViewModel.Model.Latitude_Label;
            //DataGrid_Waypoints.Columns[2].Header = ViewModel.Model.Longtitude_Label;
            //DataGrid_Waypoints.Columns[3].Header = ViewModel.Model.Altitude_Label;
        }

        private void Button_DeleteWaypoint_Click(object sender, RoutedEventArgs e)
        {
            if(DataGrid_Waypoints.SelectedIndex!=-1)
            {
                ViewModel.WayPoints.RemoveAt(DataGrid_Waypoints.SelectedIndex);
            }
        }
    }
    public class WayPoint //路径点
    {
        public WayPoint(int no, float lat, float lon, float alt)
        {
            number = no;
            latitude = lat;
            longtitude = lon;
            altitude = alt;
        }
        private float latitude;
        private float longtitude;
        private float altitude;
        private int number;

        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }

        public float Latitude //经度
        {
            get
            {
                return latitude;
            }

            set
            {
                latitude = value;
            }
        }

        public float Longtitude //纬度
        {
            get
            {
                return longtitude;
            }

            set
            {
                longtitude = value;
            }
        }

        public float Altitude //海拔
        {
            get
            {
                return altitude;
            }

            set
            {
                altitude = value;
            }
        }

        
    }

    public class RouteManagementModel
    {
        public RouteManagementModel()
        {
            
        }
        public string OpenFile_Label{ get; set; }
        public string NewFile_Label { get; set; }
        public string SaveFile_Label { get; set; }
        public string SaveAsFile_Label { get; set; }
        public string Takeoff_Label { get; set; }
        public string Land_Label { get; set; }
        public string NotDefined_Label { get; set; }
        public string AddWaypoint_Label { get; set; }
        public string Latitude_Label { get; set; }
        public string Longtitude_Label { get; set; }
        public string Altitude_Label { get; set; }
        public string WayPointNumber_Label { get; set; }
        public string AddWaypointType_Label { get; set; }
        public string DeleteWaypoint_Label { get; set; }
    }
    public class RouteManagementViewModel
    {
        public RouteManagementModel Model { get; set; }
        //public List<WayPoint> WayPoints { get; set; }
        public BindingList<WayPoint> WayPoints { get; set; }
        public RouteManagementViewModel(string dictionary)
        {
            Model = JsonConvert.DeserializeObject<RouteManagementModel>(dictionary);
            WayPoints = new BindingList<WayPoint>();
        }
    }
}
