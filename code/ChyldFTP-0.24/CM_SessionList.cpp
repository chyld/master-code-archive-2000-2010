/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :24-JUL-2001 @ 13:08
Program  :ChyldFTP for Windows XP v1.0
File     :CM_SessionList.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"

/* EXTERN */
extern HWND hgWnd;

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_SessionList::CM_SessionList(void) {

   pHead = pTail = NULL;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_SessionList::Insert(CM_Session * pSession) {

   CM_Session * pTemp = pHead;

   if(!pHead) {
      pHead = pTail = pSession;
   }
   else {
      while(pTemp) {
         if(pTemp->pNext)
            pTemp = pTemp->pNext;
         else {
            pTail = pSession;
            pTemp->pNext = pSession;
            pSession->pPrev = pTemp;
            break;
         }
      }
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_SessionList::Delete(unsigned int session_id) {

   CM_Session * pTemp;

   pTemp = Get(session_id);

   if(pHead != pTail) {
      if(pTemp == pHead) {
         (pTemp->pNext)->pPrev = NULL;
         pHead = pTemp->pNext;
      }
      else if(pTemp == pTail) {
         (pTemp->pPrev)->pNext = NULL;
         pTail = pTemp->pPrev;
      }
      else {
         (pTemp->pPrev)->pNext = pTemp->pNext;
         (pTemp->pNext)->pPrev = pTemp->pPrev;
      }
   }
   else {
      pHead = pTail = NULL;
   }
   delete pTemp;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_Session * CM_SessionList::Get(unsigned int session_id) {

   CM_Session * pTemp = pHead;
   unsigned int nTemp;

   while(pTemp) {
      nTemp = pTemp->GetSessionID();

      if(nTemp == session_id)
         return pTemp;
      pTemp = pTemp->pNext;
   }
   return NULL;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_Session * CM_SessionList::GetS(SOCKET socket_in) {

   SOCKET comS, datS;
   CM_Session * pTemp = pHead;

   while(pTemp) {
      comS = pTemp->GetSocket(CLIENT);
      datS = pTemp->GetSocket(SERVER);

      if((socket_in == comS) || (socket_in == datS))
         return pTemp;
      pTemp = pTemp->pNext;
   }
   return NULL;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_SessionList::~CM_SessionList(void) {
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
