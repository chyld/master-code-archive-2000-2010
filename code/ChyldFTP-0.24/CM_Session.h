/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :17-JUL-2001 @ 14:40
Program  :ChyldFTP for Windows XP v1.0
File     :CM_Session.h
 * -------------------------------------------------------------------------- */


#ifndef CM_SESSION_H_INCLUDED
#define CM_SESSION_H_INCLUDED

#include "classheader.h"
#include "cm_userinformation.h"
#include "cm_socket.h"
#include "cm_log.h"
#include "cm_filedownload.h"
#include "cm_state.h"
#include "cm_ftpcommands.h"
#include "cm_search.h"

class CM_Session {
public:
   CM_Session(void);
   void InitializeSession(CM_UserInformation * pUI);
   SOCKET GetSocket(int l_type);
   unsigned int GetSessionID(void);
   void SocketMessageParser(LPARAM lParam, WPARAM wParam);
   bool ReConnectSocket(void);
   void CheckIfRunning(void);
   void SearchForFile(void);
  ~CM_Session(void);

   friend class CM_SessionList;

private:
   void FD_CONNECT_cmdMessage(LPARAM lParam, WPARAM wParam);
   void FD_CONNECT_dataMessage(LPARAM lParam, WPARAM wParam);
   void FD_ACCEPT_cmdMessage(LPARAM lParam, WPARAM wParam);
   void FD_ACCEPT_dataMessage(LPARAM lParam, WPARAM wParam);
   void FD_READ_cmdMessage(LPARAM lParam, WPARAM wParam);
   void FD_READ_dataMessage(LPARAM lParam, WPARAM wParam);
   void FD_CLOSE_cmdMessage(LPARAM lParam, WPARAM wParam);
   void FD_CLOSE_dataMessage(LPARAM lParam, WPARAM wParam);
   bool IsSocketWritable(SOCKET sWrite);
   bool IsSocketReadable(SOCKET sWrite);
   void DestroySession(void);
   char * GetRecvData(char * cBuf, SOCKET sRecv);
   int  GetFileData(char * cBuf, SOCKET sRecv);
   void Authenticate(void);
   bool ParseRecvData(char * lpstrBuffer, char * lpstrParse);
   void OpenDataPort(void);
   void CloseDataPort(void);
   char * DetermineDataPort(void);
   void AssembleDirectoryListing(char * Buffer);
   void FileManagement(void);
   void DownloadFile(void);
   void SetDownloadPort(char * retr);

private:
   CM_Socket            dataSocket, cmdSocket;
   CM_UserInformation   userInfo;
   CM_State             state;
   CM_Log               log;
   CM_FTPCommands       ftpCmds;
   CM_Search            search;
/*
   CM_HTTPCommands      httpCmds;
   CM_FileDownload      download;
*/
   CM_Session           * pPrev;
   CM_Session           * pNext;
   unsigned int         sessionID;
   char                 currentDir[10000];
   char                 fullDirListing[256000];
   int                  list_in_progress;
   int                  download_in_progress;
   int                  data_close;
   int                  change_dir_OK;
   int                  running;
};

#endif


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
