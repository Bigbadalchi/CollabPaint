
#include <stdio.h>

#include <openssl/aes.h>
#include <openssl/rand.h>

#include <string.h>
#include <utility>

class AES_Buffer {
private:
	unsigned char* data = nullptr; 
	size_t size;
	friend AES_Buffer AES_Encrypt_128(AES_KEY &key, const char* data, const size_t data_len, unsigned char iv[AES_BLOCK_SIZE]);
	friend AES_Buffer AES_Decrypt_128(AES_KEY &key, const unsigned char* data, const size_t data_len, unsigned char iv[AES_BLOCK_SIZE]);
	AES_Buffer(size_t size);
public:
	const unsigned char* Data() const { return data; }
	size_t Size() const { return size; }
	AES_Buffer(const AES_Buffer& that);
	AES_Buffer(AES_Buffer&& rthat);
	~AES_Buffer();
};

static inline void randomKey(unsigned char key[]) {
	RAND_pseudo_bytes(key, sizeof(key));
}