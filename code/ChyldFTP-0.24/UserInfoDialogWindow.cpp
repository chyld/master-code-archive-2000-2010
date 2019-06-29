/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :16-JUL-2001 @ 22:23
Program  :ChyldFTP for Windows XP v1.0
File     :UserInfoDialogWindow.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* EXTERN */
extern HWND hgWnd;

LRESULT APIENTRY ChyldFTPDialogHandler(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam) {

   switch (uMsg) {
   case WM_INITDIALOG:
      return TRUE;

   case WM_COMMAND:
      if(LOWORD(wParam) == IDOK) {

         bool error = FALSE;

         static const UINT USER_INFORMATION_MSG = RegisterWindowMessage(LPSTR_USER_INFORMATION_MSG);
         CM_UserInformation * pUI = new CM_UserInformation;

         char cTemp[MAX_EDIT_CONTROL_LENGTH];

         GetDlgItemText(hDlg,IDC_HOSTNAME,cTemp,MAX_EDIT_CONTROL_LENGTH);
         if(strlen(cTemp) < MAX_NAME_LENGTH)
            pUI->SetHostname(cTemp);
         else {
            MessageBox(hDlg,"Hostname length exceeded!","ERROR",MB_OK | MB_ICONSTOP);
            error = TRUE;
         }

         //*****************************************************

         GetDlgItemText(hDlg,IDC_USERNAME,cTemp,MAX_EDIT_CONTROL_LENGTH);
         if(strlen(cTemp) < MAX_NAME_LENGTH)
            pUI->SetUsername(cTemp);
         else {
            MessageBox(hDlg,"Username length exceeded!","ERROR",MB_OK | MB_ICONSTOP);
            error = TRUE;
         }

         //*****************************************************

         GetDlgItemText(hDlg,IDC_PASSWORD,cTemp,MAX_EDIT_CONTROL_LENGTH);
         if(strlen(cTemp) < MAX_NAME_LENGTH)
            pUI->SetPassword(cTemp);
         else {
            MessageBox(hDlg,"Password length exceeded!","ERROR",MB_OK | MB_ICONSTOP);
            error = TRUE;
         }

         //*****************************************************

         GetDlgItemText(hDlg,IDC_MODE,cTemp,MAX_EDIT_CONTROL_LENGTH);
         if(strlen(cTemp) < MAX_NAME_LENGTH)
            pUI->SetMode(cTemp);
         else {
            MessageBox(hDlg,"Mode length exceeded!","ERROR",MB_OK | MB_ICONSTOP);
            error = TRUE;
         }

         //*****************************************************

         GetDlgItemText(hDlg,IDC_LOCALDIRECTORY,cTemp,MAX_EDIT_CONTROL_LENGTH);
         if(strlen(cTemp) < MAX_NAME_LENGTH)
            pUI->SetLocalDir(cTemp);
         else {
            MessageBox(hDlg,"Local Directory length exceeded!","ERROR",MB_OK | MB_ICONSTOP);
            error = TRUE;
         }

         //*****************************************************

         GetDlgItemText(hDlg,IDC_SEARCHSTRING,cTemp,MAX_EDIT_CONTROL_LENGTH);
         if(strlen(cTemp) < MAX_NAME_LENGTH)
            pUI->SetSearch(cTemp);
         else {
            MessageBox(hDlg,"Search String length exceeded!","ERROR",MB_OK | MB_ICONSTOP);
            error = TRUE;
         }

         //*****************************************************

         GetDlgItemText(hDlg,IDC_MAXRETRIES,cTemp,MAX_EDIT_CONTROL_LENGTH);
         if((atoi(cTemp) < 100000) && (strlen(cTemp) < 10))
            pUI->SetRetries(atoi(cTemp));
         else {
            MessageBox(hDlg,"Retry count exceeded!","ERROR",MB_OK | MB_ICONSTOP);
            error = TRUE;
         }

         //*****************************************************

         GetDlgItemText(hDlg,IDC_TIMEBETWEENRETRIES,cTemp,MAX_EDIT_CONTROL_LENGTH);
         if((atoi(cTemp) < 86400) && (strlen(cTemp) < 10))
            pUI->SetRetryDelay(atoi(cTemp));
         else {
            MessageBox(hDlg,"Retry Delay exceeded!","ERROR",MB_OK | MB_ICONSTOP);
            error = TRUE;
         }

         //*****************************************************

         GetDlgItemText(hDlg,IDC_PORTNUM,cTemp,MAX_EDIT_CONTROL_LENGTH);
         if((atoi(cTemp) < 65536) && (strlen(cTemp) < 10))
            pUI->SetPortNumber(atoi(cTemp));
         else {
            MessageBox(hDlg,"Port Number exceeded!","ERROR",MB_OK | MB_ICONSTOP);
            error = TRUE;
         }

         if(!error) {
            PostMessage(hgWnd,USER_INFORMATION_MSG,(WPARAM)pUI,0);
            EndDialog(hDlg, LOWORD(wParam));
         }
         return TRUE;
      }
      if(LOWORD(wParam) == IDCANCEL) {
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
