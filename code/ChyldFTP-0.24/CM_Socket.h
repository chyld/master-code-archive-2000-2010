/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :17-JUL-2001 @ 15:09
Program  :ChyldFTP for Windows XP v1.0
File     :CM_Socket.h
 * -------------------------------------------------------------------------- */


#ifndef CM_SOCKET_H_INCLUDED
#define CM_SOCKET_H_INCLUDED

#include "classheader.h"

class CM_Socket {
public:
   CM_Socket(void);
   bool Socket(int socket_type, int l_type);
   bool Connect(CM_UserInformation * pUI);
   unsigned int GetLocalPortNumber(void);
   unsigned int GetLocalIPAddress(void);
   unsigned int GetRemotePortNumber(void);
   unsigned int GetRemoteAddress(void);
   void SetSocket(SOCKET socket_in);
   SOCKET GetSocket(void);
  ~CM_Socket(void);
private:
   SOCKET s;
   struct sockaddr_in remote, local;
   struct hostent * h;
   int local_type;
};

#endif


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
