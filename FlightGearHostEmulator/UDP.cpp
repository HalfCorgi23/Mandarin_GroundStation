#ifndef _UDP_CONNECTION
#define _UDP_CONNECTION
#endif 
#include "UDP.h"

UDPClient::UDPClient()
{
}

int UDPClient::Initlization()
{
	printf("��������Web Server API����/n");
	if (WSAStartup(MAKEWORD(2, 2), &_wsdata) != 0)
	{
		printf("Web Server API����ʧ�ܣ�/n");
		return 1;
	} 
	_socket = socket(AF_INET, SOCK_DGRAM, 0);
	if (_socket == INVALID_SOCKET)
	{
		printf("Web Server API����ʧ�ܣ�������룺%d/n", WSAGetLastError());
		WSACleanup();
		return 1;
	}
	ZeroMemory(_buffer, BUF_SIZE);
	printf("Web Server API�Ѿ�����!/n");
	//strcpy(_buffer, "Web Server API�Ѿ�����!");
	return 0;
}

void UDPClient::SetServerAddress(char address[], u_short timeout = 5000)
{
	_serveraddress.sin_family = AF_INET;
	_serveraddress.sin_addr.S_un.S_addr = inet_addr(address);
	_serveraddress.sin_port = htons(timeout);
	_serveraddresslength = sizeof(_serveraddress);
	printf_s("��������");
	printf_s(address);
	printf_s("/n");
}

int UDPClient::SendData(char data[])
{
	strcpy(_buffer, data);
	if (sendto(_socket, _buffer, BUF_SIZE, 0, (sockaddr *)&_serveraddress, _serveraddresslength)==SOCKET_ERROR)
	{
		printf("����δ���ͣ�����ʧ�ܣ�����%d/n", WSAGetLastError());
		closesocket(_socket);
		WSACleanup();
		return 1;
	}
	return 0;
}

int UDPClient::RecieveData(char data[])
{
	_nreceivedata = recvfrom(_sockclient, _buffer, BUF_SIZE, 0, (sockaddr *)&_serveraddress, &_serveraddresslength);
	if (SOCKET_ERROR == _nreceivedata)
	{
		printf("");
		closesocket(_socket);
		WSACleanup();
		return 1;
	}
	return 0;
}

UDPClient::~UDPClient()
{
	closesocket(_socket);
	WSACleanup();
}

UDPServer::UDPServer()
{
}

UDPServer::~UDPServer()
{
}
