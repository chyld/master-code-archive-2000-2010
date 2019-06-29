/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :18-JUL-2001 @ 09:50
Program  :ChyldFTP for Windows XP v1.0
File     :CM_Socket.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* EXTERN */
extern HWND hgWnd;

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_Socket::CM_Socket(void) {

   s = INVALID_SOCKET;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
bool CM_Socket::Socket(int socket_type, int l_type) {

   char cErrorMsg[ERROR_MSG_LENGTH];

   local_type = l_type;

   switch(socket_type) {
   case TCPSOCKET:
      s = socket(AF_INET,SOCK_STREAM,0);
      break;
   case UDPSOCKET:
      s = socket(AF_INET,SOCK_DGRAM,0);
      break;
   }

   if(s == INVALID_SOCKET) {
      sprintf(cErrorMsg,"Error on socket creation : %d",WSAGetLastError());
      MessageBox(hgWnd,cErrorMsg,"ERROR",MB_OK | MB_ICONSTOP);
      return FALSE;
   }
   
   static const UINT WSA_ASYNC_SELECT_MSG = RegisterWindowMessage(LPSTR_WSA_ASYNC_SELECT_MSG);
   WSAAsyncSelect(s,hgWnd,WSA_ASYNC_SELECT_MSG,FD_CONNECT | FD_READ | FD_WRITE | FD_ACCEPT | FD_CLOSE);
   return TRUE;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
bool CM_Socket::Connect(CM_UserInformation * pUI) {

   int nSockOpt = 1;
   int iError = 0;

   switch(local_type) {
   case CLIENT:
      memset(&(remote.sin_zero),'\0',8);
      remote.sin_family = AF_INET;
      remote.sin_port = htons(pUI->GetPortNumber());

      if(inet_addr(pUI->GetHostname()) == INADDR_NONE) {
         h = gethostbyname(pUI->GetHostname());
         if(h) {
            memcpy(&(remote.sin_addr.s_addr), h->h_addr_list[0], h->h_length);
         }
         else {
            MessageBox(hgWnd,"Error determining hostname","ERROR",MB_OK | MB_ICONSTOP);
            closesocket(s);
            return FALSE;
         }
      }
      else {
         remote.sin_addr.s_addr = inet_addr(pUI->GetHostname());
      }

      if(connect(s, (struct sockaddr *)&remote,sizeof(remote)))
         if(WSAEWOULDBLOCK != (iError = WSAGetLastError())) {
            closesocket(s);
            return FALSE;
         }
      break;
   case SERVER:
      memset(&(local.sin_zero),'\0',8);
      local.sin_family = AF_INET;
      local.sin_port = htons(0);
      local.sin_addr.s_addr = htonl(INADDR_ANY);

      if(setsockopt(s,SOL_SOCKET,SO_REUSEADDR,(const char *)&nSockOpt,sizeof(int))) {
         closesocket(s);
         return FALSE;
      }

      if(setsockopt(s,SOL_SOCKET,SO_KEEPALIVE,(const char *)&nSockOpt,sizeof(int))) {
         closesocket(s);
         return FALSE;
      }

      if(bind(s, (struct sockaddr *)&local,sizeof(local))) {
         closesocket(s);
         return FALSE;
      }

      if(listen(s,SOMAXCONN)) {
         closesocket(s);
         return FALSE;
      }
      break;
   }
   return TRUE;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
unsigned int CM_Socket::GetLocalPortNumber(void) {
   
   int nlocal = sizeof(local);

   if(getsockname(s,(struct sockaddr *)&local,&nlocal)) {
      closesocket(s);
      return FALSE;
   }
   return ntohs(local.sin_port);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
unsigned int CM_Socket::GetLocalIPAddress(void) {

   int nlocal = sizeof(local);

   if(getsockname(s,(struct sockaddr *)&local,&nlocal)) {
      closesocket(s);
      return FALSE;
   }
   return ntohl(local.sin_addr.s_addr);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
unsigned int CM_Socket::GetRemotePortNumber(void) {

   int nremote = sizeof(remote);

   if(getpeername(s,(struct sockaddr *)&remote,&nremote)) {
      closesocket(s);
      return FALSE;
   }
   return ntohs(remote.sin_port);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
unsigned int CM_Socket::GetRemoteAddress(void) {

   int nremote = sizeof(remote);

   if(getpeername(s,(struct sockaddr *)&remote,&nremote)) {
      closesocket(s);
      return FALSE;
   }
   return ntohl(remote.sin_addr.s_addr);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Socket::SetSocket(SOCKET socket_in) {

   s = socket_in;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
SOCKET CM_Socket::GetSocket(void) {

   return s;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_Socket::~CM_Socket(void) {
   
   closesocket(s);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
