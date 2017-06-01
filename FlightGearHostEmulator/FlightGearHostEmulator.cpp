// FlightGearHostEmulator.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "MAVlink\common\mavlink.h"
#include "stdafx.h"

#include <WINSOCK2.H>
#include <urlmon.h>

#pragma comment(lib, "ws2_32.lib")
#pragma comment(lib, "urlmon.lib")

#define MAX_SIZE 1024

int GetLocalIP()
{
	WSADATA wsaData;
	int err = WSAStartup(MAKEWORD(2, 0), &wsaData);
	if (err != 0)
	{
		return err;
	}

	char szHostName[MAX_PATH] = { 0 };
	int nRetCode;
	nRetCode = gethostname(szHostName, sizeof(szHostName));

	char* lpLocalIP;
	PHOSTENT hostinfo;

	if (nRetCode != 0)
	{
		return WSAGetLastError();
	}

	hostinfo = gethostbyname(szHostName);
	lpLocalIP = inet_ntoa(*(struct in_addr*)*hostinfo->h_addr_list);

	if (szHostName != NULL)
	{
		printf("主机名: %s\n", szHostName);
		printf("本地IP: %s\n", lpLocalIP);
	}

	WSACleanup();
	return 0;
}

void SendData()
{
	mavlink_system_t mavlink_system;

	mavlink_system.sysid = 20;                   ///< ID 20 for this airplane
	mavlink_system.compid = MAV_COMP_ID_IMU;     ///< The component sending the message is the IMU, it could be also a Linux process

												 // Define the system type, in this case an airplane
	uint8_t system_type = MAV_TYPE_FIXED_WING;
	uint8_t autopilot_type = MAV_AUTOPILOT_GENERIC;

	uint8_t system_mode = MAV_MODE_PREFLIGHT; ///< Booting up
	uint32_t custom_mode = 0;                 ///< Custom mode, can be defined by user/adopter
	uint8_t system_state = MAV_STATE_STANDBY; ///< System ready for flight

											  // Initialize the required buffers
	mavlink_message_t msg;
	uint8_t buf[MAVLINK_MAX_PACKET_LEN];

	// Pack the message
	mavlink_msg_heartbeat_pack(mavlink_system.sysid, mavlink_system.compid, &msg, system_type, autopilot_type, system_mode, custom_mode, system_state);

	// Copy the message to the send buffer
	uint16_t len = mavlink_msg_to_send_buffer(buf, &msg);

	// Send the message with the standard UART send function
	// uart0_send might be named differently depending on
	// the individual microcontroller / library in use.

}

int main()
{
	SendData();
	return 0;
	
}



