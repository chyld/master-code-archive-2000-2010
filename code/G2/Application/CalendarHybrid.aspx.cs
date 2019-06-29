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

public partial class CalendarHybrid : System.Web.UI.Page
{
    private SqlConnection connection;
    private SqlCommand command;
    private DateTime currentDay;
    private ArrayList newEvents;
    private ArrayList registeredEvents;

    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;

        currentDay = new DateTime();

        if ((Request.QueryString["d"] == null) || (Request.QueryString["m"] == null) || (Request.QueryString["y"] == null))
            currentDay = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date;
        else
            currentDay = DateTime.Parse(Request.QueryString["m"] + "/" + Request.QueryString["d"] + "/" + Request.QueryString["y"]);

        GetNewEvents();
        GetRegisteredEvents();
    }

    protected string DisplayHybrid()
    {
        string hybridString = "";

        hybridString += "<table>";
        hybridString += "<tr><td align='left'>"+LT()+"</td><td></td><td></td><td></td><td align='right'>"+GT()+"</td></tr>";
        hybridString += "<tr><td class='tdWeekend'>"+DA()+"</td><td></td><td></td><td></td><td></td></tr>";
        hybridString += "<tr><td class='tdWeekday'>"+DA()+"</td><td class='tdWeekday'>"+DA()+"</td><td class='tdWeekday'>"+DA()+"</td><td class='tdWeekday'>"+DA()+"</td><td class='tdWeekday'>"+DA()+"</td></tr>";
        hybridString += "<tr><td></td><td></td><td></td><td></td><td class='tdWeekend'>"+DA()+"</td></tr>";
        hybridString += "</table>";

        return hybridString;
    }

    protected string LT()
    {
        return "<a href='CalendarHybrid.aspx?y=" + currentDay.AddDays(-7).Year + "&m=" + currentDay.AddDays(-7).Month + "&d=" + currentDay.AddDays(-7).Day + "'><img border='0' src='Images/Back.png' alt=''></a>";
    }
    
    protected string GT()
    {
        return "<a href='CalendarHybrid.aspx?y=" + currentDay.AddDays(+7).Year + "&m=" + currentDay.AddDays(+7).Month + "&d=" + currentDay.AddDays(+7).Day + "'><img border='0' src='Images/Forward.png' alt=''></a>";
    }

    protected string DA()
    {
        string DAstring = "";

        DAstring += "<div class='divHybridDate'><b>" + currentDay.DayOfWeek.ToString() + "</b><br />" + currentDay.ToString("MMMM dd, yyyy") + "</div>";
        DAstring += DisplayNewEvents();
        DAstring += DisplayRegisteredEvents();

        currentDay = currentDay.AddDays(1);
        return DAstring;
    }

    protected void GetNewEvents()
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));

        command.CommandText = "got_GetNewEvents";
        SqlDataReader reader = command.ExecuteReader();
        newEvents = new ArrayList();

        while (reader.Read())
        {
            newEvents.Add(new GroupEvent(reader["EventId"].ToString(), reader["EventStartDate"].ToString(), reader["EventEndDate"].ToString()));
        }

        connection.Close();
    }

    protected void GetRegisteredEvents()
    {
        connection.Open();
        command.Parameters.Clear();

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));

        command.CommandText = "got_GetRegisteredEvents";
        SqlDataReader reader = command.ExecuteReader();
        registeredEvents = new ArrayList();

        while (reader.Read())
        {
            registeredEvents.Add(new GroupEvent(reader["EventId"].ToString(), reader["EventName"].ToString(), reader["EventStartDate"].ToString(), reader["EventEndDate"].ToString(), reader["GroupColor"].ToString(), reader["UserColor"].ToString()));
        }

        connection.Close();
    }

    protected string DisplayNewEvents()
    {
        string neweventstring = "";
        foreach (GroupEvent ne in newEvents)
        {
            DateTime tdate = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day);
            DateTime sdate = new DateTime(ne.EventStartDate.Year, ne.EventStartDate.Month, ne.EventStartDate.Day);
            DateTime edate = new DateTime(ne.EventEndDate.Year, ne.EventEndDate.Month, ne.EventEndDate.Day);

            if ((tdate >= sdate) && (tdate <= edate))
                neweventstring += "<br /><a href='EventRSVP.aspx?id=" + ne.EventId + "'><img border='0' src='Images/NewEvent.gif' alt='Add New Event' /></a>";
        }
        return neweventstring;
    }

    protected string DisplayRegisteredEvents()
    {
        string registeredeventstring = "";
        foreach (GroupEvent re in registeredEvents)
        {
            DateTime tdate = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day);
            DateTime sdate = new DateTime(re.EventStartDate.Year, re.EventStartDate.Month, re.EventStartDate.Day);
            DateTime edate = new DateTime(re.EventEndDate.Year, re.EventEndDate.Month, re.EventEndDate.Day);

            if ((tdate >= sdate) && (tdate <= edate))
            {
                registeredeventstring += "<div style='border-right:2px dashed #" + re.UserColor + ";border-left:2px dashed #" + re.UserColor + ";border-top:2px dashed #" + re.GroupColor + ";border-bottom:2px dashed #" + re.GroupColor + ";' class='divHybridEvent'>";
                registeredeventstring += "<b>Event Date</b><br />";
                registeredeventstring += re.EventStartDate.ToString("MM/dd @ hh:mm tt");
                registeredeventstring += " - <br />";
                registeredeventstring += re.EventEndDate.ToString("MM/dd @ hh:mm tt");
                registeredeventstring += "<br /><br />";
                registeredeventstring += "<b>Event Name</b><br />";
                registeredeventstring += "<a class='link' href='Event.aspx?id=" + re.EventId + "'>";
                registeredeventstring += re.EventName;
                registeredeventstring += "</a>";
                registeredeventstring += "</div>";
            }
        }
        return registeredeventstring;
    }
}
