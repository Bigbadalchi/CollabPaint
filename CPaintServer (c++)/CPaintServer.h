
#ifndef CPAINT_SERVER_H
#define CPAINT_SERVER_H

#define WIN32_LEAN_AND_MEAN

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <Windows.h>
#include <WinSock2.h>

#include <vector>
#include <unordered_map>
#include <thread>
#include <mutex>

#include <stdio.h>

#include <AES_TCP/AES_Socket.h>

#define CLIENT_CMD_SUBSCRIBE 1

#define KEY "\xFA\xAF\x23\xC0\t+@A_E4g~F`"

class CPaintServer {
	AES_Socket listenSock;
	const char* passwordHash;
	std::thread listenThread;
	std::mutex mu;
	std::vector<std::pair<AES_Socket*, SOCKADDR_IN>> connections;

	void ListenCallback();
	void handleInboundRequests(AES_Socket sock, SOCKADDR_IN remoteADDR);

public:
	CPaintServer(const char* ip, USHORT port,
		const char* passwordHash = NULL);
};

#endif