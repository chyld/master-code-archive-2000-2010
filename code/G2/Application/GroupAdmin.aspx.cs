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

public partial class GroupAdmin : System.Web.UI.Page
{
    private SqlConnection connection;
    private SqlCommand command;
    private SqlParameter returnStatus;
    private SqlParameter returnValue;

    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
    }

    protected void buttonGroupAdmin_Click(object sender, EventArgs e)
    {
        if ((dropdownlistGroupAdmin.SelectedValue == "Delete") || (dropdownlistGroupAdmin.SelectedValue == "Cancel"))
            DeleteEventFiles(Session["UserId"].ToString(), textboxGroupAdminGroupname.Text, dropdownlistGroupAdmin.SelectedValue);

        connection.Open();
        command.Parameters.Clear();

        returnStatus = new SqlParameter();
        returnValue = new SqlParameter();

        returnStatus.ParameterName = "@ReturnStatus";
        returnStatus.SqlDbType = SqlDbType.NVarChar;
        returnStatus.Size = 256;
        returnStatus.Direction = ParameterDirection.Output;

        returnValue.ParameterName = "@ReturnValue";
        returnValue.SqlDbType = SqlDbType.Int;
        returnValue.Direction = ParameterDirection.ReturnValue;

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@GroupName", textboxGroupAdminGroupname.Text));
        command.Parameters.Add(new SqlParameter("@GroupPassword", textboxGroupAdminPassword.Text));
        command.Parameters.Add(new SqlParameter("@GroupColor", "cccccc"));
        command.Parameters.Add(returnStatus);
        command.Parameters.Add(returnValue);

        command.CommandText = "got_" + dropdownlistGroupAdmin.SelectedValue + "Group";
        command.ExecuteNonQuery();
        labelGroupAdminStatus.Text = (string)command.Parameters["@ReturnStatus"].Value;
        labelGroupAdminStatus.Visible = true;

        connection.Close();
    }

    protected void buttonGroupColor_Click(object sender, EventArgs e)
    {
        connection.Open();
        command.Parameters.Clear();

        command.CommandText = "got_ChangeGroupColor";
        command.Parameters.Add(new SqlParameter("@GroupId", dropdownlistGroupColor.SelectedValue));
        command.Parameters.Add(new SqlParameter("@GroupColor", textboxGroupColor.Text.Substring(1)));
        command.ExecuteNonQuery();

        connection.Close();
    }

    protected void DeleteEventFiles(string UserId, string GroupName, string Mode)
    {
        connection.Open();
        command.Parameters.Clear();

        command.CommandText = "got_GetFilesToDelete";
        command.Parameters.Add(new SqlParameter("@UserId", UserId));
        command.Parameters.Add(new SqlParameter("@GroupName", GroupName));
        command.Parameters.Add(new SqlParameter("@Mode", Mode));
        SqlDataReader reader = command.ExecuteReader();
        string filename;

        while (reader.Read())
        {
            filename = ConfigurationManager.AppSettings["fileuploadpath"].ToString() + "{" + reader["FileId"].ToString() + "}-" + reader["FileName"].ToString();
            System.IO.File.Delete(filename);
        }

        connection.Close();
    }

    protected void buttonCancelMembership_Click(object o, EventArgs e)
    {
        dropdownlistGroupAdmin.SelectedValue = "Cancel";
        textboxGroupAdminGroupname.Text = ((System.Web.UI.WebControls.Button)o).CommandArgument.ToString();
        buttonGroupAdmin_Click(o, e);
    }

    protected string GetGroupOwnerImage(bool IsOwner)
    {
        if (IsOwner)
            return "Yes.gif";
        else
            return "No.gif";
    }
}
