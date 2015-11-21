

#pragma comment(lib, "Ws2_32.lib")

#include "AES.h"

#define WIN32_LEAN_AND_MEAN
#define _WINSOCK2API_
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <WinSock2.h>
#include <Windows.h>
#include <thread>

#ifndef SD_BOTH
#define SD_BOTH 0x02
#endif

#define BREAK_ITERATION 50

class AES_Socket {
private:
	
	UCHAR header[4];
	unsigned char* cryptedbuffer = nullptr;
	size_t buffersize = 0u;
	
	static size_t intPow(size_t input, size_t exp) {
		size_t val = 1;
		for (size_t t = 0; t < exp; ++t)
			val *= input;
		return val;
	}

	bool connected = false;
	SOCKET sock;
	AES_KEY enc_key, dec_key;
	unsigned char IV[AES_BLOCK_SIZE + 1];
	unsigned char remoteIV[AES_BLOCK_SIZE + 1];
	unsigned char KEY[128 / 8];
	AES_Socket();
	AES_Socket(unsigned char key[128 / 8], SOCKET newConn);
	AES_Socket(const AES_Socket& that);

public:

	bool Connected() const { return connected; }

	AES_Socket(AES_Socket&& rthat);

	int setTimeOut(DWORD milliseconds);

	int Bind(SOCKADDR* addr, int addr_struct_size);
	int Listen(int backlog);
	AES_Socket Accept(SOCKADDR* remote_addr, int* addr_struct_size, int receiveFlags = 0);

	AES_Socket(unsigned char key[128/8]);
	bool Connect(SOCKADDR* addr, int addr_struct_size);
	int Receive(char* buffer, size_t size, int flags = 0);
	int Send(const char* data, size_t size, int flags = 0);
	int setSocketOption(int level, int option_name, const char* option_val, int option_length);
	
	int Shutdown(int param);
	int Close();

	operator bool() const {
		return sock != NULL;
	}

	~AES_Socket();
};