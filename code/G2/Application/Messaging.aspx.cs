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

public partial class Messaging : System.Web.UI.Page
{
    private SqlConnection connection;
    private SqlCommand command;

    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;

        if(!IsPostBack)
            PopulateNodes();
    }

    protected void buttonMessaging_Click(object sender, EventArgs e)
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@SendUserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@ReceiveUserName", dropdownlistMessaging.SelectedValue));
        command.Parameters.Add(new SqlParameter("@MessageText", textboxMessaging.Text));

        command.CommandText = "got_SendUserMessage";
        command.ExecuteNonQuery();

        textboxMessaging.Text = "";

        connection.Close();
        Response.Redirect("Messaging.aspx");
    }

    protected void PopulateNodes()
    {
        connection.Open();
        command.Parameters.Clear();

        string userread = "", usertemp = "";
        command.Parameters.Add(new SqlParameter("@ReceiveUserId", Session["UserId"].ToString()));
        command.CommandText = "got_GetReceivedMessages";
        SqlDataReader reader = command.ExecuteReader();

        TreeNode parent = new TreeNode();
        TreeNode child = new TreeNode();

        while (reader.Read())
        {
            userread = reader["SentUsername"].ToString();

            if (userread != usertemp)
            {
                parent = new TreeNode(userread);
                parent.Value = "";
                parent.Expanded = false;
                treeviewReceivedMessage.Nodes.Add(parent);
                child = new TreeNode(reader["MessageDate"].ToString());
                child.Value = reader["MessageId"].ToString();
                parent.ChildNodes.Add(child);
                usertemp = userread;
            }
            else
            {
                child = new TreeNode(reader["MessageDate"].ToString());
                child.Value = reader["MessageId"].ToString();
                parent.ChildNodes.Add(child);
            }
        }
        connection.Close();
    }

    protected void RefreshContent(object sender, EventArgs e)
    {
        Response.Redirect("Messaging.aspx");
    }
}
