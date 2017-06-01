#pragma once
#include <stdio.h>  
#include <WINSOCK2.H>  

#pragma comment(lib,"WS2_32.lib")  
#define BUF_SIZE 64 

class UDPClient
{
public:
	UDPClient();
	int Initlization();
	void SetServerAddress(char address[], u_short timeout = 5000);
	int SendData(char data[]);
	int RecieveData(char data[]);
	~UDPClient();

private:
	WSADATA _wsdata;
	SOCKET _socket;
	char _buffer[BUF_SIZE];
	SOCKADDR_IN _serveraddress;
	SOCKET _sockclient = socket(AF_INET, SOCK_DGRAM, 0);
	int _nreceivedata;
	int _serveraddresslength;
};
class UDPServer
{
public:
	UDPServer();
	int Initlization();
	void SetClientAddress(char address[], u_short timeout = 5000);
	int SendData(char data[]);
	int RecieveData(char data[]);
	~UDPServer();

private:
	WSADATA _wsdata;
	SOCKET _socket;
	char _buffer[BUF_SIZE];
	SOCKADDR_IN _serveraddress;
	SOCKADDR_IN _clientaddress;
	SOCKET _sockclient = socket(AF_INET, SOCK_DGRAM, 0);
	int _nreceivedata;
	int _serveraddresslength;
};


