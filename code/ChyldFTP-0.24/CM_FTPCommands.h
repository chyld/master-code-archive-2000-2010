/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :26-JUL-2001 @ 10:16
Program  :ChyldFTP for Windows XP v1.0
File     :CM_FTPCommands.h
 * -------------------------------------------------------------------------- */


#ifndef CM_FTPCOMMANDS_H_INCLUDED
#define CM_FTPCOMMANDS_H_INCLUDED

#include "classheader.h"

class CM_FTPCommands {
public:
   void Initialize(CM_UserInformation * pUI);
   int SendUsername(SOCKET s);
   int SendPassword(SOCKET s);
private:
   CM_UserInformation ui;
};

#endif


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
