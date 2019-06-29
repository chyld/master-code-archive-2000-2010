/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :17-JUL-2001 @ 15:11
Program  :ChyldFTP for Windows XP v1.0
File     :CM_UserInformation.h
 * -------------------------------------------------------------------------- */


#ifndef CM_USERINFORMATION_H_INCLUDED
#define CM_USERINFORMATION_H_INCLUDED

#include "classheader.h"

class CM_UserInformation {
public:
   CM_UserInformation(void);

   void SetHostname(char * host);
   void SetUsername(char * user);
   void SetPassword(char * pass);
   void SetMode(char * mode);
   void SetLocalDir(char * dir);
   void SetSearch(char * search);
   void SetRetries(int retry);
   void SetRetryDelay(int delay);
   void SetPortNumber(int port);

   char * GetHostname(void);
   char * GetUsername(void);
   char * GetPassword(void);
   char * GetMode(void);
   char * GetLocalDir(void);
   char * GetSearch(void);
   int GetRetries(void);
   int GetRetryDelay(void);
   int GetPortNumber(void);
private:
   char lpstrHostname[MAX_NAME_LENGTH];
   char lpstrUsername[MAX_NAME_LENGTH];
   char lpstrPassword[MAX_NAME_LENGTH];
   char lpstrMode[MAX_NAME_LENGTH];
   char lpstrLocalDir[MAX_NAME_LENGTH];
   char lpstrSearch[MAX_NAME_LENGTH];
   int nRetries;
   int nRetryDelay;
   int nPort;
};

#endif


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
