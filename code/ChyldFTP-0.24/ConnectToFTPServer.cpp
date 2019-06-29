/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :18-JUL-2001 @ 15:56
Program  :ChyldFTP for Windows XP v1.0
File     :ConnectToFTPServer.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

void ConnectToFTPServer(CM_UserInformation * pUI, CM_SessionList * pSL) {

   CM_Session * ftp = new CM_Session;
   ftp->InitializeSession(pUI);

   pSL->Insert(ftp);
}


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
