using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Event : System.Web.UI.Page
{
    private SqlConnection connection;
    private SqlCommand command;

    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;

        ValidateUser();
    }

    protected void ValidateUser()
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@EventId", Request.QueryString["id"].ToString()));

        command.CommandText = "got_IsEventViewAuthorized";
        SqlDataReader reader = command.ExecuteReader();

        reader.Read();
        if (int.Parse(reader["IsAuthorized"].ToString()) == 0)
        {
            Response.Redirect("CalendarMonth.aspx");
        }
        connection.Close();
    }

    protected string GetStatusImage(int status)
    {
        string image = "";

        switch (status)
        {
            case 0:
                image = "Red.gif";
                break;
            case 1:
                image = "Green.gif";
                break;
            case 2:
                image = "Yellow.gif";
                break;
        }

        return image;
    }

    protected string GetActionCompletedImage(bool iscompleted)
    {
        string image = "";

        if (iscompleted)
            image = "Yes.gif";
        else
            image = "No.gif";

        return image;
    }

    protected bool IsEventOwner()
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@EventId", Request.QueryString["id"].ToString()));

        command.CommandText = "got_IsEventOwner";
        SqlDataReader reader = command.ExecuteReader();

        bool status;
        reader.Read();
        if (int.Parse(reader["IsEventOwner"].ToString()) == 0)
            status = false;
        else
            status = true;

        connection.Close();
        return status;
    }

    protected bool IsActionItemOwner(string ActionItemId)
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@ActionItemId", ActionItemId));

        command.CommandText = "got_IsActionItemOwner";
        SqlDataReader reader = command.ExecuteReader();

        bool status;
        reader.Read();
        if (int.Parse(reader["IsActionItemOwner"].ToString()) == 0)
            status = false;
        else
            status = true;

        connection.Close();
        return status;
    }

    protected bool IsNoteOwner(string NoteId)
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@NoteId", NoteId));

        command.CommandText = "got_IsNoteOwner";
        SqlDataReader reader = command.ExecuteReader();

        bool status;
        reader.Read();
        if (int.Parse(reader["IsNoteOwner"].ToString()) == 0)
            status = false;
        else
            status = true;

        connection.Close();
        return status;
    }

    protected bool IsFileOwner(string FileId)
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@FileId", FileId));

        command.CommandText = "got_IsFileOwner";
        SqlDataReader reader = command.ExecuteReader();

        bool status;
        reader.Read();
        if (int.Parse(reader["IsFileOwner"].ToString()) == 0)
            status = false;
        else
            status = true;

        connection.Close();
        return status;
    }

    protected void buttonInsertActionItem_Click(object sender, EventArgs e)
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@EventId", Request.QueryString["id"].ToString()));
        command.Parameters.Add(new SqlParameter("@ActionItemName", textboxNewActionItem.Text));
        command.Parameters.Add(new SqlParameter("@UserId", dropdownlistActionItemAssignedTo.SelectedValue));

        command.CommandText = "got_InsertActionItems";
        command.ExecuteNonQuery();

        textboxNewActionItem.Text = "";

        connection.Close();
    }

    protected void buttonNotes_Click(object sender, EventArgs e)
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@EventId", Request.QueryString["id"].ToString()));
        command.Parameters.Add(new SqlParameter("@NoteText", textboxNotes.Text));

        command.CommandText = "got_InsertEventNotes";
        command.ExecuteNonQuery();

        textboxNotes.Text = "";

        connection.Close();
    }

    protected void buttonEventFile_Click(object sender, EventArgs e)
    {
        if (fileuploadEventFile.HasFile)
        {
            string FileId = System.Guid.NewGuid().ToString();

            fileuploadEventFile.SaveAs(ConfigurationManager.AppSettings["fileuploadpath"].ToString() + "{" + FileId + "}-" + fileuploadEventFile.FileName);
            labelEventFile.Text = "Success! File Uploaded!";

            connection.Open();
            command.Parameters.Clear();

            command.Parameters.Add(new SqlParameter("@FileId", FileId));
            command.Parameters.Add(new SqlParameter("@FileName", fileuploadEventFile.FileName));
            command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
            command.Parameters.Add(new SqlParameter("@EventId", Request.QueryString["id"].ToString()));

            command.CommandText = "got_InsertEventFiles";
            command.ExecuteNonQuery();

            connection.Close();
        }
        else
        {
            labelEventFile.Text = "Sorry, no file was provided.";
        }
    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        string deletefile = ConfigurationManager.AppSettings["fileuploadpath"].ToString() + ((System.Web.UI.WebControls.Button)sender).CommandArgument.ToString();
        System.IO.File.Delete(deletefile);

        string fileid = ((System.Web.UI.WebControls.Button)sender).CommandArgument.ToString().Substring(1, 36);
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@FileId", fileid));

        command.CommandText = "got_DeleteEventFiles";
        command.ExecuteNonQuery();

        connection.Close();
    }

    protected void buttonDeleteEvent_Click(object sender, EventArgs e)
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@EventId", Request.QueryString["id"].ToString()));

        command.CommandText = "got_GetEventFiles";
        SqlDataReader reader = command.ExecuteReader();
        string filename;

        while (reader.Read())
        {
            filename = ConfigurationManager.AppSettings["fileuploadpath"].ToString() + "{" + reader["FileId"].ToString() + "}-" + reader["FileName"].ToString();
            System.IO.File.Delete(filename);
        }

        connection.Close();

        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@EventId", Request.QueryString["id"].ToString()));

        command.CommandText = "got_DeleteEvent";
        command.ExecuteNonQuery();

        connection.Close();

        Response.Redirect("CalendarMonth.aspx");
    }
}
