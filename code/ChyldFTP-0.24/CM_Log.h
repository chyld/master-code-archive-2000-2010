/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :17-JUL-2001 @ 15:05
Program  :ChyldFTP for Windows XP v1.0
File     :CM_Log.h
 * -------------------------------------------------------------------------- */


#ifndef CM_LOG_H_INCLUDED
#define CM_LOG_H_INCLUDED

#include "classheader.h"

class CM_Log {
public:
   CM_Log(void);
   void Write(char * lpData, int nTmpA, int nTmpB = 0, int nTmpC = 0);
   void Write(char * lpData, char * pStr, int nTmpA, int nTmpB = 0);
   void Write(char * lpData, char * pStr, float fTmpA, int nTmpA = 0);
   void Write(char * lpData, char * pStr);
   void Write(char * lpData);
   void New(char * fname);
   void Save(char * data, int len);
   void Close(void);
  ~CM_Log(void);
private:
   SYSTEMTIME SystemTime;
   HANDLE hFile;
   HANDLE hDown;
};

#endif


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
