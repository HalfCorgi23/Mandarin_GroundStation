using System;
using System.IO;

namespace FlightEmulatorConnection
{
    public class VehicleModel
    {
        public VehicleModel(Stream stream)
        {
            _mavlink_parse = new MAVLink.MavlinkParse();
            _datastream = stream;
        }

        public bool IsArmed
        {
            get
            {
                return _armed;
            }

            set
            {
                _armed = value;
            }
        }

        public float Yaw
        {
            get
            {
                return _attitude_t.yaw;
            }

            set
            {
                _attitude_t.yaw = value;
            }
        }

        public float Pitch
        {
            get
            {
                return _attitude_t.pitch;
            }

            set
            {
                _attitude_t.pitch = value;
            }
        }

        public float Roll
        {
            get
            {
                return _attitude_t.roll;
            }

            set
            {
                _attitude_t.roll = value;
            }
        }

        public uint BootTime
        {
            get
            {
                return _attitude_t.time_boot_ms;
            }

            set
            {
                _attitude_t.time_boot_ms = value;
            }
        }

        public int Attitude
        {
            get
            {
                return _global_position_int_t.alt;
            }

            set
            {
                _global_position_int_t.alt = value;
            }
        }

        public int Longitude
        {
            get
            {
                return _global_position_int_t.lon;
            }

            set
            {
                _global_position_int_t.lon = value;
            }
        }

        public int Latitude
        {
            get
            {
                return _global_position_int_t.lat;
            }

            set
            {
                _global_position_int_t.lat = value;
            }
        }

        public float RollSpeed
        {
            get
            {
                return _attitude_t.rollspeed;
            }

            set
            {
                _attitude_t.rollspeed = value;
            }
        }

        public float PitchSpeed
        {
            get
            {
                return _attitude_t.pitchspeed;
            }

            set
            {
                _attitude_t.pitchspeed = value;
            }
        }

        public float YawSpeed
        {
            get
            {
                return _attitude_t.yawspeed;
            }

            set
            {
                _attitude_t.yawspeed = value;
            }
        }

        public ushort BatteryVoltage
        {
            get
            {
                return _sys_status_t.voltage_battery;
            }

            set
            {
                _sys_status_t.voltage_battery = value;
            }
        }

        public short BatteryCurrent
        {
            get
            {
                return _sys_status_t.current_battery;
            }

            set
            {
                _sys_status_t.current_battery = value;
            }
        }

        public byte BatteryRemaining
        {
            get
            {
                return _sys_status_t.battery_remaining;
            }

            set
            {
                _sys_status_t.battery_remaining = value;
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
            }
        }

        public byte Type
        {
            get
            {
                return _heartbeat_t.type;
            }

            set
            {
                _heartbeat_t.type = value;
            }
        }

        public byte State
        {
            get
            {
                return _heartbeat_t.system_status;
            }

            set
            {
                _heartbeat_t.system_status = value;
            }
        }

        public bool Connect(int timeout = 2000)
        {
            return true;
        }

        public void Disarm()
        {
            MAVLink.mavlink_command_long_t req = new MAVLink.mavlink_command_long_t();
            req.target_system = 1;
            req.target_component = 1;
            req.command = (ushort)MAVLink.MAV_CMD.COMPONENT_ARM_DISARM;
            req.param1 = _armed ? 0 : 1;
            _armed = !_armed;
            byte[] packet = _mavlink_parse.GenerateMAVLinkPacket(MAVLink.MAVLINK_MSG_ID.COMMAND_LONG, req);
        }

        public T ReadSingleData<T>(int timeout = 2000)
        {
            DateTime deadline = DateTime.Now.AddMilliseconds(timeout);
            while (DateTime.Now < deadline)
            {
                var packet = _mavlink_parse.ReadPacket(_datastream);

                if (packet == null)
                    continue;
                if (packet.GetType() == typeof(T))
                {
                    return (T)packet;
                }
            }
            throw new Exception("No packet match found");
        }

        public bool ReadHeartbeat(int timeout = 5000)
        {
            DateTime deadline = DateTime.Now.AddMilliseconds(timeout);
            while (DateTime.Now < deadline)
            {
                var packet = _mavlink_parse.ReadPacket(_datastream);
                if (packet == null)
                {
                    continue;
                }
                else if(packet.GetType().ToString()== "MAVLink+mavlink_heartbeat_t")
                {
                    _heartbeat_t = (MAVLink.mavlink_heartbeat_t)packet;
                    _heartbeating = true;
                    return true;
                }
            }
            _heartbeating = false;
            return false;
        }

        public int UpdateData(int timeout = 5000)
        {
            if (ReadHeartbeat())
            {
                DateTime deadline = DateTime.Now.AddMilliseconds(timeout);
                while (DateTime.Now < deadline)
                {
                    var data = _mavlink_parse.ReadPacket(_datastream);
                    if (data == null)
                    {
                        continue;
                    }
                    switch (data.GetType().ToString())
                    {
                        case "MAVLink+mavlink_attitude_t":
                            _attitude_t = (MAVLink.mavlink_attitude_t)data; break;
                        case "MAVLink+mavlink_sys_status_t":
                            _sys_status_t = (MAVLink.mavlink_sys_status_t)data; break;
                        case "MAVLink+_global_position_int_t":
                            _global_position_int_t = (MAVLink.mavlink_global_position_int_t)data; break;
                        case "MAVLink+mavlink_servo_output_raw_t":
                            _servo_output_raw_t = (MAVLink.mavlink_servo_output_raw_t)data; break;
                        case "MAVLink+mavlink_rc_channels_raw_t":
                            _rc_channels_raw_t = (MAVLink.mavlink_rc_channels_raw_t)data; break;
                        case "MAVLink+mavlink_vfr_hud_t":
                            _vfr_hud_t = (MAVLink.mavlink_vfr_hud_t)data; break;
                        case "MAVLink+mavlink_system_time_t":
                            _system_time_t = (MAVLink.mavlink_system_time_t)data; break;
                        case "MAVLink+mavlink_heartbeat_t":
                            _heartbeat_t = (MAVLink.mavlink_heartbeat_t)data;
                            deadline = DateTime.Now.AddMilliseconds(timeout);
                            break;
                        default:
                            break;
                    }
                }
            }
            return 0;//连接超时
        }

        public enum MAV_TYPE
        {
            GENERIC = 0,
            FIXED_WING = 1,
            QUADROTOR = 2,
            COAXIAL = 3,
            HELICOPTER = 4,
            ANTENNA_TRACKER = 5,
            GROUND_CONTROL_STATION = 6,
            AIRSHIP = 7,
            FREE_BALLOON = 8,
            ROCKET = 9,
            GROUND_ROVER = 10,
            SURFACE_BOAT = 11,
            SUBMARINE = 12,
            HEXAROTOR = 13,
            OCTOROTOR = 14,
            TRICOPTER = 15,
            FLAPPING_WING = 16,
            KITE = 17,
            ONBOARD_CONTROLLER = 18,
            VTOL_DUOROTOR = 19,
            VTOL_QUADROTOR = 20,
            ENUM_END = 21,
        }
        public enum MAV_STATE
        {
            UNINITLIZED = 0,
            BOOTING = 1,
            CALIBRATING = 2,
            STANDBY = 3,
            ACTIVED = 4,
            CRITICAL = 5,
            EMERGENCY = 6,
            POWEROFF = 7,
            ENUM_END = 8,
        };
        public enum MAV_MODE_FLAG
        {
            CUSTOM_MODE_ENABLED = 1,
            TEST_ENABLED = 2,
            AUTO_ENABLED = 4,
            GUIDED_ENABLED = 8,
            STABILIZE_ENABLED = 16,
            HIL_ENABLED = 32,
            MANUAL_INPUT_ENABLED = 64,
            SAFETY_ARMED = 128,
            ENUM_END = 129,
        };

        private MAVLink.MavlinkParse _mavlink_parse;
        private Stream _datastream;

        private bool _armed;
        private bool _heartbeating;

        private MAVLink.mavlink_servo_output_raw_t _servo_output_raw_t;
        private MAVLink.mavlink_rc_channels_raw_t _rc_channels_raw_t;
        private MAVLink.mavlink_vfr_hud_t _vfr_hud_t;
        private MAVLink.mavlink_system_time_t _system_time_t;
        private MAVLink.mavlink_attitude_t _attitude_t;
        private MAVLink.mavlink_heartbeat_t _heartbeat_t;
        private MAVLink.mavlink_raw_imu_t _raw_imu_t;
        private MAVLink.mavlink_scaled_pressure_t _scaled_pressure_t;
        private MAVLink.mavlink_sys_status_t _sys_status_t;
        private MAVLink.mavlink_meminfo_t _meminfo_t;
        private MAVLink.mavlink_mission_current_t _mission_current_t;
        private MAVLink.mavlink_gps_raw_int_t _gps_raw_int_t;
        private MAVLink.mavlink_nav_controller_output_t _nav_controller_output_t;
        private MAVLink.mavlink_global_position_int_t _global_position_int_t;
        private MAVLink.mavlink_ahrs_t _ahrs_t;
        private MAVLink.mavlink_hwstatus_t _hwstatus_t;
        private MAVLink.mavlink_command_ack_t _command_ack_t;

    }
}
