/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :24-JUL-2001 @ 19:49
Program  :ChyldFTP for Windows XP v1.0
File     :CM_State.h
 * -------------------------------------------------------------------------- */


#ifndef CM_STATE_H_INCLUDED
#define CM_STATE_H_INCLUDED

#include "classheader.h"

class CM_State {
public:
   CM_State(void);
   unsigned int GetState(void);
   unsigned int GetRetry(void);
   bool GetSocketState(void);
   void SetState(unsigned int new_state);
   void SetRetry(unsigned int new_retry);
   void SetSocketState(bool socket_conn);
private:
   unsigned int current_state;
   unsigned int retries_avail;
   bool socket_connected;
};

#endif


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
