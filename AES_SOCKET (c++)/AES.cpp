
#include "AES.h"

//AES_Buffer CLASS Implementation
AES_Buffer::AES_Buffer(size_t size)
{
	data = new unsigned char[size];
	this->size = size;
}

AES_Buffer::AES_Buffer(const AES_Buffer& that)
{
	this->size = that.size;
	this->data = new unsigned char[this->size];
	memcpy_s(this->data, this->size, that.data, that.size);
}

AES_Buffer::AES_Buffer(AES_Buffer&& rthat) {
	this->size = std::move(rthat.size);
	this->data = rthat.data;
	rthat.data = nullptr;
}

AES_Buffer::~AES_Buffer()
{
	if (data != nullptr)
		delete[] data;
}


//AES Implementation

AES_Buffer AES_Encrypt_128(AES_KEY &key, const char* data, const size_t data_len, unsigned char iv[AES_BLOCK_SIZE])
{
	size_t first_enc = data_len / AES_BLOCK_SIZE * AES_BLOCK_SIZE;
	size_t enc_len = (data_len % AES_BLOCK_SIZE == 0? first_enc : first_enc + AES_BLOCK_SIZE);
	AES_Buffer encryption(enc_len);
	
	if (first_enc < enc_len) {
		//pad
		char* padded_data = new char[enc_len];
		memcpy_s(padded_data, enc_len, data, data_len);
		memset(padded_data + data_len, 0, enc_len - data_len);
		AES_cbc_encrypt((const unsigned char*)data, encryption.data, enc_len, &key, iv, AES_ENCRYPT);
	}
	else
		AES_cbc_encrypt((const unsigned char*)data, encryption.data, first_enc, &key, iv, AES_ENCRYPT);
	
	return encryption;
}

AES_Buffer AES_Decrypt_128(AES_KEY &key, const unsigned char* data, const size_t encrypted_len, unsigned char iv[AES_BLOCK_SIZE])
{
	AES_Buffer decryption(encrypted_len);
	AES_cbc_encrypt(data, decryption.data, encrypted_len, &key, iv, AES_DECRYPT);
	return decryption;
}


