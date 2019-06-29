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

public partial class Contacts : System.Web.UI.Page
{
    private SqlConnection connection;
    private SqlCommand command;

    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        connection.Open();
        
        if(!IsPostBack)
            PopulateNodes();
    }

    protected void PopulateNodes()
    {
        string groupread = "", grouptemp = "";
        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"]));
        command.CommandText = "got_GetGroupDirectory";
        SqlDataReader reader = command.ExecuteReader();

        TreeNode parent = new TreeNode();
        TreeNode child = new TreeNode();

        while (reader.Read())
        {
            groupread = reader["GroupName"].ToString();

            if (groupread != grouptemp)
            {
                parent = new TreeNode(groupread);
                parent.Expanded = false;
                treeviewContacts.Nodes.Add(parent);
                child = new TreeNode(reader["UserName"].ToString());
                parent.ChildNodes.Add(child);
                grouptemp = groupread;
            }
            else
            {
                child = new TreeNode(reader["UserName"].ToString());
                parent.ChildNodes.Add(child);
            }
        }
    }

    protected string PrintEval(string DataValue)
    {
        string testvalue = "";

        try
        {
            testvalue = Eval(DataValue).ToString();
        }
        catch (Exception) { }

        return testvalue;
    }

    protected string FormatDate(string sqldate)
    {
        DateTime tempdate = new DateTime();
        tempdate = DateTime.Parse(sqldate);

        return String.Format("{0:dddd, MMMM dd, yyyy}", tempdate);
    }
}
