/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :16-JUL-2001 @ 22:45
Program  :ChyldFTP for Windows XP v1.0
File     :WindowRegistration.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* EXTERN */
extern HINSTANCE hgInstance;
extern HWND hgWnd;
extern WSADATA wsaData;

/* GLOBALS */
char lpszWinTitle[]  = "ChyldFTP v1.0 for Windows XP";
char lpszClassName[] = "ChyldFTP";

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */

/*
typedef struct _WNDCLASS { 
    UINT       style;         //Specifies the class style(s). 
    WNDPROC    lpfnWndProc;   //Pointer to the window procedure.
    int        cbClsExtra;    //Specifies the number of extra bytes for structure.
    int        cbWndExtra;    //Specifies the number of extra bytes for instance.
    HINSTANCE  hInstance;     //Contains the handle to the window procedure for the class.
    HICON      hIcon;         //Handle to the class icon.   
    HCURSOR    hCursor;       //Handle to the class cursor.   
    HBRUSH     hbrBackground; //Handle to the class background brush.
    LPCTSTR    lpszMenuName;  //If NULL, windows belonging to this class have no menu.
    LPCTSTR    lpszClassName; //Pointer to a null-terminated string or is an atom.
} WNDCLASS, *PWNDCLASS; 
*/

bool RegisterWindowClass(void) {

   WNDCLASS wc;

   wc.cbClsExtra = 0;
   wc.cbWndExtra = 0;
   wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
   wc.hCursor = LoadCursor(NULL, IDC_ARROW);
   wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
   wc.hInstance =  hgInstance;
   wc.lpfnWndProc = (WNDPROC)MainWindowsMessageHandler;
   wc.lpszClassName = lpszClassName;
   wc.lpszMenuName = MAKEINTRESOURCE(IDR_CHYLDFTPMENU);
   wc.style = 0;

   if(!RegisterClass(&wc))
      return FALSE;
   return TRUE;
}

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */

/*
HWND CreateWindowEx(
  DWORD dwExStyle,      // extended window style
  LPCTSTR lpClassName,  // registered class name
  LPCTSTR lpWindowName, // window name
  DWORD dwStyle,        // window style
  int x,                // horizontal position of window
  int y,                // vertical position of window
  int nWidth,           // window width
  int nHeight,          // window height
  HWND hWndParent,      // handle to parent or owner window
  HMENU hMenu,          // menu handle or child identifier
  HINSTANCE hInstance,  // Windows NT/2000/XP: This value is ignored.
  LPVOID lpParam        // window-creation data
);
*/

bool CreateMotherWindow(void) {

   hgWnd = CreateWindowEx(WS_EX_CLIENTEDGE,
                          lpszClassName,
                          lpszWinTitle,
                          WS_OVERLAPPEDWINDOW,
                          CW_USEDEFAULT,
                          CW_USEDEFAULT,
                          CW_USEDEFAULT,
                          CW_USEDEFAULT,
                          NULL,
                          NULL,
                          hgInstance,
                          NULL);

   if(!(hgWnd))
      return FALSE;
   return TRUE;
}

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */

bool InitializeWinsock(int majver, int minver) {
   
   int wsaerr;
   WORD wVersionRequested;

   /*
   typedef struct WSAData {
      WORD                  wVersion;
      WORD                  wHighVersion;
      char                  szDescription[WSADESCRIPTION_LEN+1];
      char                  szSystemStatus[WSASYS_STATUS_LEN+1];
      unsigned short        iMaxSockets;
      unsigned short        iMaxUdpDg;
      char FAR *            lpVendorInfo;
   }WSADATA, *LPWSADATA; 
   */

   wVersionRequested = MAKEWORD(majver, minver);
   wsaerr = WSAStartup( wVersionRequested, &wsaData );

   if(wsaerr != 0)
      return FALSE;
   return TRUE;
}

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */

void DisplayWindow(int nShowCmd) {

   INITCOMMONCONTROLSEX commctrl;

   ShowWindow(hgWnd,nShowCmd);
   UpdateWindow(hgWnd);

   /* 
   Initializing Common Controls...
   */

   commctrl.dwICC = ICC_INTERNET_CLASSES ;   
   InitCommonControlsEx(&commctrl);
}


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */