using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class GroupEvent
{
    private string _EventId;
    private string _EventName;
    private DateTime _EventStartDate;
    private DateTime _EventEndDate;
    private string _GroupColor;
    private string _UserColor;

	public GroupEvent()
	{
	}

    public GroupEvent(string eid, string esd, string eed)
    {
        EventId = eid;
        EventStartDate = new DateTime();
        EventStartDate = DateTime.Parse(esd);
        EventEndDate = new DateTime();
        EventEndDate = DateTime.Parse(eed);
    }

    public GroupEvent(string eid, string en, string esd, string eed, string gc, string uc)
    {
        EventId = eid;
        EventName = en;
        EventStartDate = new DateTime();
        EventStartDate = DateTime.Parse(esd);
        EventEndDate = new DateTime();
        EventEndDate = DateTime.Parse(eed);
        GroupColor = gc;
        UserColor = uc;
    }

    public string EventId
    {
        get
        {
            return _EventId;
        }

        set
        {
            _EventId = value;
        }
    }
    
    public string EventName
    {
        get
        {
            return _EventName;
        }

        set
        {
            _EventName = value;
        }
    }
    
    public DateTime EventStartDate
    {
        get
        {
            return _EventStartDate;
        }

        set
        {
            _EventStartDate = value;
        }
    }
    
    public DateTime EventEndDate
    {
        get
        {
            return _EventEndDate;
        }

        set
        {
            _EventEndDate = value;
        }
    }
    
    public string GroupColor
    {
        get
        {
            return _GroupColor;
        }

        set
        {
            _GroupColor = value;
        }
    }

    public string UserColor
    {
        get
        {
            return _UserColor;
        }

        set
        {
            _UserColor = value;
        }
    }
}
