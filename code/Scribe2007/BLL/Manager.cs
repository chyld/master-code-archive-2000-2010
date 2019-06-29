#region assembly references
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Chyld.Scribe.Common;
using Chyld.Scribe.DAL;
#endregion

namespace Chyld.Scribe.BLL
{
    public class Manager
    {
        #region fields
        private String m_ApplicationPath;
        private SqlServer m_SqlServer;
        #endregion

        #region constructors
        #region constructor :: public Manager(String p_ApplicationPath, String p_ConnectionString)
        public Manager(String p_ApplicationPath, String p_ConnectionString)
        {
            m_ApplicationPath = p_ApplicationPath;
            m_SqlServer = new SqlServer(p_ConnectionString);
        }
        #endregion
        #endregion

        #region methods
        #region method :: public void InsertNote(String p_Text, String p_Tags, List<String> p_Filenames)
        public void InsertNote(String p_Text, String p_Tags, List<String> p_Filenames)
        {
            // -> Checking for null or empty values
            if(String.IsNullOrEmpty(p_Text) || (p_Tags == null) || (p_Filenames == null))
                throw new NullReferenceException("An argument to InsertNote was null.");

            // -> Regex string for validation
            String alphanumText = @"^[\x20-\x7d\x0a\x0d]*$";
            String alphanumTags = @"^[a-z0-9,]*$";

            // -> Test each character individually
            foreach(char c in p_Text)
            {
                if(!Regex.IsMatch(c.ToString(), alphanumText))
                    throw new ArgumentException("Invalid note text, character : " + c.ToString() + " value: " + Convert.ToInt64(c).ToString());
            }

            // -> Test the tag string
            if(!Regex.IsMatch(p_Tags, alphanumTags))
                throw new ArgumentException("Invalid tag characters.");

            // -> Creating the note to insert and the encryption engine
            NoteCT noteCT = new NoteCT();
            CipherEngine ce = new CipherEngine(noteCT);

            // -> Encrypting the note text
            noteCT.Text = ce.EncryptBytes(Encoding.UTF8.GetBytes(p_Text));

            // -> Encrypting each tag
            // -> Don't add an empty tag
            foreach(String tag in p_Tags.Split(','))
                if(!String.IsNullOrEmpty(tag.Trim()))
                    noteCT.Tags.Add(ce.EncryptBytes(Encoding.UTF8.GetBytes(tag)));

            // -> Encrypting each filename and its data
            foreach(String file in p_Filenames)
            {
                // -> Encrypting the filename
                Byte[] fileCT = ce.EncryptBytes(Encoding.UTF8.GetBytes(file));

                // -> Getting the full path to the input and output file
                String originalFile = m_ApplicationPath + "/Temp/" + file;
                String encryptedFile = m_ApplicationPath + "/Temp/" + file + ".enc";

                // -> Encrypting the file
                ce.EncryptFile(originalFile, encryptedFile);

                // -> Adding the encrypted filename and filedata
                noteCT.Files.Add(fileCT, File.ReadAllBytes(encryptedFile));

                // -> Deleting both files
                if(File.Exists(encryptedFile))
                    File.Delete(encryptedFile);
                if(File.Exists(originalFile))
                    File.Delete(originalFile);
            }

            // -> Inserting the note into the database
            m_SqlServer.InsertNote(noteCT);
        }
        #endregion

        #region method :: public List<NotePT> GetAllNotes()
        public List<NotePT> GetAllNotes()
        {
            return m_SqlServer.GetAllNotes();
        }
        #endregion

        #region method :: public NoteCT GetFile(Int64 p_NoteId, String p_Filename)
        public NoteCT GetFile(Int64 p_NoteId, String p_Filename)
        {
            return m_SqlServer.GetFile(p_NoteId, p_Filename);
        }
        #endregion

        #region method :: private Boolean IsPasswordAuthenticated(String p_PasswordA, String p_PasswordB, String p_PasswordC)
        public Boolean IsPasswordAuthenticated(String p_PasswordA, String p_PasswordB, String p_PasswordC)
        {
            // -> Get the time
            DateTime d = DateTime.Now;

            // -> Parse out the dayname, month, dayvalue, year component
            String dayOfWeek = d.DayOfWeek.ToString().Substring(0, 3).ToLower();
            String month = d.ToString("MMMM").Substring(0, 3).ToLower();
            String dayHexValue = Int32.Parse(d.ToString("dd")).ToString("X2").ToLower();
            String year = d.ToString("yyyy").Substring(2, 2).ToLower();

            // -> Concat the values
            String dynamicPassword = dayOfWeek + month + dayHexValue + year;

            // -> Check all passwords for validity
            if((p_PasswordA == "cmedford") && (p_PasswordB == "scribe") && (p_PasswordC == dynamicPassword))
                return true;
            return false;
        }
        #endregion

        #region method :: public void DeleteOldTempFiles()
        public void DeleteOldTempFiles()
        {
            // -> Get a list of temp files
            String[] filenames = Directory.GetFiles(m_ApplicationPath + "/Temp/");

            // -> Delete each file older than a day
            foreach(String filename in filenames)
            {
                TimeSpan timeSpan = DateTime.Now - File.GetCreationTime(filename);
                if(timeSpan.Days > 1)
                    File.Delete(filename);
            }
        }
        #endregion
        #endregion
    }
}
