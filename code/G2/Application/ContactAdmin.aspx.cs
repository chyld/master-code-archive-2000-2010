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

public partial class ContactAdmin : System.Web.UI.Page
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
    }

    protected void buttonAddPermission_Click(object sender, EventArgs e)
    {
        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
        command.Parameters.Add(new SqlParameter("@ShareField", dropdownlistField.SelectedValue));
        command.Parameters.Add(new SqlParameter("@TargetMode", dropdownlistMode.SelectedValue));
        command.Parameters.Add(new SqlParameter("@TargetName", textboxTarget.Text));

        command.CommandText = "got_AddPermission";
        command.ExecuteNonQuery();
    }
}
