/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :24-JUL-2001 @ 21:31
Program  :ChyldFTP for Windows XP v1.0
File     :CM_Log.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_Log::CM_Log(void) {

   char cBuf[100];
   GetSystemTime(&SystemTime);

   sprintf(cBuf,"c:\\log\\%0.4d-%0.2d-%0.2d@%0.2d.%0.2d.%0.2d.%0.3d.txt\0",
                              SystemTime.wYear,
                              SystemTime.wMonth,
                              SystemTime.wDay,
                              SystemTime.wHour,
                              SystemTime.wMinute,
                              SystemTime.wSecond,
                              SystemTime.wMilliseconds);

   hFile = CreateFile(cBuf,
                      GENERIC_READ | GENERIC_WRITE,
                      FILE_SHARE_READ,
                      NULL,
                      CREATE_ALWAYS,
                      FILE_ATTRIBUTE_NORMAL,
                      NULL);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Log::New(char * fname) {

   char cBuf[100];

   sprintf(cBuf,"c:\\down\\%s",fname);

   hDown = CreateFile(cBuf,
                      GENERIC_READ | GENERIC_WRITE,
                      FILE_SHARE_READ,
                      NULL,
                      CREATE_ALWAYS,
                      FILE_ATTRIBUTE_NORMAL,
                      NULL);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Log::Save(char * data, int len) {

   DWORD dwBytesWritten;

   WriteFile(hDown,data,len,&dwBytesWritten,NULL);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Log::Close(void) {

   CloseHandle(hDown);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Log::Write(char * lpData, int nTmpA, int nTmpB, int nTmpC) {

   DWORD dwBytesWritten;
   char cBuf[MAX_LOG_WRITE_BUFFER];

   sprintf(cBuf,lpData,nTmpA,nTmpB,nTmpC);

   WriteFile(hFile,cBuf,strlen(cBuf),&dwBytesWritten,NULL);
   WriteFile(hFile,"\r\n",2,&dwBytesWritten,NULL);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Log::Write(char * lpData, char * pStr, int nTmpA, int nTmpB) {
   
   DWORD dwBytesWritten;
   char cBuf[MAX_LOG_WRITE_BUFFER];

   sprintf(cBuf,lpData,pStr,nTmpA,nTmpB);

   WriteFile(hFile,cBuf,strlen(cBuf),&dwBytesWritten,NULL);
   WriteFile(hFile,"\r\n",2,&dwBytesWritten,NULL);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Log::Write(char * lpData, char * pStr, float fTmpA, int nTmpA) {
   
   DWORD dwBytesWritten;
   char cBuf[MAX_LOG_WRITE_BUFFER];

   sprintf(cBuf,lpData,pStr,fTmpA,nTmpA);

   WriteFile(hFile,cBuf,strlen(cBuf),&dwBytesWritten,NULL);
   WriteFile(hFile,"\r\n",2,&dwBytesWritten,NULL);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Log::Write(char * lpData, char * pStr) {

   DWORD dwBytesWritten;
   char cBuf[MAX_LOG_WRITE_BUFFER];

   sprintf(cBuf,lpData,pStr);

   WriteFile(hFile,cBuf,strlen(cBuf),&dwBytesWritten,NULL);
   WriteFile(hFile,"\r\n",2,&dwBytesWritten,NULL);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Log::Write(char * lpData) {

   DWORD dwBytesWritten;
   char cBuf[MAX_LOG_WRITE_BUFFER];

   strcpy(cBuf,lpData);

   WriteFile(hFile,cBuf,strlen(cBuf),&dwBytesWritten,NULL);
   WriteFile(hFile,"\r\n",2,&dwBytesWritten,NULL);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_Log::~CM_Log(void) {

   CloseHandle(hFile);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
