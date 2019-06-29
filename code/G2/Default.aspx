<%@ Page Language="C#" %>

<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection connection;
        SqlCommand command;

        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        connection.Open();

        command.Parameters.Add(new SqlParameter("@UserName", Membership.GetUser().ToString()));
        command.CommandText = "got_SetupUserData";
        SqlDataReader reader = command.ExecuteReader();

        reader.Read();

        Session.Add("UserName", Membership.GetUser().ToString());
        Session.Add("UserId", reader["UserId"].ToString());

        Response.Redirect("Application/CalendarMonth.aspx"); 
    }

</script>
