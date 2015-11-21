
#include "CPaintServer.h"

CPaintServer::CPaintServer(const char* ip, USHORT port, const char* passwordHash /*= NULL*/)
	: listenSock((unsigned char*)KEY)
{
	SOCKADDR_IN addr;
	addr.sin_addr.S_un.S_addr = inet_addr(ip);
	addr.sin_port = htons(port);
	addr.sin_family = AF_INET;

	listenSock.Bind((SOCKADDR*)&addr, sizeof(SOCKADDR_IN));
	listenSock.Listen(SOMAXCONN);
	listenThread = std::thread(std::bind(&CPaintServer::ListenCallback, this));
}

void CPaintServer::ListenCallback()
{
	SOCKADDR_IN remote_addr;
	int size = sizeof(SOCKADDR_IN); 
	char ValidationBuffer[2048];
	while (AES_Socket newConn = listenSock.Accept((SOCKADDR*)&remote_addr, &size, 0)) {
		
		//Validate the client and create an outbound connection with the client
		
		newConn.setTimeOut(2000);
		int rec = newConn.Receive(ValidationBuffer, 2048, 0);
		
		//FIX THIS SHIT
		//std::thread(std::bind(&CPaintServer::handleInboundRequests, this, newConn, remote_addr)).detach();

		std::lock_guard<std::mutex> _lg(mu);
		//We need to register an outbound connection

	}
}

void CPaintServer::handleInboundRequests(AES_Socket sock, SOCKADDR_IN remoteADDR)
{
	//This method handles an established inbound connection from a client

}
