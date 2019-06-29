/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :16-JUL-2001 @ 21:42
Program  :ChyldFTP for Windows XP v1.0
File     :ClassHeader.h
 * -------------------------------------------------------------------------- */


#ifndef CLASSHEADER_H_INCLUDED
#define CLASSHEADER_H_INCLUDED

class CM_FileDownload;
class CM_Log;
class CM_Socket;
class CM_UserInformation;
class CM_Session;
class CM_SessionList;
class CM_State;
class CM_FTPCommands;
class CM_Search;
class CM_FileNode;

#define CLIENT                      1
#define SERVER                      2
#define TCPSOCKET                   3
#define UDPSOCKET                   4
#define MAX_NAME_LENGTH             50
#define ERROR_MSG_LENGTH            50
#define MAX_FTP_COMMAND_LENGTH      100
#define MAX_WAIT_FOR_SEARCH         2000
#define MAX_WAIT_CHECK_RUNNING      60000
#define MAX_EDIT_CONTROL_LENGTH     5000
#define MAX_TCP_RECEIVE_BUFFER      4500
#define MAX_LOG_WRITE_BUFFER        25000
#define LPSTR_DESTROY_SESSN_OBJECT  "USER_DEFINED_MESSAGE__CHYLD-FTP-WINDOWS-XP-25-JULY-2001@09:51:32.15"
#define LPSTR_USER_INFORMATION_MSG  "USER_DEFINED_MESSAGE__CHYLD-FTP-WINDOWS-XP-17-JULY-2001@16:15:45.33"
#define LPSTR_WSA_ASYNC_SELECT_MSG  "USER_DEFINED_MESSAGE__CHYLD-FTP-WINDOWS-XP-18-JULY-2001@16:28:54:21"

#define LOGIN_OK                    "230 "
#define CLOSING_SERVICE             "221 "
#define LOGIN_INCORRECT             "530 "
#define TRANSFER_COMPLETE           "226 "
#define DOWNLOADING_DATA          	"150 "
#define COMMAND_SUCCESS             "250 "
#define NOT_A_DIRECTORY             "550 "
#define SERVICE_NOT_READY           "421 "
#define SERVICE_TIMEOUT             "425 "

#define INITIAL                     0
#define CONNECTED                   1
#define AUTHENTICATED               2
#define SEARCHING                   3
#define DOWNLOADING                 4
#define FINISHED                    5

bool RegisterWindowClass(void);
bool CreateMotherWindow(void);
bool InitializeWinsock(int majver, int minver);
void DisplayWindow(int nShowCmd);

void ConnectToFTPServer(CM_UserInformation * pUI, CM_SessionList * pSL);

LRESULT APIENTRY MainWindowsMessageHandler(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
LRESULT APIENTRY ChyldFTPDialogHandler(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam);
LRESULT APIENTRY ChyldFTPAboutHandler(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam);

#endif


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */