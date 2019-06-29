#region assembly references
using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Chyld.Scribe.Common;
#endregion

namespace Chyld.Scribe.DAL
{
    public class SqlServer
    {
        #region fields
        private SqlConnection m_SqlConnection;
        private SqlCommand m_SqlCommand;
        private SqlTransaction m_Transaction;
        #endregion

        #region constructors
        #region constructor :: public SqlServer(String p_ConnectionString)
        public SqlServer(String p_ConnectionString)
        {
            m_SqlConnection = new SqlConnection(p_ConnectionString);
        }
        #endregion
        #endregion

        #region methods
        #region method :: public void InsertNote(NoteCT p_NoteCT)
        public void InsertNote(NoteCT p_NoteCT)
        {
            try
            {
                // -> Open connection
                m_SqlConnection.Open();
                m_Transaction = m_SqlConnection.BeginTransaction();

                // -> Setup command
                m_SqlCommand = new SqlCommand();
                m_SqlCommand.CommandType = CommandType.StoredProcedure;
                m_SqlCommand.Connection = m_SqlConnection;
                m_SqlCommand.Transaction = m_Transaction;

                // -> Construct command that will insert data into the Note table
                m_SqlCommand.CommandText = "InsertNote";
                m_SqlCommand.Parameters.Clear();
                m_SqlCommand.Parameters.Add(new SqlParameter("@Text", SqlDbType.VarBinary, p_NoteCT.Text.Length));
                m_SqlCommand.Parameters["@Text"].Value = p_NoteCT.Text;
                Decimal noteId = (Decimal)m_SqlCommand.ExecuteScalar();

                // -> Construct command that will insert data into the Tags table
                m_SqlCommand.CommandText = "InsertTag";
                foreach(Byte[] tag in p_NoteCT.Tags)
                {
                    m_SqlCommand.Parameters.Clear();
                    m_SqlCommand.Parameters.Add(new SqlParameter("@NoteId", SqlDbType.BigInt));
                    m_SqlCommand.Parameters["@NoteId"].Value = noteId;
                    m_SqlCommand.Parameters.Add(new SqlParameter("@Tag", SqlDbType.VarBinary, tag.Length));
                    m_SqlCommand.Parameters["@Tag"].Value = tag;
                    m_SqlCommand.ExecuteNonQuery();
                }

                // -> Construct command that will insert data into the Files table
                m_SqlCommand.CommandText = "InsertFile";
                foreach(Byte[] key in p_NoteCT.Files.Keys)
                {
                    m_SqlCommand.Parameters.Clear();
                    m_SqlCommand.Parameters.Add(new SqlParameter("@NoteId", SqlDbType.BigInt));
                    m_SqlCommand.Parameters["@NoteId"].Value = noteId;
                    m_SqlCommand.Parameters.Add(new SqlParameter("@Filename", SqlDbType.VarBinary, key.Length));
                    m_SqlCommand.Parameters["@Filename"].Value = key;
                    m_SqlCommand.Parameters.Add(new SqlParameter("@Filedata", SqlDbType.VarBinary, p_NoteCT.Files[key].Length));
                    m_SqlCommand.Parameters["@Filedata"].Value = p_NoteCT.Files[key];
                    m_SqlCommand.ExecuteNonQuery();
                }

                // -> Construct command that will insert data into the Metadata table
                m_SqlCommand.CommandText = "InsertMetadata";
                foreach(String key in p_NoteCT.Metadata.Keys)
                {
                    m_SqlCommand.Parameters.Clear();
                    m_SqlCommand.Parameters.Add(new SqlParameter("@NoteId", SqlDbType.BigInt));
                    m_SqlCommand.Parameters["@NoteId"].Value = noteId;
                    m_SqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, key.Length));
                    m_SqlCommand.Parameters["@Name"].Value = key;
                    m_SqlCommand.Parameters.Add(new SqlParameter("@Value", SqlDbType.NVarChar, p_NoteCT.Metadata[key].Length));
                    m_SqlCommand.Parameters["@Value"].Value = p_NoteCT.Metadata[key];
                    m_SqlCommand.ExecuteNonQuery();
                }

                // -> If all passes, then commit the transaction
                m_Transaction.Commit();
            }
            catch(Exception ex)
            {
                // -> On error, rollback the transaction
                if(m_Transaction != null)
                    m_Transaction.Rollback();

                throw new Exception("Note was not inserted into database. " + ex.Message);
            }
            finally
            {
                // -> Close the connection
                if(m_SqlConnection != null)
                    m_SqlConnection.Close();
            }
        }
        #endregion

        #region method :: public List<NotePT> GetAllNotes()
        public List<NotePT> GetAllNotes()
        {
            List<NotePT> listNotePT = new List<NotePT>();

            try
            {
                // -> Open connection
                m_SqlConnection.Open();

                // -> Setup command
                m_SqlCommand = new SqlCommand();
                m_SqlCommand.CommandText = "GetAllNotes";
                m_SqlCommand.CommandType = CommandType.StoredProcedure;
                m_SqlCommand.Connection = m_SqlConnection;

                // -> Insert queried data into dataset
                DataSet ds = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(m_SqlCommand);
                dataAdapter.Fill(ds);
                dataAdapter.Dispose();

                // -> Rename the dataset and tables
                ds.DataSetName = "Scribe";
                ds.Tables[0].TableName = "Notes";
                ds.Tables[1].TableName = "Tags";
                ds.Tables[2].TableName = "Files";
                ds.Tables[3].TableName = "Metadata";

                // -> Add relations to dataset
                ds.Relations.Add("Notes-Tags", ds.Tables["Notes"].Columns["NoteId"], ds.Tables["Tags"].Columns["NoteId"]);
                ds.Relations.Add("Notes-Files", ds.Tables["Notes"].Columns["NoteId"], ds.Tables["Files"].Columns["NoteId"]);
                ds.Relations.Add("Notes-Metadata", ds.Tables["Notes"].Columns["NoteId"], ds.Tables["Metadata"].Columns["NoteId"]);

                // -> Constructing a list of NotePT objects
                foreach(DataRow rowNotes in ds.Tables["Notes"].Select())
                {
                    // -> Getting the encrypted data
                    NoteCT noteCT = new NoteCT();
                    noteCT.NoteId = (Int64)rowNotes["NoteId"];
                    noteCT.DateCreated = (DateTime)rowNotes["DateCreated"];
                    noteCT.Text = (Byte[])rowNotes["Text"];

                    foreach(DataRow rowTags in rowNotes.GetChildRows("Notes-Tags"))
                        noteCT.Tags.Add((Byte[])rowTags["Tag"]);

                    foreach(DataRow rowFiles in rowNotes.GetChildRows("Notes-Files"))
                        noteCT.Files.Add((Byte[])rowFiles["Filename"], null);

                    foreach(DataRow rowMetadata in rowNotes.GetChildRows("Notes-Metadata"))
                        noteCT.Metadata.Add((String)rowMetadata["Name"], (String)rowMetadata["Value"]);

                    // -> Used for encryption/decryption
                    CipherEngine ce = new CipherEngine(noteCT);

                    // -> Decrypting the data
                    NotePT notePT = new NotePT();
                    notePT.NoteId = noteCT.NoteId;
                    notePT.DateCreated = noteCT.DateCreated;
                    notePT.Text = Encoding.UTF8.GetString(ce.DecryptBytes(noteCT.Text));

                    foreach(Byte[] tagCT in noteCT.Tags)
                        notePT.Tags.Add(Encoding.UTF8.GetString(ce.DecryptBytes(tagCT)));

                    foreach(Byte[] keyCT in noteCT.Files.Keys)
                        notePT.Filenames.Add(Encoding.UTF8.GetString(ce.DecryptBytes(keyCT)));

                    // -> Add plain text object to list
                    listNotePT.Add(notePT);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error during retrieval of note list. " + ex.Message);
            }
            finally
            {
                // -> Close the connection
                if(m_SqlConnection != null)
                    m_SqlConnection.Close();
            }

            return listNotePT;
        }
        #endregion

        #region method :: public Byte[] GetFile(Int64 p_NoteId)
        public NoteCT GetFile(Int64 p_NoteId, String p_Filename)
        {
            NoteCT noteCT = new NoteCT();

            try
            {
                // -> Open connection
                m_SqlConnection.Open();

                // -> Setup command
                m_SqlCommand = new SqlCommand();
                m_SqlCommand.CommandType = CommandType.StoredProcedure;
                m_SqlCommand.Connection = m_SqlConnection;

                // -> Get encryption metadata and filename
                m_SqlCommand.CommandText = "GetAllNoteFilenames";
                m_SqlCommand.Parameters.Clear();
                m_SqlCommand.Parameters.Add(new SqlParameter("@NoteId", SqlDbType.BigInt));
                m_SqlCommand.Parameters["@NoteId"].Value = p_NoteId;

                // -> Fill the dataset
                DataSet ds = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(m_SqlCommand);
                dataAdapter.Fill(ds);
                dataAdapter.Dispose();

                // -> Holds the FileId and the encrypted filename for each file in a note
                Dictionary<Int64, Byte[]> encryptedFilenames = new Dictionary<Int64, Byte[]>();

                // -> Checking for redundant records in dataset
                // -> Getting encryption info for note and all files by note
                foreach(DataRow row in ds.Tables[0].Rows)
                {
                    if(!noteCT.Metadata.ContainsKey(row["Name"].ToString()))
                        noteCT.Metadata.Add(row["Name"].ToString(), row["Value"].ToString());

                    if(!encryptedFilenames.ContainsKey((Int64)row["FileId"]))
                        encryptedFilenames.Add((Int64)row["FileId"], (Byte[])row["Filename"]);
                }

                CipherEngine ce = new CipherEngine(noteCT);
                Int64 theFileId = 0;

                // -> Decrypting each file using the CipherEngine and returned metadata
                // -> If a decrypted file matches the parameter, then save its FileId
                foreach(Int64 fileId in encryptedFilenames.Keys)
                    if(Encoding.UTF8.GetString(ce.DecryptBytes(encryptedFilenames[fileId])) == p_Filename)
                        theFileId = fileId;

                // -> Now that we have the FileId, go and get the file
                // -> Do not decrypt it in memory, must use FileDecrypt
                m_SqlCommand.CommandText = "GetFile";
                m_SqlCommand.Parameters.Clear();
                m_SqlCommand.Parameters.Add(new SqlParameter("@FileId", SqlDbType.BigInt));
                m_SqlCommand.Parameters["@FileId"].Value = theFileId;

                // -> The file as a series of bytes
                Byte[] encryptedFiledata = (Byte[])m_SqlCommand.ExecuteScalar();

                // -> Add the encrypted filename and filedata to the NoteCT object
                noteCT.Files.Add(encryptedFilenames[theFileId], encryptedFiledata);
            }
            catch(Exception ex)
            {
                throw new Exception("Error during retrieval of file data. " + ex.Message);
            }
            finally
            {
                // -> Close the connection
                if(m_SqlConnection != null)
                    m_SqlConnection.Close();
            }

            return noteCT;
        }
        #endregion
        #endregion
    }
}
