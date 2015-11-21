
#include "AES_Socket.h"

int AES_Socket::Bind(SOCKADDR* addr, int addr_struct_size)
{
	return bind(sock, addr, addr_struct_size);
}

int AES_Socket::Listen(int backlog)
{
	return listen(sock, backlog);
}

AES_Socket AES_Socket::Accept(SOCKADDR* remote_addr, int* addr_struct_size, int receiveFlags /* = 0 */)
{
	SOCKET conn = accept(sock, remote_addr, addr_struct_size);
	if (conn != INVALID_SOCKET) {
		AES_Socket ASock(KEY, conn);

		int rc = recv(conn, (char*)ASock.remoteIV, AES_BLOCK_SIZE, 0); //receive the other IV
		if (rc <= 0) {
			fprintf_s(stderr, "Error: %i\n", WSAGetLastError());
			closesocket(conn);
			conn = NULL;
			ASock.sock = NULL;
			return ASock;
		}
		else if (rc < AES_BLOCK_SIZE) {
			//If we didn't get all 16 bytes loop until we get em all
			int breakct = 0;
			u_long bytes = (u_long)rc;
			do {
				std::this_thread::sleep_for(std::chrono::microseconds(30));
				ioctlsocket(conn, FIONREAD, &bytes);

				if (bytes == 0 && ++breakct == 50) {
					//Couldn't get IV or connected socket is not valid (Not using the same communication scheme)"
					closesocket(conn);
					conn = NULL;
					ASock.sock = NULL;
					return ASock;
				}

				int bytes_to_receive = (int)std::fminf((float)(AES_BLOCK_SIZE - rc), (float)bytes);
				rc += recv(conn, ((char*)ASock.remoteIV) + rc, bytes_to_receive, 0);
			} while (rc != AES_BLOCK_SIZE);
		}

		fprintf_s(stdout, "Received initialization vector: %s\n", ASock.remoteIV);

		//Send this one's initialization vector
		send(conn, (const char*)ASock.IV, AES_BLOCK_SIZE, 0);
		fprintf_s(stdout, "Sending initialization vector: %s\n", ASock.IV);

		return ASock;

	}
	else {
		return AES_Socket();
	}
}

AES_Socket::AES_Socket(unsigned char key[128 / 8])
{
	AES_set_encrypt_key(key, 128, &enc_key);
	AES_set_decrypt_key(key, 128, &dec_key);

	memcpy_s(KEY, 128 / 8, key, 128 / 8);

	//GENERATE AN INITIALIZATION VECTOR
	RAND_pseudo_bytes(IV, AES_BLOCK_SIZE);

	IV[AES_BLOCK_SIZE] = 0;
	remoteIV[AES_BLOCK_SIZE] = 0;

	cryptedbuffer = new unsigned char[20 * AES_BLOCK_SIZE];
	buffersize = 20 * AES_BLOCK_SIZE;

	sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
}

AES_Socket::AES_Socket(unsigned char key[128 / 8], SOCKET newConn)
{
	AES_set_encrypt_key(key, 128, &enc_key);
	AES_set_decrypt_key(key, 128, &dec_key);

	memcpy_s(KEY, 128 / 8, key, 128 / 8);

	//GENERATE AN INITIALIZATION VECTOR
	RAND_pseudo_bytes(IV, AES_BLOCK_SIZE);

	IV[AES_BLOCK_SIZE] = 0;
	remoteIV[AES_BLOCK_SIZE] = 0;


	this->sock = newConn;
	connected = true;

	cryptedbuffer = new unsigned char[20 * AES_BLOCK_SIZE];
	buffersize = 20 * AES_BLOCK_SIZE;

}

AES_Socket::AES_Socket()
{
	sock = NULL;
}

AES_Socket::AES_Socket(AES_Socket&& rthat)
{

	memcpy_s(this->KEY, 128 / 8, rthat.KEY, 128 / 8);
	memcpy_s(this->IV, AES_BLOCK_SIZE, rthat.IV, AES_BLOCK_SIZE);
	memcpy_s(this->remoteIV, AES_BLOCK_SIZE, rthat.remoteIV, AES_BLOCK_SIZE);
	this->enc_key = rthat.enc_key;
	this->dec_key = rthat.dec_key;
	this->buffersize = rthat.buffersize;

	this->cryptedbuffer = rthat.cryptedbuffer;
	rthat.cryptedbuffer = nullptr;

	this->sock = rthat.sock;
	rthat.sock = NULL;

}

int AES_Socket::setTimeOut(DWORD milliseconds)
{
	return setsockopt(sock, SOL_SOCKET, SO_RCVTIMEO, (char*)&milliseconds, sizeof(DWORD));
}

bool AES_Socket::Connect(SOCKADDR* addr, int addr_struct_size)
{

	if (connected) return false;

	if (SOCKET_ERROR == connect(sock, addr, addr_struct_size))
		return false;

	//Send initialization vector from this socket
	fprintf_s(stdout, "Sending initialization vector : %s\n", this->IV);
	if (SOCKET_ERROR == send(sock, (const char*)IV, AES_BLOCK_SIZE, 0))
		return false;

	int rc = recv(sock, (char*)remoteIV, AES_BLOCK_SIZE, 0); //receive the other IV
	if (rc <= 0) {
		fprintf_s(stderr, "Error: %i \n", WSAGetLastError());
		return false;
	}
	else if (rc < AES_BLOCK_SIZE) {
		//If we didn't get all 16 bytes loop until we get em all
		u_long bytes = (u_long)rc;
		do {
			std::this_thread::sleep_for(std::chrono::microseconds(30));
			ioctlsocket(sock, FIONREAD, &bytes);
			int bytes_to_receive = (int)std::fminf((float)(AES_BLOCK_SIZE - rc), (float)bytes);
			rc += recv(sock, ((char*)remoteIV) + rc, bytes_to_receive, 0);
		} while (rc != AES_BLOCK_SIZE);
	}
	
	fprintf_s(stdout, "Received initialization vector: %s \n", this->remoteIV);

	connected = true;
	return true;
}

int AES_Socket::Send(const char* data, size_t size, int flags /*= 0*/)
{
	//decompose the size into the header
	size_t sz = size;
	for (size_t i = 0u; i < 4u; ++i) {
		size_t val = intPow(255, 3u - i);
		header[i] = (UCHAR)(sz / val);
		sz %= val;
	}

	AES_Buffer buff = AES_Encrypt_128(enc_key, data, size, IV);

	if (SOCKET_ERROR == send(sock, (const char*)header, 4, flags))
		return -1;

	if (SOCKET_ERROR == send(sock, (const char*)buff.Data(), buff.Size(), flags))
		return -1;

	return size;

}

int AES_Socket::setSocketOption(int level, int option_name, const char* option_val, int option_length)
{  
	return setsockopt(sock, level, option_name, option_val, option_length);
}

int AES_Socket::Shutdown(int param)
{
	return shutdown(sock, param);
}

int AES_Socket::Close()
{
	return closesocket(sock);
}

int AES_Socket::Receive(char* buffer, size_t size, int flags /* = 0 */)
{
	int rec = recv(sock, (char*)header, 4, flags);
	if (rec == SOCKET_ERROR) {
		return -1;
	}

	size_t data_size = 0u;
	for (size_t i = 0u; i < 4u; ++i) {
		data_size += header[i] * intPow(255, 3u - i);
	}

	size_t bufferAmount = (data_size % AES_BLOCK_SIZE == 0 ? data_size : ((data_size + AES_BLOCK_SIZE) / AES_BLOCK_SIZE) * AES_BLOCK_SIZE);

	if (buffersize < bufferAmount) {
		if (cryptedbuffer != nullptr) 
			delete[] cryptedbuffer;
		cryptedbuffer = new unsigned char[bufferAmount];
		buffersize = bufferAmount;
	}

	rec = 0;
	u_long expected_amount;
	int breakct = 0;
	char* curr_pos = (char*)cryptedbuffer;
	do {

		ioctlsocket(sock, FIONREAD, &expected_amount);
		
		if (expected_amount > 0) {
			int current_rec = recv(sock, curr_pos, expected_amount, 0);

			if (current_rec <= 0) {
				return -1;
			}

			rec += current_rec;

			curr_pos += current_rec;

		}
		else {
			std::this_thread::sleep_for(std::chrono::microseconds(10));
			if (++breakct > BREAK_ITERATION)
				break;
		}


	} while (rec < (int)bufferAmount);

	size_t block_return_size = rec;
	block_return_size -= block_return_size % AES_BLOCK_SIZE; // ignore the last few bytes if there is an incomplete block at the end

	if (block_return_size > size)
		block_return_size = size - size % AES_BLOCK_SIZE;

	AES_Buffer buff = AES_Decrypt_128(dec_key, cryptedbuffer, block_return_size, remoteIV);

	size_t return_bytes = (block_return_size <= data_size ? block_return_size : data_size);

	memcpy_s(buffer, size, buff.Data(), return_bytes);

	return return_bytes;
}

AES_Socket::~AES_Socket()
{
	if (connected) {
		shutdown(sock, SD_BOTH);
		closesocket(sock);
		sock = NULL;
	}

	if (cryptedbuffer != nullptr)
		delete[] cryptedbuffer;

}
