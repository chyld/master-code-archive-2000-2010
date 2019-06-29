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

public partial class CreateEvent : System.Web.UI.Page
{
    private SqlConnection connection;
    private SqlCommand command;
    private DateTime startdatetime;
    private DateTime enddatetime;

    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        connection.Open();
    }

    protected void buttonCreateEvent_Click(object sender, EventArgs e)
    {
        if (IsDateValid())
        {
            command.Parameters.Add(new SqlParameter("@EventName", textboxEventName.Text));
            command.Parameters.Add(new SqlParameter("@EventLocation", textboxEventLocation.Text));
            command.Parameters.Add(new SqlParameter("@EventStartDate", startdatetime));
            command.Parameters.Add(new SqlParameter("@EventEndDate", enddatetime));
            command.Parameters.Add(new SqlParameter("@UserId", Session["UserId"].ToString()));
            command.Parameters.Add(new SqlParameter("@GroupId", dropdownlistGroupName.SelectedValue));

            command.CommandText = "got_CreateEvent";
            command.ExecuteNonQuery();

            Response.Redirect("CreateEvent.aspx");
        }
    }

    protected bool IsDateValid()
    {
        DateTime starttime = new DateTime();
        DateTime endtime = new DateTime();

        try
        {    
            starttime = DateTime.Parse(textboxStartTime.Text);
        }
        catch (Exception) 
        {
            requiredfieldvalidatorStartTime.ErrorMessage = "Invalid Start Time";
            requiredfieldvalidatorStartTime.IsValid = false;
            return false;
        }

        try
        {   
            endtime = DateTime.Parse(textboxEndTime.Text);
        }
        catch (Exception)
        {
            requiredfieldvalidatorEndTime.ErrorMessage = "Invalid End Time";
            requiredfieldvalidatorEndTime.IsValid = false;
            return false;
        }

        if (calendarStartDate.SelectedDate == DateTime.Parse("1/1/0001 12:00:00 AM"))
        {
            customvalidatorStartDate.ErrorMessage = "Pick a Start Date";
            customvalidatorStartDate.IsValid = false;
            return false;
        }

        if (calendarEndDate.SelectedDate == DateTime.Parse("1/1/0001 12:00:00 AM"))
        {
            customvalidatorEndDate.ErrorMessage = "Pick an End Date";
            customvalidatorEndDate.IsValid = false;
            return false;
        }

        if (calendarStartDate.SelectedDate > calendarEndDate.SelectedDate)
        {
            customvalidatorStartDate.ErrorMessage = "Start Date <= End Date";
            customvalidatorEndDate.ErrorMessage = "End Date >= Start Date";
            customvalidatorStartDate.IsValid = false;
            customvalidatorEndDate.IsValid = false;
            return false;
        }

        startdatetime = new DateTime(calendarStartDate.SelectedDate.Year, calendarStartDate.SelectedDate.Month, calendarStartDate.SelectedDate.Day, starttime.Hour, starttime.Minute, starttime.Second);
        enddatetime = new DateTime(calendarEndDate.SelectedDate.Year, calendarEndDate.SelectedDate.Month, calendarEndDate.SelectedDate.Day, endtime.Hour, endtime.Minute, endtime.Second);

        if (startdatetime > enddatetime)
        {
            customvalidatorCreateEvent.ErrorMessage = "Check Start Time < End Time";
            customvalidatorCreateEvent.IsValid = false;
            return false;
        }

        return true;
    }
}
