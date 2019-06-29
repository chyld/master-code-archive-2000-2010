#region assembly references
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;
using Chyld.Scribe.Common;
using Chyld.Scribe.BLL;
#endregion

namespace Chyld.Scribe.Scribe
{
    public partial class DefaultPage : System.Web.UI.Page
    {
        #region fields
        private Manager m_Manager;
        private String m_StatusMessage;
        private String m_ConnectionString;
        #endregion

        #region events
        #region event :: protected void Page_Init(object p_Sender, EventArgs p_EventArgs)
        protected void Page_Init(object p_Sender, EventArgs p_EventArgs)
        {
            m_StatusMessage = String.Empty;

            try
            {
                // -> If "auth" is null, set it to false
                Session["authenticated"] = Session["authenticated"] ?? false;

                // -> Check authentication status
                if(!(Boolean)Session["authenticated"])
                    Response.Redirect("Dream.aspx");
            }
            catch(Exception ex)
            {
                m_StatusMessage += " :error: " + ex.Message;
            }
        }
        #endregion

        #region event :: protected void Page_Load(object p_Sender, EventArgs p_EventArgs)
        protected void Page_Load(object p_Sender, EventArgs p_EventArgs)
        {
            try
            {
                // -> Points to DiscountASP SQL 2005
                m_ConnectionString = @"Data Source=tcp:sql2k511.discountasp.net;Initial Catalog=SQL2005_403330_scribe;User ID=SQL2005_403330_scribe_user;Password=p2ssw0rd;";

                // -> Point to local copy of SQL 2005
                //m_ConnectionString = @"Data Source=Chyld-PC;Initial Catalog=Scribe;Integrated Security=SSPI;";

                // -> Manager object controls everything
                m_Manager = new Manager(Server.MapPath("."), m_ConnectionString);

                // -> Check if the client requested a file
                if((!String.IsNullOrEmpty(Request["id"])) && (!String.IsNullOrEmpty(Request["filename"])))
                    SendRequestedFileToClient(Int64.Parse(Request["id"]), Request["filename"]);

                // -> Check if the client wants to exit
                if(!String.IsNullOrEmpty(Request["action"]))
                    if(Int32.Parse(Request["action"]) == 0)
                        ExitSystem();
            }
            catch(Exception ex)
            {
                m_StatusMessage += " :error: " + ex.Message;
            }
        }
        #endregion

        #region event :: protected void Page_PreRender(object p_Sender, EventArgs p_EventArgs)
        protected void Page_PreRender(object p_Sender, EventArgs p_EventArgs)
        {
            try
            {
                // -> Getting a list of notes and reversing the order
                List<NotePT> listNotePT = m_Manager.GetAllNotes();
                listNotePT.Reverse();

                // -> Binding note list to grid
                gridviewNotes.DataSource = listNotePT;
                gridviewNotes.DataBind();

                // -> Clean up page
                Clean();
            }
            catch(Exception ex)
            {
                m_StatusMessage += " :error: " + ex.Message;
            }
            finally
            {
                labelApplicationStatus.Text = m_StatusMessage;
            }
        }
        #endregion

        #region event :: protected void buttonSubmit_Click(object p_Sender, EventArgs p_EventArgs)
        protected void buttonSubmit_Click(object p_Sender, EventArgs p_EventArgs)
        {
            try
            {
                List<String> filenames = new List<String>();
                for(Int32 count = 1; count <= 100; count++)
                {
                    // -> Looping through a possible 100 fileupload controls
                    FileUpload fileupload = (FileUpload)this.FindControl("tabcontainerScribeInput$tabpanelFiles$fileupload" + count.ToString("D3"));

                    // -> If one is found, check to see if it has a file
                    if(fileupload != null)
                        if(fileupload.HasFile)
                        {
                            // -> Save the file to disk for further encryption
                            String fullFilePath = Server.MapPath(".") + "/Temp/" + fileupload.FileName;
                            fileupload.SaveAs(fullFilePath);
                            filenames.Add(fileupload.FileName);
                        }
                }

                // -> Insert the note into the database
                m_Manager.InsertNote(textboxNoteData.Text, textboxNoteTags.Text, filenames);
            }
            catch(Exception ex)
            {
                m_StatusMessage += " :error: " + ex.Message;
            }
        }
        #endregion

        #region event :: protected void gridviewNotes_RowDataBoundEvent(object p_Sender, GridViewRowEventArgs p_GridViewRowEventArgs)
        protected void gridviewNotes_RowDataBoundEvent(object p_Sender, GridViewRowEventArgs p_GridViewRowEventArgs)
        {
            try
            {
                // -> As each row in the grid is rendered, the RowDataBoundEvent is fired
                if(p_GridViewRowEventArgs.Row.RowType == DataControlRowType.DataRow)
                    p_GridViewRowEventArgs.Row.Cells[0].Text = FormatNotesOutput((NotePT)p_GridViewRowEventArgs.Row.DataItem);
            }
            catch(Exception ex)
            {
                m_StatusMessage += " :error: " + ex.Message;
            }
        }
        #endregion
        #endregion

        #region methods
        #region method :: private String FormatNotesOutput(NotePT p_NotePT)
        private String FormatNotesOutput(NotePT p_NotePT)
        {
            String output = String.Empty;

            output += "<div class=\"note\">";
            output += "<div class=\"date\">" + DisplayExitButton() + DisplayDateSection(p_NotePT) + "</div>";
            output += "<div class=\"data\">" + p_NotePT.Text.Replace("\n", "<br />") + "</div>";
            output += "<div class=\"tags\">" + DisplayTags(p_NotePT) + "</div>";
            output += "<div class=\"files\">" + DisplayFiles(p_NotePT) + "</div>";
            output += "</div>";

            return output;
        }
        #endregion

        #region method :: private String DisplayExitButton()
        private String DisplayExitButton()
        {
            return "<a href=\"Default.aspx?action=0\"><img src=\"Images/lime.png\" /></a>";
        }
        #endregion

        #region method :: private String DisplayDateSection(NotePT p_NotePT)
        private String DisplayDateSection(NotePT p_NotePT)
        {
            return p_NotePT.DateCreated.ToString();
        }
        #endregion

        #region method :: private String DisplayTags(NotePT p_NotePT)
        private String DisplayTags(NotePT p_NotePT)
        {
            String tags = String.Empty;

            foreach(String tag in p_NotePT.Tags)
                tags += "<div class=\"tag\">tag :: " + tag + " ::</div>";

            return tags;
        }
        #endregion

        #region method :: private String DisplayFiles(NotePT p_NotePT)
        private String DisplayFiles(NotePT p_NotePT)
        {
            String files = String.Empty;

            foreach(String file in p_NotePT.Filenames)
                files += "<div class=\"file\">file :: <a href=\"Default.aspx?id=" + p_NotePT.NoteId.ToString() + "&filename=" + file + "\">" + file + "</a> ::</file>";

            return files;
        }
        #endregion

        #region method :: private void SendRequestedFileToClient(Int64 p_NoteId, String p_Filename)
        private void SendRequestedFileToClient(Int64 p_NoteId, String p_Filename)
        {
            // -> Get input and output full file names
            String path = Server.MapPath(".") + "/Temp/";
            String inputFile = path + p_Filename + ".enc";
            String outputFile = path + p_Filename;

            // -> Get the encrypted file
            NoteCT noteCT = m_Manager.GetFile(p_NoteId, p_Filename);
            CipherEngine ce = new CipherEngine(noteCT);

            // -> Write the file to disk
            foreach(Byte[] filenameCT in noteCT.Files.Keys)
                File.WriteAllBytes(inputFile, noteCT.Files[filenameCT]);

            // -> Decrypt the file
            ce.DecryptFile(inputFile, outputFile);

            // -> Read the decrypted file into a byte array
            Byte[] binarydata = File.ReadAllBytes(outputFile);

            // -> Send the byte array out to the client
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + p_Filename);
            Response.ContentType = "application/octet-stream";
            Response.OutputStream.Write(binarydata, 0, binarydata.Length);
            Response.End();
        }
        #endregion

        #region method :: private void Clean()
        private void Clean()
        {
            textboxNoteData.Text = String.Empty;
            textboxNoteTags.Text = String.Empty;

            m_Manager.DeleteOldTempFiles();
        }
        #endregion

        #region method :: private void ExitSystem()
        private void ExitSystem()
        {
            Session["authenticated"] = false;
            Response.Redirect(ConfigurationManager.AppSettings["ExitLocation"]);
        }
        #endregion
        #endregion
    }
}
