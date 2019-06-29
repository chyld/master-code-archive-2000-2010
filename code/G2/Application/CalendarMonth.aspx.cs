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

public partial class CalendarMonth : System.Web.UI.Page
{
    private SqlConnection connection;
    private SqlCommand command;
    private DateTime currentDate;
    private ArrayList newEvents;
    private ArrayList registeredEvents;

    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        connection.Open();

        currentDate = new DateTime();

        if ((Request.QueryString["m"] == null) || (Request.QueryString["y"] == null))
            currentDate = DateTime.Now;
        else
            currentDate = DateTime.Parse(Request.QueryString["m"] + "/1/" + Request.QueryString["y"]);
    }

    protected void GetNewEvents()
    {
        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));

        command.CommandText = "got_GetNewEvents";
        SqlDataReader reader = command.ExecuteReader();
        newEvents = new ArrayList();

        while (reader.Read())
        {
            newEvents.Add(new GroupEvent(reader["EventId"].ToString(), reader["EventStartDate"].ToString(), reader["EventEndDate"].ToString()));
        }
    }

    protected void GetRegisteredEvents()
    {
        command.Parameters.Clear();
        connection.Close();
        connection.Open();

        command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));

        command.CommandText = "got_GetRegisteredEvents";
        SqlDataReader reader = command.ExecuteReader();
        registeredEvents = new ArrayList();

        while (reader.Read())
        {
            registeredEvents.Add(new GroupEvent(reader["EventId"].ToString(),reader["EventName"].ToString(),reader["EventStartDate"].ToString(),reader["EventEndDate"].ToString(),reader["GroupColor"].ToString(),reader["UserColor"].ToString()));
        }
    }

    protected string DisplayNewEvents(int currentDay)
    {
        string neweventstring = "";
        foreach (GroupEvent ne in newEvents)
        {
            DateTime tdate = new DateTime(currentDate.Year, currentDate.Month, currentDay);
            DateTime sdate = new DateTime(ne.EventStartDate.Year, ne.EventStartDate.Month, ne.EventStartDate.Day);
            DateTime edate = new DateTime(ne.EventEndDate.Year, ne.EventEndDate.Month, ne.EventEndDate.Day);

            if ((tdate >= sdate) && (tdate <= edate))
                neweventstring += "<br /><a href='EventRSVP.aspx?id=" + ne.EventId + "'><img border='0' src='Images/NewEvent.gif' alt='Add New Event' /></a>";
        }
        return neweventstring;
    }

    protected string DisplayRegisteredEvents(int currentDay)
    {
        string registeredeventstring = "";

        foreach (GroupEvent re in registeredEvents)
        {
            DateTime tdate = new DateTime(currentDate.Year, currentDate.Month, currentDay);
            DateTime sdate = new DateTime(re.EventStartDate.Year, re.EventStartDate.Month, re.EventStartDate.Day);
            DateTime edate = new DateTime(re.EventEndDate.Year, re.EventEndDate.Month, re.EventEndDate.Day);

            if ((tdate >= sdate) && (tdate <= edate))
            {
                registeredeventstring += "<br /><span style='white-space:nowrap;'><img style='border:1px solid #000000; background-color:#" + re.GroupColor + ";' src='Images/TransparentPixel.gif' width='10' height='10' /><img style='border-top:1px solid #000000; border-right:1px solid #000000; border-bottom:1px solid #000000; background-color:#" + re.UserColor + ";' src='Images/TransparentPixel.gif' width='10' height='10' />&nbsp;<a class='aEventName' href='Event.aspx?id=" + re.EventId + "'>" + re.EventName + "</a></span>";
            }
        }
        return registeredeventstring;
    }

    protected string DisplayMonth()
    {
        string month = "";

        month += "<table class='tableMonth'>";
        month += "<tr>";
        month += "<td>" + MonthHeader() + "</td>";
        month += "</tr>";
        month += "<tr>";
        month += "<td>" + MonthBody() + "</td>";
        month += "</tr>";
        month += "</table>";

        return month;
    }

    protected string MonthHeader()
    {
        string header = "";

        header += "<table class='tableMonthHeader'><tr>";
        header += "<td align='left'><a href='CalendarMonth.aspx?m=" + currentDate.AddMonths(-1).ToString("MM") + "&y=" + currentDate.AddMonths(-1).ToString("yyyy") + "'><img border='0' src='Images/Back.png' alt=''></a></td>";
        header += "<td align='center'>" + currentDate.ToString("MMMM") + " :: " + currentDate.ToString("yyyy") + "</td>";
        header += "<td align='right'><a href='CalendarMonth.aspx?m=" + currentDate.AddMonths(+1).ToString("MM") + "&y=" + currentDate.AddMonths(+1).ToString("yyyy") + "'><img border='0' src='Images/Forward.png' alt=''></a></td>";
        header += "</tr></table>";

        return header;
    }

    protected string MonthBody()
    {
        string body = "";

        body += "<table>";
        body += "<tr>" + DayHeader() + "</tr>" + DayMain();
        body += "</table>";

        return body;
    }

    protected string DayHeader()
    {
        string dayheader = "";

        dayheader += "<td class='tdDayHeader'>Sunday</td>";
        dayheader += "<td class='tdDayHeader'>Monday</td>";
        dayheader += "<td class='tdDayHeader'>Tuesday</td>";
        dayheader += "<td class='tdDayHeader'>Wednesday</td>";
        dayheader += "<td class='tdDayHeader'>Thursday</td>";
        dayheader += "<td class='tdDayHeader'>Friday</td>";
        dayheader += "<td class='tdDayHeader'>Saturday</td>";

        return dayheader;
    }

    protected string DayMain()
    {
        int totaldays = 0;
        bool startcount = false;
        string mainstring = "";

        int daysinmonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        DateTime firstday = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0);
        int firstdayindex = (int)firstday.DayOfWeek;
        DateTime lastday = new DateTime(currentDate.Year, currentDate.Month, daysinmonth, 23, 59, 59);

        GetNewEvents();
        GetRegisteredEvents();

        while (totaldays < daysinmonth)
        {
            mainstring += "<tr>";
            for (int dayindex = 0; dayindex < 7; dayindex++)
            {
                if (dayindex == firstdayindex) startcount = true;
                if (totaldays >= daysinmonth) startcount = false;
                mainstring += "<td class='tdDay'>";
                if (startcount)
                {
                    mainstring += "<div class='divDayNumber'>" + (++totaldays).ToString() + "</div>";
                    mainstring += DisplayNewEvents(totaldays);
                    mainstring += DisplayRegisteredEvents(totaldays);
                }
                mainstring += "</td>";
            }
            mainstring += "</tr>";
        }

        return mainstring;
    }
}
