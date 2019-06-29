/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :26-JUL-2001 @ 10:19
Program  :ChyldFTP for Windows XP v1.0
File     :CM_FTPCommands.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_FTPCommands::Initialize(CM_UserInformation * pUI) {

   ui = (*pUI);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
int CM_FTPCommands::SendUsername(SOCKET s) {

   char cBuf[MAX_FTP_COMMAND_LENGTH];
   
   strcpy(cBuf,"USER ");
   strcpy((cBuf+strlen(cBuf)),ui.GetUsername());
   strcat(cBuf,"\r\n");
   
   return send(s,cBuf,strlen(cBuf),0);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
int CM_FTPCommands::SendPassword(SOCKET s) {

   char cBuf[MAX_FTP_COMMAND_LENGTH];

   strcpy(cBuf,"PASS ");
   strcpy((cBuf+strlen(cBuf)),ui.GetPassword());
   strcat(cBuf,"\r\n");

   return send(s,cBuf,strlen(cBuf),0);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */