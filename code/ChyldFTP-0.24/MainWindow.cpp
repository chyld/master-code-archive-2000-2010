/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :16-JUL-2001 @ 22:46
Program  :ChyldFTP for Windows XP v1.0
File     :MainWindow.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* EXTERN */
extern HINSTANCE hgInstance;

LRESULT APIENTRY MainWindowsMessageHandler(HWND hWnd, 
                                           UINT uMsg, 
                                           WPARAM wParam, 
                                           LPARAM lParam) {
   

   static CM_SessionList sl;
   static CM_UserInformation * pUI = NULL;

   /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||USER INFORMATION MESSAGE||*/
   static const UINT USER_INFORMATION_MSG = RegisterWindowMessage(LPSTR_USER_INFORMATION_MSG);
   if(uMsg == USER_INFORMATION_MSG) {
      if(pUI) delete pUI;
      pUI = (CM_UserInformation *)wParam;
   }

   /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||DESTROY SESSION OBJ MESSAGE||*/
   static const UINT DESTROY_SESSION_OBJ_MSG = RegisterWindowMessage(LPSTR_DESTROY_SESSN_OBJECT);
   if(uMsg == DESTROY_SESSION_OBJ_MSG) {
      sl.Delete((unsigned int)wParam);
   }

   /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||WSA ASYNC SELECT MESSAGE||*/
   static const UINT WSA_ASYNC_SELECT_MSG = RegisterWindowMessage(LPSTR_WSA_ASYNC_SELECT_MSG);
   if(uMsg == WSA_ASYNC_SELECT_MSG) {
      if(sl.GetS((SOCKET)wParam))
         (sl.GetS((SOCKET)wParam))->SocketMessageParser(lParam,wParam);
   }

   /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||REGULAR WINDOW MESSAGES||*/
   switch (uMsg) {
   case WM_COMMAND:
      switch(LOWORD(wParam)) {
      case ID_MAIN_EXITCHYLDFTP:
         PostMessage(hWnd, WM_CLOSE, 0, 0);
         break;
      case ID_CHYLDFTP_USERINFORMATION:
         DialogBox(hgInstance, (LPCTSTR)IDD_CHYLDFTPDIALOG, hWnd, (DLGPROC)ChyldFTPDialogHandler);
         break;
      case ID_CHYLDFTP_CONNECTTOFTPSERVER:
         ConnectToFTPServer(pUI,&sl);
         break;
      case ID_ABOUT_CHYLDMEDFORD:
         DialogBox(hgInstance, (LPCTSTR)IDD_ABOUTCHYLDFTP, hWnd, (DLGPROC)ChyldFTPAboutHandler);
         break;
      }
      break;
   case WM_RBUTTONDOWN:
      break;
   case WM_LBUTTONDOWN:
      break;
   case WM_CREATE:
      break;
   case WM_TIMER:
      if(sl.Get((unsigned int)(wParam & 0xFFFFFFFE)))
         (sl.Get((unsigned int)(wParam & 0xFFFFFFFE)))->ReConnectSocket();
      if(sl.Get((unsigned int)(wParam & 0xFFFFFFFD)))
         (sl.Get((unsigned int)(wParam & 0xFFFFFFFD)))->SearchForFile();
      if(sl.Get((unsigned int)(wParam & 0xFFFFFFFB)))
         (sl.Get((unsigned int)(wParam & 0xFFFFFFFB)))->CheckIfRunning();
      break;
   case WM_CLOSE:
      DestroyWindow(hWnd);
      break;
   case WM_DESTROY:
      WSACleanup();
      PostQuitMessage(0);
      break;
   default: 
      return DefWindowProc(hWnd, uMsg, wParam, lParam); 
   } 
   return NULL; 
}


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
