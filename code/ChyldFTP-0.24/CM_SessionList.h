/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :24-JUL-2001 @ 13:05
Program  :ChyldFTP for Windows XP v1.0
File     :CM_SessionList.h
 * -------------------------------------------------------------------------- */


#ifndef CM_SESSIONLIST_H_INCLUDED
#define CM_SESSIONLIST_H_INCLUDED

#include "classheader.h"

class CM_SessionList {
public:
   CM_SessionList(void);
   void Insert(CM_Session * pSession);
   void Delete(unsigned int session_id);
   CM_Session * Get(unsigned int session_id);
   CM_Session * GetS(SOCKET socket_in);
  ~CM_SessionList(void);
private:
   CM_Session * pHead, * pTail;
};

#endif


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
