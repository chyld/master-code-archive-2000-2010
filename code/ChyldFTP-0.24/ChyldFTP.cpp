/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :16-JUL-2001 @ 22:23
Program  :ChyldFTP for Windows XP v1.0
File     :ChyldFTP.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* GLOBALS */
HINSTANCE hgInstance;
HWND hgWnd;
WSADATA wsaData;

int APIENTRY WinMain(HINSTANCE hInstance, 
                     HINSTANCE hPrevInstance, 
                     LPSTR lpCmdLine, 
                     int nShowCmd) {
   MSG msg;
   hgInstance = hInstance;

   if(!RegisterWindowClass()) return 0;
   if(!CreateMotherWindow()) return 0;
   if(!InitializeWinsock(2,2)) return 0;


   DisplayWindow(nShowCmd);


   while(GetMessage(&msg, NULL, 0, 0)) {
      TranslateMessage(&msg); 
      DispatchMessage(&msg); 
   }
   return msg.wParam;
}


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */