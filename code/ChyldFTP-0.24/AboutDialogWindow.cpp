/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :17-JUL-2001 @ 09:37
Program  :ChyldFTP for Windows XP v1.0
File     :AboutDialogWindow.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* EXTERN */
extern HWND hgWnd;

LRESULT APIENTRY ChyldFTPAboutHandler(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam) {

   switch (uMsg) {
   case WM_INITDIALOG:
      return TRUE;

   case WM_COMMAND:
      if(LOWORD(wParam) == IDOK) {
         EndDialog(hDlg, LOWORD(wParam));
         return TRUE;
      }
      break;
   }
   return FALSE;
}


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
