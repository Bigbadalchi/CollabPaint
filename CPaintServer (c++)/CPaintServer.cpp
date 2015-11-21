
#include "CPaintServer.h"

CPaintServer::CPaintServer(const char* ip, USHORT port, unsigned char key[128 / 8])
	: listenSock(key)
{
	SOCKADDR_IN addr;

	addr.sin_addr.S_un.S_addr = inet_addr(ip);
	addr.sin_port = htons(port);
	addr.sin_family = AF_INET;

	listenSock.Bind((SOCKADDR*)&addr, sizeof(SOCKADDR_IN));
	listenSock.Listen(SOMAXCONN);
	listenThread = std::thread(std::bind(&CPaintServer::ListenFunction, this));
}

void CPaintServer::ListenFunction()
{
	SOCKADDR_IN remote_addr;
	int size = sizeof(SOCKADDR_IN); 
	char ValidationBuffer[2048];
	while (AES_Socket newConn = listenSock.Accept((SOCKADDR*)&remote_addr, &size, 0)) {

		//Validate the client and create an outbound connection with the client


		newConn.setTimeOut(2000);
		int rec = newConn.Receive(ValidationBuffer, 2048, 0);

		SOCKADDR_IN inboundADDR;
		bool parse_success = true;
		char reply[2];
		memset(reply, 0, sizeof(reply));

		if (rec <= 0) {
			//Assume the client is malicious and discard it!
			newConn.Close();
			continue;
		}
		else {
			//We've received the info to create an outbound connection
			newConn.setTimeOut(-1); //remove timeout

			//parsing format: ipv4_addr|port
			std::string received_data(ValidationBuffer);
			
			size_t position = received_data.find('|'); 
			if (position != std::string::npos) {
				std::string ip = received_data.substr(0, position);
				inboundADDR.sin_addr.S_un.S_addr = inet_addr(ip.c_str());
			}
			else {
				inboundADDR.sin_addr.S_un.S_addr = -1;
			}

			if (inboundADDR.sin_addr.S_un.S_addr == -1) {
				//Failed to parse
				reply[0] = COULD_NOT_PARSE;
				newConn.Send(reply, 2, 0);
				newConn.Close();
				continue;
			}

			size_t len = received_data.length();
			if (len > position + 1) {
				std::string port = received_data.substr(position + 1, len - position - 1);
				inboundADDR.sin_port = htons((USHORT)atoi(port.c_str()));
			}
			else {
				inboundADDR.sin_port = htons(0);
			}

			if (ntohs(inboundADDR.sin_port) == 0) {
				//Failed to parse
				reply[0] = COULD_NOT_PARSE;
				newConn.Send(reply, 2, 0);
				newConn.Close();
				continue;
			}

		}

		//use this connection for sending outbound packets and use the inboundADDR to establish an inbound connection
		std::lock_guard<std::mutex> _lg(mu);
		//We need to register the client
		
		clients.push_back(Client(newConn.HardDeReference(), this, inboundADDR));

	}
}

void CPaintServer::handleInboundRequests(SOCKADDR_IN remoteADDR)
{
	//This method establishes and handles an inbound connection from a client
	


}

Client::Client(AES_Socket* outbound_sock, const CPaintServer* parent, SOCKADDR_IN remoteADDR)
{
	this->remoteADDR = remoteADDR;
	run_thread = std::thread(std::bind(&CPaintServer::handleInboundRequests, parent, remoteADDR));

}

Client::~Client()
{
	run_thread.join();
	delete SOCK;
}
