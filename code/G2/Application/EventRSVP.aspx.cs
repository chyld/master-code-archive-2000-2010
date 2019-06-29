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

public partial class EventRSVP : System.Web.UI.Page
{
    private SqlConnection connection;
    private SqlCommand command;
    private string outbound;

    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        connection.Open();
    }
    
    protected void AcceptInvitation(object sender, EventArgs e)
    {
        outbound = "Event.aspx?id=" + Request.QueryString["id"].ToString();
        ReplyInvitation(1);
    }

    protected void DeclineInvitation(object sender, EventArgs e)
    {
        outbound = "CalendarMonth.aspx";
        ReplyInvitation(0);
    }

    protected void ReplyInvitation(int replystatus)
    {
        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@EventId", Request.QueryString["id"].ToString()));
        command.Parameters.Add(new SqlParameter("@IsAccepted", replystatus));

        command.CommandText = "got_ReplyInvitation";
        command.ExecuteNonQuery();

        Response.Redirect(outbound);
    }
}
