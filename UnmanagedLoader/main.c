#include <windows.h>
#include <wincrypt.h>
#include <stdio.h>

extern char* file;

int main(int argc, char** argv)
{
	BYTE* data = malloc(1024 * 1024);
	DWORD size;
	if (CryptStringToBinaryA(file, 0, CRYPT_STRING_BASE64, data, &size, NULL, NULL))
	{
		printf("success\n");
		FILE* fp;
		fp = fopen("D:\\b.exe", "wb");
		fwrite(data, 1, size, fp);
		fclose(fp);
	}
	return 0;
}

int main2(int argc, char** argv)
{
	FILE *fp, *fp2;
	fp = fopen("D:\\CpyFcDel.NET.exe", "rb");
	fseek(fp, 0L, SEEK_END);
	long size = ftell(fp);
	rewind(fp);
	BYTE* fcontent = malloc(size);
	char* encrypted = malloc(1024 * 1024);
	DWORD outputsize;
	fread(fcontent, 1, size, fp);
	CryptBinaryToStringA(fcontent, size, CRYPT_STRING_BASE64, encrypted, &outputsize);
	fp2 = fopen("D:\\out.txt", "wb");
	fwrite(encrypted, 1, outputsize, fp2);
	fclose(fp2);

	return 0;
}
