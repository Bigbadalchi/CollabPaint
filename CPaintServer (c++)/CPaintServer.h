
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

#include "CommunicationFlags.h"

#define CLIENT_CMD_SUBSCRIBE 1

struct Client {
	AES_Socket* SOCK;
	std::thread run_thread;
	Client(AES_Socket* outbound_sock, char KEY[128 / 8]);
	bool _finalized;
	~Client();
};

class CPaintServer {
	AES_Socket listenSock;
	std::thread listenThread;
	std::mutex mu;

	std::vector<Client> clients;

	void ListenFunction();
	void handleInboundRequests(SOCKADDR_IN remoteADDR);

public:
	CPaintServer(const char* ip, USHORT port,
		unsigned char key[128 / 8]);
};

#endif