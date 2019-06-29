namespace Chyld.Elysium.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Chyld.Elysium.Extension;

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public delegate void SaveDelegate(String text, DateTime date, Int32 id);

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public enum ViewType : byte
    {
        Week,
        BiWeek,
        Month,
        Year
    }

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public class DayBox
    {
        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public Label Label_Date { get; set; }
        public ImageButton Button_Edit { get; set; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public DayBox(Calendar calendar, String date)
        {
            Label_Date = new Label();
            Button_Edit = new ImageButton();

            Label_Date.Text = date;
            Button_Edit.ImageUrl = "~/Images/edit.png";
            Button_Edit.CommandArgument = date;
            Button_Edit.Click += calendar.EditMethod;

            calendar.Controls.Add(Label_Date);
            calendar.Controls.Add(Button_Edit);
        }
    }

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    [ToolboxData("<{0}:Calendar runat=server></{0}:Calendar>")]
    public class Calendar : CompositeControl
    {
        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private List<DayBox> m_DayBoxList = new List<DayBox>();
        private Int32 m_NumberOfWeeks;
        private DateTime m_TempDate;
        private readonly String m_DateFormat = "MMMM dd, yyyy";
        private Boolean m_IsEditMode = false;
        private ImageButton m_Button_Save = new ImageButton();
        private DropDownList m_DropDownList_SectionType = new DropDownList();
        private TextBox m_TextBox_EnteredData = new TextBox();

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public event SaveDelegate SaveEvent;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Object> CalendarDataSource { get; set; }
        public IEnumerable<Object> SectionTypeDataSource { get; set; }
        public IEnumerable<Object> MainContentDataSource { get; set; }

        public DateTime InputDate
        {
            get { return (ViewState["InputDate"] != null) ? ((DateTime)ViewState["InputDate"]).Date : DateTime.Now.Date; }
            set { ViewState["InputDate"] = value; }
        }

        public ViewType Display
        {
            get { return (ViewState["Display"] != null) ? (ViewType)ViewState["Display"] : ViewType.Week; }
            set { ViewState["Display"] = value; }
        }

        public Int32 SelectedSectionType
        {
            get { return m_DropDownList_SectionType.SelectedValue.ToInt32(); }
        }

        public DateTime SelectedEditDate
        {
            get { return m_Button_Save.CommandArgument.ToDateTime(); }
        }

        public ImageClickEventHandler EditMethod
        {
            get { return this.EditInfo; }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public void IncrementDate()
        {
            switch(Display)
            {
                case ViewType.Week:
                    InputDate = InputDate.AddDays(7);
                    break;
                case ViewType.BiWeek:
                    InputDate = InputDate.AddDays(7);
                    break;
                case ViewType.Month:
                    InputDate = InputDate.AddMonths(1);
                    break;
                case ViewType.Year:
                    InputDate = InputDate.AddYears(1);
                    break;
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public void DecrementDate()
        {
            switch(Display)
            {
                case ViewType.Week:
                    InputDate = InputDate.AddDays(-7);
                    break;
                case ViewType.BiWeek:
                    InputDate = InputDate.AddDays(-7);
                    break;
                case ViewType.Month:
                    InputDate = InputDate.AddMonths(-1);
                    break;
                case ViewType.Year:
                    InputDate = InputDate.AddYears(-1);
                    break;
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            m_DropDownList_SectionType.ID = "dropdownlistSectionId";
            m_DropDownList_SectionType.AutoPostBack = true;
            m_DropDownList_SectionType.SelectedIndexChanged += this.EditInfo;

            m_Button_Save.ID = "savebuttonId";
            m_Button_Save.ImageUrl = "~/Images/save.png";
            m_Button_Save.Click += this.SaveInfo;

            m_TextBox_EnteredData.ID = "textboxentryId";
            m_TextBox_EnteredData.TextMode = TextBoxMode.MultiLine;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected override void CreateChildControls()
        {
            Controls.Clear();
            m_DayBoxList.Clear();

            Controls.Add(m_Button_Save);
            Controls.Add(m_DropDownList_SectionType);
            Controls.Add(m_TextBox_EnteredData);

            switch (Display)
            {
                case ViewType.Week:
                    StartDate = InputDate.AddDays((Int32)InputDate.DayOfWeek * -1);
                    EndDate = StartDate.AddDays(6);
                    m_NumberOfWeeks = 1;

                    m_TempDate = StartDate;
                    for (Int32 count = 0; count < (m_NumberOfWeeks * 7); count++)
                    {
                        m_DayBoxList.Add(new DayBox(this, m_TempDate.ToString(m_DateFormat)));
                        m_TempDate = m_TempDate.AddDays(1);
                    }
                    break;
                case ViewType.BiWeek:
                    StartDate = InputDate.AddDays((Int32)InputDate.DayOfWeek * -1);
                    EndDate = StartDate.AddDays(13);
                    m_NumberOfWeeks = 2;

                    m_TempDate = StartDate;
                    for (Int32 count = 0; count < (m_NumberOfWeeks * 7); count++)
                    {
                        m_DayBoxList.Add(new DayBox(this, m_TempDate.ToString(m_DateFormat)));
                        m_TempDate = m_TempDate.AddDays(1);
                    }
                    break;
                case ViewType.Month:
                    StartDate = InputDate.AddDays((InputDate.Day - 1) * -1);
                    StartDate = StartDate.AddDays((Int32)StartDate.DayOfWeek * -1);
                    EndDate = StartDate.AddDays(DateTime.DaysInMonth(StartDate.Year, StartDate.Month) - 1);
                    EndDate = EndDate.AddDays(6 - ((Int32)EndDate.DayOfWeek));
                    m_NumberOfWeeks = (((EndDate - StartDate).Days + 1) / 7);

                    m_TempDate = StartDate;
                    for (Int32 count = 0; count < (m_NumberOfWeeks * 7); count++)
                    {
                        m_DayBoxList.Add(new DayBox(this, m_TempDate.ToString(m_DateFormat)));
                        m_TempDate = m_TempDate.AddDays(1);
                    }
                    break;
                case ViewType.Year:
                    StartDate = DateTime.Parse("01/01/" + InputDate.Year.ToString());
                    StartDate = StartDate.AddDays((Int32)StartDate.DayOfWeek * -1);
                    EndDate = DateTime.Parse("12/31/" + InputDate.Year.ToString());
                    EndDate = EndDate.AddDays(6 - ((Int32)EndDate.DayOfWeek));
                    m_NumberOfWeeks = (((EndDate - StartDate).Days + 1) / 7);

                    m_TempDate = StartDate;
                    for (Int32 count = 0; count < (m_NumberOfWeeks * 7); count++)
                    {
                        m_DayBoxList.Add(new DayBox(this, m_TempDate.ToString(m_DateFormat)));
                        m_TempDate = m_TempDate.AddDays(1);
                    }
                    break;
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            CreateChildControls();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public override void RenderControl(HtmlTextWriter writer)
        {
            DisplayWeek(writer);
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void DisplayWeek(HtmlTextWriter writer)
        {
            writer.Write("<table class='calendar' cellspacing='5'>");

            DisplayHeader(writer);

            for (Int32 outer = 0; outer < m_NumberOfWeeks; outer++)
            {
                // DATE-DROPDOWNLIST & IMAGEBUTTON
                writer.Write("<tr class='daterow'>");
                for (Int32 inner = 0; inner < 7; inner++)
                {
                    writer.Write("<td class='" + CalendarFormat(inner, "date", GetDayBox(outer, inner).Label_Date.Text) + "'>");
                    writer.Write("<div class='datetext'>");

                    if((m_IsEditMode) && (GetDayBox(outer, inner).Label_Date.Text == m_Button_Save.CommandArgument))
                        m_DropDownList_SectionType.RenderControl(writer);
                    else
                        GetDayBox(outer, inner).Label_Date.RenderControl(writer);

                    writer.Write("</div>");
                    writer.Write("<div class='datebutton'>");

                    if((m_IsEditMode) && (GetDayBox(outer, inner).Label_Date.Text == m_Button_Save.CommandArgument))
                        m_Button_Save.RenderControl(writer);
                    else
                        GetDayBox(outer, inner).Button_Edit.RenderControl(writer);
                    
                    writer.Write("</div>");
                    writer.Write("</td>");
                }
                writer.Write("</tr>");

                // DATA-TEXTBOX
                writer.Write("<tr class='datarow'>");
                for (Int32 inner = 0; inner < 7; inner++)
                {
                    writer.Write("<td class='" + CalendarFormat(inner, "data", GetDayBox(outer, inner).Label_Date.Text) + "'>");

                    if((m_IsEditMode) && (GetDayBox(outer, inner).Label_Date.Text == m_Button_Save.CommandArgument))
                    {
                        m_TextBox_EnteredData.RenderControl(writer);
                    }
                    else
                    {
                        writer.Write("<div class='content'><span></span>");

                        var query = from obj in CalendarDataSource
                                    where (((DateTime)obj.GetType().GetProperty("Date").GetValue(obj, null)).ToString(m_DateFormat) == GetDayBox(outer, inner).Label_Date.Text)
                                    select new { Text = obj.GetType().GetProperty("Text").GetValue(obj, null).ToString(), Style = obj.GetType().GetProperty("Style").GetValue(obj, null).ToString() };

                        foreach(var entry in query)
                        {
                            writer.Write("<div class='record' style='" + entry.Style + "'>");
                            writer.Write(entry.Text.Replace("\n", "<br />"));
                            writer.Write("</div>");
                        }
                        writer.Write("</div>");
                    }
                    writer.Write("</td>");
                }
                writer.Write("</tr>");
            }
            writer.Write("</table>");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private DayBox GetDayBox(Int32 outer, Int32 inner)
        {
            return m_DayBoxList[(outer * 7) + inner];
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void DisplayHeader(HtmlTextWriter writer)
        {
            writer.Write("<tr>");
            writer.Write("<td class='dateheader'>Sunday</td>");
            writer.Write("<td class='dateheader'>Monday</td>");
            writer.Write("<td class='dateheader'>Tuesday</td>");
            writer.Write("<td class='dateheader'>Wednesday</td>");
            writer.Write("<td class='dateheader'>Thursday</td>");
            writer.Write("<td class='dateheader'>Friday</td>");
            writer.Write("<td class='dateheader'>Saturday</td>");
            writer.Write("</tr>");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private String CalendarFormat(Int32 index, String baseCssClass, String labelDateText)
        {
            String dateclass = baseCssClass;
            String datetext = labelDateText;
            String currentmonth = DateTime.Now.ToString("MMMM");
            String currentyear = DateTime.Now.ToString("yyyy");

            if ((index == 0) || (index == 6))
                dateclass += " weekend";
            else
                dateclass += " weekday";

            if (datetext == DateTime.Now.ToString(m_DateFormat))
                dateclass += " today";

            if (datetext.Contains(currentmonth) && datetext.Contains(currentyear))
                dateclass += " present";
            else if (DateTime.Parse(datetext) < DateTime.Now.Date)
                dateclass += " past";
            else
                dateclass += " future";

            return dateclass;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void EditInfo(Object o, EventArgs e)
        {
            m_TextBox_EnteredData.Text = String.Empty;

            if(o is ImageButton)
            {
                m_Button_Save.CommandArgument = ((ImageButton)o).CommandArgument;
                m_DropDownList_SectionType.DataValueField = "Id";
                m_DropDownList_SectionType.DataTextField = "Text";
                m_DropDownList_SectionType.DataSource = SectionTypeDataSource;
                m_DropDownList_SectionType.DataBind();
            }

            if(m_DropDownList_SectionType.Items.Count > 0)
            {
                foreach(Object obj in MainContentDataSource)
                    m_TextBox_EnteredData.Text = obj.GetType().GetProperty("Text").GetValue(obj, null).ToString();

                m_TextBox_EnteredData.Focus();
                m_IsEditMode = true;
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void SaveInfo(Object o, EventArgs e)
        {
            SaveEvent(m_TextBox_EnteredData.Text, SelectedEditDate, SelectedSectionType);
        }
    }
}
