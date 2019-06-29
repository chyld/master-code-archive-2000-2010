/* -------------------------------------------------------------------------- *
Author   :Chyld Medford
Created  :17-Aug-2001 @ 11:01
Program  :ChyldFTP for Windows XP v1.0
File     :CM_Search.cpp
 * -------------------------------------------------------------------------- */


#include "chyldftp.h"
char cNewPath[10000];

/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
CM_Search::CM_Search(void) {

   CM_FileNode * pFN = new CM_FileNode;
   pFN->pFileName = "/\0";
   pFN->pDown = pFN->pRight = NULL;
   pFN->archive = 1;
   pFN->pathinc = 1;
   pFN->dloaded = 1;
   pRoot = pFN;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Search::ParseData(char * Buffer, char * currentDir) {

   char * pTmp, * pPos, *pMov;
   char filename[10000];
   int count, length;

   while(pTmp = strstr(Buffer,"\n")) {
      if((((int)Buffer[0]) >= 0x30) && (((int)Buffer[0]) <= 0x39)) {  
         //DOS SYSTEM
         pMov = Buffer;
         for(count = 0; count <= 2; count++) {
            pPos = strstr(pMov," ");
            while(pPos[0] == ' ') ++pPos;
            pMov = pPos;
         }
         for(count = (pTmp - pMov) / sizeof(char); count >= 0; count--) {
            if((((int)pMov[count]) != 0x0a) && 
               (((int)pMov[count]) != 0x0d) && 
               (((int)pMov[count]) != 0x20))
               break;
         }
         strncpy(filename,pMov,(count+1));
         filename[count+1] = NULL;
         if((strcmp(filename,".")) && (strcmp(filename,"..")))
            CreateNode(filename,currentDir);
      }
      if((((int)Buffer[0]) == 0x64) || (((int)Buffer[0]) == 0x2d)){
         //UNIX SYSTEM
         pMov = Buffer;
         for(count = 0; count < 7; count++) {
            pPos = strstr(pMov," ");
            while(pPos[0] == ' ') ++pPos;
            if((((pPos[0] >= 0x41) && (pPos[0] <= 0x5a)) || 
                ((pPos[0] >= 0x61) && (pPos[0] <= 0x7a))) && 
                (count == 4)) 
                --count;
            pMov = pPos;
         }
         for(count = (pTmp - pMov) / sizeof(char); count >= 0; count--) {
            if((((int)pMov[count]) != 0x0a) && 
               (((int)pMov[count]) != 0x0d) && 
               (((int)pMov[count]) != 0x20))
               break;
         }

         strncpy(filename,pMov,(count+1));
         filename[count+1] = NULL; 
         
         if(strstr(filename,"->")) {
            length = (strstr(filename,"->") - filename) / sizeof(char);
            filename[length] = NULL;
            while(filename[strlen(filename)-1] == 0x20) 
               filename[strlen(filename)-1] = NULL;
         }
         if((strcmp(filename,".")) && (strcmp(filename,"..")))
            CreateNode(filename,currentDir);
      }
   Buffer = ++pTmp;
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Search::CreateNode(char * filename, char * currentDir) {

   CM_FileNode * pFN = new CM_FileNode;
   pFN->pFileName = (char *)malloc((strlen(filename)+1)*sizeof(char));
   strcpy(pFN->pFileName,filename);
   pFN->pRight = pFN->pDown = NULL;
   pFN->archive = 0;
   pFN->pathinc = 0;
   pFN->dloaded = 0;

   InsertData(pFN,currentDir,pRoot);
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Search::InsertData(CM_FileNode * pFN, char * currentDir, CM_FileNode * pLocation) {

   char * pItem, *pDir;
   char cDir[10000];
   pDir = cDir;

   strcpy(cDir,currentDir);

   while(pItem = NextDirectory(pDir)) {
      pLocation = pLocation->pDown;
      while(strcmp(pLocation->pFileName,pItem)) pLocation = pLocation->pRight;
      UpdateDirectory(&pDir);
   }

   if(pLocation->pDown) {
      pLocation = pLocation->pDown;
      while(pLocation->pRight) pLocation = pLocation->pRight;
      pLocation->pRight = pFN;
   }
   else {
      pLocation->pDown = pFN;
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
char * CM_Search::NextDirectory(char * currentDir) {

   static char cSend[10000];
   char * pBegin, * pEnd, * pTemp;
   int count = 0;
   pTemp = currentDir;

   while(pTemp = strstr(pTemp,"/")) {count++; pTemp++;}

   if(((strlen(currentDir) == 1) && (currentDir[0] == 0x2f)) || (!count)) 
      return NULL;

   pTemp = currentDir;
   if(currentDir[0] == 0x2f) {
      pBegin = ++pTemp;
   }
   else {
      pBegin = strstr(pTemp,"/");
      pBegin++;
   }

   if(pEnd = strstr(pBegin,"/")) {
      pEnd--;
   }
   else {
      pEnd = ((currentDir) + (strlen(currentDir) - 1));
   }

   strncpy(cSend,pBegin,(pEnd-pBegin)+1);
   cSend[(pEnd-pBegin)+1] = NULL;
   return cSend;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Search::UpdateDirectory(char ** currentDir) {

   char * pTemp;

   if((*currentDir)[0] == 0x2f) {
      (*currentDir)++;
   }
   else {
      pTemp = strstr((*currentDir),"/");
      (*currentDir) = ++pTemp;
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
char * CM_Search::SearchData(void) {
   
   char * pTemp;
   found_file = 0;
   for(int i = 0; i < 10000; i++) cNewPath[i] = 0x1b;
   cNewPath[9998] = '\n';
   cNewPath[9999] = NULL;


   CMcat(cNewPath,GetNextFile(pRoot));
   pTemp = strstr(cNewPath,"\n");
   return ++pTemp;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
char * CM_Search::GetFile(char * findFile) {
   
   char * pTemp;
   found_file = 0;
   for(int i = 0; i < 10000; i++) cNewPath[i] = 0x1b;
   cNewPath[9998] = '\n';
   cNewPath[9999] = NULL;


   CMcat(cNewPath,RetrieveFile(findFile,pRoot));
   pTemp = strstr(cNewPath,"\n");
   return ++pTemp;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
char * CM_Search::GetNextFile(CM_FileNode * pPosition) {

   if(pPosition->archive) {
      if(pPosition->pDown  && !found_file)  {pPosition->pathinc = 1; CMcat(cNewPath,GetNextFile(pPosition->pDown)); }
      if(pPosition->pRight && !found_file)  {pPosition->pathinc = 0; CMcat(cNewPath,GetNextFile(pPosition->pRight));}
      if(found_file && pPosition->pathinc)
         return pPosition->pFileName;
      return NULL;
   }
   pPosition->archive = 1; found_file = 1;
   return pPosition->pFileName;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
char * CM_Search::RetrieveFile(char * findFile, CM_FileNode * pPosition) {

   if(!strstr(pPosition->pFileName,findFile) || (pPosition->dloaded) || (pPosition->pDown)) {
      if(pPosition->pDown  && !found_file)  {pPosition->pathinc = 1; CMcat(cNewPath,RetrieveFile(findFile,pPosition->pDown)); }
      if(pPosition->pRight && !found_file)  {pPosition->pathinc = 0; CMcat(cNewPath,RetrieveFile(findFile,pPosition->pRight));}
      if(found_file && pPosition->pathinc)
         return pPosition->pFileName;
      return NULL;
   }
   pPosition->dloaded = 1; found_file = 1;
   return pPosition->pFileName;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
void CM_Search::CMcat(char * store, char * data) {

   char * p;
   if(data) {
      if(strcmp(data,"/")) {
         p = (strstr(store,"\n"))-(strlen(data));
         strncpy(p+1,data,strlen(data));
         strncpy(p-1,"\n/",2);
      }
   }
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */
char * CM_Search::GetRealFileName(char * pFileName) {

   char * pB;
   char * pT = pFileName;

   while(strstr(pT,"/")) {pB = strstr(pT,"/"); ++pT;}

   return pT;
}
/* -------------------------------------------------------------------------- */
/* -------------------------------------------------------------------------- */


/* -------------------------------------------------------------------------- *
END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END OF FILE ** END
 * -------------------------------------------------------------------------- */
