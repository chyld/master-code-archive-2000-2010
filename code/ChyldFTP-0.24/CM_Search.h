/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :17-Aug-2001 @ 10:57
Program  :ChyldFTP for Windows XP v1.0
File     :CM_Search.h
 * -------------------------------------------------------------------------- */


#ifndef CM_SEARCH_H_INCLUDED
#define CM_SEARCH_H_INCLUDED

#include "classheader.h"

class CM_FileNode {
public:
   friend class CM_Search;
private:
   char * pFileName;
   int    archive;
   int    pathinc;
   int    dloaded;

   CM_FileNode * pRight;
   CM_FileNode * pDown;
};

/* --- */

class CM_Search {
public:
   CM_Search(void);
   void ParseData(char * Buffer, char * currentDir);
   void CreateNode(char * filename, char * currentDir);
   void InsertData(CM_FileNode * pFN, char * currentDir, CM_FileNode * pLocation);
   char * NextDirectory(char * currentDir);
   void UpdateDirectory(char ** currentDir);
   char * SearchData(void);
   char * GetNextFile(CM_FileNode * pPosition);
   void CMcat(char * store, char * data);
   char * GetFile(char * findFile);
   char * RetrieveFile(char * findFile, CM_FileNode * pPosition);
   char * GetRealFileName(char * pFileName);

private:
   int           found_file;
   CM_FileNode * pRoot;
};

#endif


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
