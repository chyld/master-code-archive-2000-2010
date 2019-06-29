/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :18-JUL-2001 @ 15:43
Program  :ChyldFTP for Windows XP v1.0
File     :CM_Session.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* EXTERN */
extern HWND hgWnd;

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_Session::CM_Session(void) {

   log.Write("Creating Session Object");
   
   pPrev = pNext     = NULL;
   strcpy(currentDir,"/\0");
   fullDirListing[0] = NULL;
   list_in_progress  = 0;
   download_in_progress = 0;
   data_close        = 0;
   change_dir_OK     = 0;
   running           = 1;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_Session::~CM_Session(void) {

   log.Write("Destroying Session Object");
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
unsigned int CM_Session::GetSessionID(void) {

   return sessionID;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::SocketMessageParser(LPARAM lParam, WPARAM wParam) {

   running = 1;

   switch(LOWORD(lParam)) {
   case FD_CONNECT:
      log.Write("FD_CONNECT lP -> %d wP -> %d",lParam,wParam);
      if(wParam == cmdSocket.GetSocket())
         FD_CONNECT_cmdMessage(lParam,wParam);
      else
         FD_CONNECT_dataMessage(lParam,wParam);
      break;
   case FD_READ:
      log.Write("FD_READ lP -> %d wP -> %d",lParam,wParam);
      if(wParam == cmdSocket.GetSocket())
         FD_READ_cmdMessage(lParam,wParam);
      else
         FD_READ_dataMessage(lParam,wParam);
      break;
   case FD_WRITE:
      log.Write("FD_WRITE lP -> %d wP -> %d",lParam,wParam);
      break;
   case FD_ACCEPT:
      log.Write("FD_ACCEPT lP -> %d wP -> %d",lParam,wParam);
      if(wParam == cmdSocket.GetSocket())
         FD_ACCEPT_cmdMessage(lParam,wParam);
      else
         FD_ACCEPT_dataMessage(lParam,wParam);
      break;
   case FD_CLOSE:
      log.Write("FD_CLOSE lP -> %d wP -> %d",lParam,wParam);
      if(wParam == cmdSocket.GetSocket())
         FD_CLOSE_cmdMessage(lParam,wParam);
      else
         FD_CLOSE_dataMessage(lParam,wParam);
      break;
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::FD_CONNECT_cmdMessage(LPARAM lParam, WPARAM wParam) {

   if((state.GetState() == INITIAL) && (IsSocketWritable(wParam)) && (state.GetSocketState())) {
      log.Write("Successfully Connected on Socket %d",wParam);
      KillTimer(hgWnd,GetSessionID() | 0x1);
      state.SetState(CONNECTED);
      Authenticate();
   }
   if((state.GetState() == INITIAL) && (state.GetRetry())) {
      log.Write("Not Connected Yet -> Sending ReConnect Message");
      int nPause = (userInfo.GetRetryDelay())*(1000);
      SetTimer(hgWnd,GetSessionID() | 0x1,nPause,NULL);
   }
   if((state.GetState() == INITIAL) && (!state.GetRetry())) {
      DestroySession();
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::FD_CONNECT_dataMessage(LPARAM lParam, WPARAM wParam) {
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::FD_ACCEPT_cmdMessage(LPARAM lParam, WPARAM wParam) {
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::FD_ACCEPT_dataMessage(LPARAM lParam, WPARAM wParam) {

   struct sockaddr sa;
   int sa_len = sizeof(sa);
   SOCKET new_socket;

   log.Write("Old SOCKET for Data Transfer -> %d",dataSocket.GetSocket());

   new_socket = accept(wParam,&sa,&sa_len);
   CloseDataPort();
   dataSocket.SetSocket(new_socket);

   log.Write("New SOCKET for Data Transfer -> %d",dataSocket.GetSocket());
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::FD_READ_cmdMessage(LPARAM lParam, WPARAM wParam) {

   char cBuf[MAX_TCP_RECEIVE_BUFFER];
   char cTmp[100];

   GetRecvData(cBuf,wParam);

   switch(state.GetState()) {
   case CONNECTED:
      if(ParseRecvData(cBuf,LOGIN_OK)) {
         log.Write("Session IS Authenticated");
         state.SetState(AUTHENTICATED);
         send(cmdSocket.GetSocket(),"TYPE I\r\n",8,0);
         SetTimer(hgWnd,GetSessionID() | 0x2,MAX_WAIT_FOR_SEARCH,NULL);
      }
      else if(ParseRecvData(cBuf,CLOSING_SERVICE) ||
              ParseRecvData(cBuf,LOGIN_INCORRECT) ||
              ParseRecvData(cBuf,SERVICE_TIMEOUT) ||
              ParseRecvData(cBuf,SERVICE_NOT_READY) ) {
         log.Write("Service was closed by Server");
         int nPause = (userInfo.GetRetryDelay())*(1000);
         SetTimer(hgWnd,GetSessionID() | 0x1,nPause,NULL);
         state.SetState(INITIAL);
      }
      else {
         log.Write("Not Authenticated Yet");
      }
      break;
   case AUTHENTICATED:
      if(ParseRecvData(cBuf,DOWNLOADING_DATA)&&
			list_in_progress) {
         log.Write("Receiving a directory listing");
         state.SetState(SEARCHING);
         recv(dataSocket.GetSocket(),cTmp,sizeof(cTmp),MSG_PEEK);
      }
      if(ParseRecvData(cBuf,DOWNLOADING_DATA)&&
			download_in_progress) {
         log.Write("Receiving a file");
         state.SetState(DOWNLOADING);
         recv(dataSocket.GetSocket(),cTmp,sizeof(cTmp),MSG_PEEK);
      }
      if(ParseRecvData(cBuf,COMMAND_SUCCESS)) {
         change_dir_OK = 1;
         SetTimer(hgWnd,GetSessionID() | 0x2,MAX_WAIT_FOR_SEARCH,NULL);
         log.Write("Change Directory OK");
      }
      if(ParseRecvData(cBuf,NOT_A_DIRECTORY)) {
         log.Write("Not A Directory");
         FileManagement();
      }
      break;
   case SEARCHING:
      break;
   case DOWNLOADING:
      break;
   case FINISHED:
      break;
   default:
      log.Write("FD READ - default state");
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::FD_READ_dataMessage(LPARAM lParam, WPARAM wParam) {
   
   int recv_len;
   char cBuf[MAX_TCP_RECEIVE_BUFFER];

   if(state.GetState() == SEARCHING) {   
      GetRecvData(cBuf,wParam);
      AssembleDirectoryListing(cBuf);
      if(data_close) {
         log.Write("Manually Calling FD_CLOSE data Message");
         FD_CLOSE_dataMessage(lParam,wParam);
      }
   }
   if(state.GetState() == DOWNLOADING) {
      recv_len = GetFileData(cBuf,wParam);
      log.Save(cBuf,recv_len);
      if(data_close) {
         log.Write("Manually Calling FD_CLOSE data Message");
         FD_CLOSE_dataMessage(lParam,wParam);
      }
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::FD_CLOSE_dataMessage(LPARAM lParam, WPARAM wParam) {

   int recv_len;
   char cBuf[MAX_TCP_RECEIVE_BUFFER];
   char cTmp[100];

   if(state.GetState() == SEARCHING) {
      log.Write("Any Remaining Data?");
      while(recv(dataSocket.GetSocket(),cTmp,sizeof(cTmp),MSG_PEEK)) {
         GetRecvData(cBuf,dataSocket.GetSocket());
         AssembleDirectoryListing(cBuf);
      }
      log.Write("Parsing Directory Listing");
      log.Write(fullDirListing);
      search.ParseData(fullDirListing,currentDir);
      fullDirListing[0] = NULL;
      list_in_progress = 0;
      data_close = 0;
      state.SetState(AUTHENTICATED);
      FileManagement();
   } else if(state.GetState() == DOWNLOADING) {
      log.Write("Any Remaining Data?");
      while(recv(dataSocket.GetSocket(),cTmp,sizeof(cTmp),MSG_PEEK)) {
         recv_len = GetFileData(cBuf,dataSocket.GetSocket());
         log.Save(cBuf,recv_len);
      }
      log.Close();
      download_in_progress = 0;
      data_close = 0;
      state.SetState(AUTHENTICATED);
      DownloadFile();
   } else {
      data_close = 1;
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::FD_CLOSE_cmdMessage(LPARAM lParam, WPARAM wParam) {
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::FileManagement(void) {

   char * pFile;

   pFile = search.SearchData();
   if(strlen(pFile)) {
      strcpy(currentDir,pFile);
      SetTimer(hgWnd,GetSessionID() | 0x2,MAX_WAIT_FOR_SEARCH,NULL);
   }
   else {
      currentDir[0] = NULL;
      DownloadFile();
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::DownloadFile(void) {

   char retr[10000];
   char * pFile;

   log.Write("Checking to see if there is a file to download");

   pFile = search.GetFile(userInfo.GetSearch());
   if(strlen(pFile)) {
      strcpy(retr,"RETR \0");
      strcat(retr,pFile);
      strcat(retr,"\r\n");
      SetDownloadPort(retr);
      log.New(search.GetRealFileName(pFile));
      log.Write("Get Command Sent ---> %s",retr);
   }
   else {
      DestroySession();
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::AssembleDirectoryListing(char * Buffer) {

   strcat(fullDirListing,Buffer);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
bool CM_Session::ParseRecvData(char * lpstrBuffer, char * lpstrParse) {

   if(strstr(lpstrBuffer,lpstrParse))
      return TRUE;
   return FALSE;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
char * CM_Session::GetRecvData(char * cBuf, SOCKET sRecv) {
   
   int recvLen = recv(sRecv,cBuf,(MAX_TCP_RECEIVE_BUFFER-2),0);
   if(recvLen == SOCKET_ERROR) {
      log.Write("Socket Error : %d",recvLen);
      cBuf[0] = NULL;
      return NULL;
   }
   cBuf[recvLen] = '\0';
   log.Write("%s",cBuf);
   return cBuf;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
int CM_Session::GetFileData(char * cBuf, SOCKET sRecv) {
   
   int recvLen = recv(sRecv,cBuf,(MAX_TCP_RECEIVE_BUFFER-2),0);
   if(recvLen == SOCKET_ERROR) {
      log.Write("Socket Error : %d",recvLen);
      return 0;
   }
   return recvLen;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
bool CM_Session::IsSocketWritable(SOCKET sWrite) {

   int maxfds = sWrite;
   struct timeval tv;
   fd_set writefds;

   tv.tv_sec  = 0;
   tv.tv_usec = 0;

   FD_ZERO(&writefds);
   FD_SET(sWrite,&writefds);

   select(maxfds,NULL,&writefds,NULL,&tv);

   for(unsigned int i = 0; i <= sWrite; i++)
      if(FD_ISSET(i,&writefds))
         return TRUE;
   return FALSE;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
bool CM_Session::IsSocketReadable(SOCKET sRead) {

   int maxfds = sRead;
   struct timeval tv;
   fd_set readfds;

   tv.tv_sec  = 0;
   tv.tv_usec = 0;

   FD_ZERO(&readfds);
   FD_SET(sRead,&readfds);

   select(maxfds,&readfds,NULL,NULL,&tv);

   for(unsigned int i = 0; i <= sRead; i++)
      if(FD_ISSET(i,&readfds))
         return TRUE;
   return FALSE;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
bool CM_Session::ReConnectSocket(void) {

   if(state.GetRetry() && (state.GetState() == INITIAL) ) {

      log.Write("RECONNECTING - old socket %d -> Retries Left %d",cmdSocket.GetSocket(),state.GetRetry());
      closesocket(cmdSocket.GetSocket());
      cmdSocket.Socket(TCPSOCKET,CLIENT);
      state.SetSocketState(cmdSocket.Connect(&userInfo));
      state.SetRetry(state.GetRetry() - 1);
      log.Write("RECONNECTING - new socket %d",cmdSocket.GetSocket());
      return TRUE;
   }
   if(!state.GetRetry()) {
      DestroySession();
      return TRUE;
   }
   return FALSE;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::SearchForFile(void) {

   char cwd[10000];

   if(strlen(currentDir) && (!change_dir_OK)) {
      strcpy(cwd,"CWD \0");
      strcat(cwd,currentDir);
      strcat(cwd,"\r\n");
      log.Write("\t\t\t[CD ==]>%s",cwd);
      send(cmdSocket.GetSocket(),cwd,strlen(cwd),0);
      KillTimer(hgWnd,GetSessionID() | 0x2);
   }

   if(strlen(currentDir) && change_dir_OK) {
      log.Write("Searching for file");
      CloseDataPort();
      OpenDataPort();
      char * pPort = DetermineDataPort();

      send(cmdSocket.GetSocket(),pPort,strlen(pPort),0);
      send(cmdSocket.GetSocket(),"LIST\r\n",6,0);

      delete pPort;
      list_in_progress = 1;
      KillTimer(hgWnd,GetSessionID() | 0x2);
      change_dir_OK = 0;
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::SetDownloadPort(char * retr) {

   CloseDataPort();
   OpenDataPort();
   char * pPort = DetermineDataPort();

   send(cmdSocket.GetSocket(),pPort,strlen(pPort),0);
   send(cmdSocket.GetSocket(),retr,strlen(retr),0);

   delete pPort;
   download_in_progress = 1;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
char * CM_Session::DetermineDataPort(void) {

   unsigned int msb, lsb, ipaddress;
   char cMSB[25], cLSB[25];
   struct in_addr in;
   
   char * cPort = new char[100];

   msb = lsb = dataSocket.GetLocalPortNumber();
   ipaddress = cmdSocket.GetLocalIPAddress();

   msb = msb >> 8;
   lsb = lsb & 0xFF;
   itoa(msb,cMSB,10);
   itoa(lsb,cLSB,10);
   strcat(cMSB,",");
   strcat(cMSB,cLSB);

   in.S_un.S_addr = htonl(ipaddress);
   strcpy(cPort,"PORT ");
   strcat(cPort,inet_ntoa(in));

   strcat(cPort,",");
   strcat(cPort,cMSB);
   strcat(cPort,"\r\n");

   while(strstr(cPort,".")) {
      strncpy(strstr(cPort,"."),",",1);
   }
   
   log.Write("%s",cPort);

   return cPort;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::OpenDataPort(void) {

   dataSocket.Socket(TCPSOCKET,SERVER);
   dataSocket.Connect(&userInfo);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::CloseDataPort(void) {

   closesocket(dataSocket.GetSocket());
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::DestroySession(void) {

   static const UINT DESTROY_SESSION_OBJ_MSG = RegisterWindowMessage(LPSTR_DESTROY_SESSN_OBJECT);

   KillTimer(hgWnd,GetSessionID() | 0x1);
   KillTimer(hgWnd,GetSessionID() | 0x2);
   KillTimer(hgWnd,GetSessionID() | 0x3);
   PostMessage(hgWnd,DESTROY_SESSION_OBJ_MSG,GetSessionID(),0);
   log.Write("Session Timeout -> Sending Destroy Message");
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::InitializeSession(CM_UserInformation * pUI) {

   SYSTEMTIME SystemTime;
   userInfo = *pUI;

   state.SetRetry(userInfo.GetRetries());
   cmdSocket.Socket(TCPSOCKET,CLIENT);
   state.SetSocketState(cmdSocket.Connect(&userInfo));

   GetSystemTime(&SystemTime);

   sessionID = (SystemTime.wDay           <<  26)    |
               (SystemTime.wHour          <<  21)    |
               (SystemTime.wMinute        <<  15)    |
               (SystemTime.wSecond        <<   9)    |
               (SystemTime.wMilliseconds & 0x1FF);

   sessionID = (sessionID & 0xFFFFFFF8);

   ftpCmds.Initialize(pUI);

   log.Write("Initializing Session %d",sessionID);

   SetTimer(hgWnd,GetSessionID() | 0x4,MAX_WAIT_CHECK_RUNNING,NULL);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::Authenticate(void) {

   ftpCmds.SendUsername(cmdSocket.GetSocket());
   ftpCmds.SendPassword(cmdSocket.GetSocket());

   log.Write("Username and Password Sent");
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
SOCKET CM_Session::GetSocket(int l_type) {

   if(l_type == SERVER)
      return dataSocket.GetSocket();
   return cmdSocket.GetSocket();
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Session::CheckIfRunning(void) {

   if(!running) {
      log.Write("Connection Timeout - Reconnecting...");
      state.SetRetry(userInfo.GetRetries());
      state.SetState(INITIAL);
      ReConnectSocket();
   }
   else {
      log.Write("Still Running");
      running = 0;
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
