namespace Chyld.Elysium.WebUI
{
    using System;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using Chyld.Elysium.Controls;
    using Chyld.Elysium.Data;
    using Chyld.Elysium.Extension;
    using Chyld.Elysium.Security;

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public partial class Main : System.Web.UI.Page
    {
        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private ElysiumDB m_DB;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public Int32 UserId
        {
            get { return ((Authentication)Session["Authentication"]).UserId; }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void Page_Init(Object o, EventArgs e)
        {
            m_DB = new ElysiumDB();
            BuildFilterUI();
            AddAsyncPostBackTriggers();
            calendar.SaveEvent += SaveData;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void Page_Load(Object o, EventArgs e)
        {
            QueryDatabase();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonExit_Click(Object o, EventArgs e)
        {
            ((Authentication)Session["Authentication"]).IsValidUser = false;
            Response.Redirect("~/Main.aspx");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonNotes_Click(Object o, EventArgs e)
        {
            Response.Redirect("~/Notes.aspx");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonAdmin_Click(Object o, EventArgs e)
        {
            Response.Redirect("~/Admin.aspx");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonHome_Click(Object o, EventArgs e)
        {
            calendar.InputDate = DateTime.Now;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonLeft_Click(Object o, EventArgs e)
        {
            calendar.DecrementDate();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonRight_Click(Object o, EventArgs e)
        {
            calendar.IncrementDate();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonWeek_Click(Object o, EventArgs e)
        {
            calendar.Display = ViewType.Week;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonBiWeek_Click(Object o, EventArgs e)
        {
            calendar.Display = ViewType.BiWeek;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonMonth_Click(Object o, EventArgs e)
        {
            calendar.Display = ViewType.Month;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonYear_Click(Object o, EventArgs e)
        {
            calendar.Display = ViewType.Year;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void AddAsyncPostBackTriggers()
        {
            AsyncPostBackTrigger triggerLeft = new AsyncPostBackTrigger();
            triggerLeft.ControlID = imagebuttonLeft.UniqueID;
            updatepanel.Triggers.Add(triggerLeft);

            AsyncPostBackTrigger triggerRight = new AsyncPostBackTrigger();
            triggerRight.ControlID = imagebuttonRight.UniqueID;
            updatepanel.Triggers.Add(triggerRight);

            AsyncPostBackTrigger triggerHome = new AsyncPostBackTrigger();
            triggerHome.ControlID = imagebuttonHome.UniqueID;
            updatepanel.Triggers.Add(triggerHome);

            AsyncPostBackTrigger triggerWeek = new AsyncPostBackTrigger();
            triggerWeek.ControlID = imagebuttonWeek.UniqueID;
            updatepanel.Triggers.Add(triggerWeek);

            AsyncPostBackTrigger triggerBiWeek = new AsyncPostBackTrigger();
            triggerBiWeek.ControlID = imagebuttonBiWeek.UniqueID;
            updatepanel.Triggers.Add(triggerBiWeek);

            AsyncPostBackTrigger triggerMonth = new AsyncPostBackTrigger();
            triggerMonth.ControlID = imagebuttonMonth.UniqueID;
            updatepanel.Triggers.Add(triggerMonth);

            AsyncPostBackTrigger triggerYear = new AsyncPostBackTrigger();
            triggerYear.ControlID = imagebuttonYear.UniqueID;
            updatepanel.Triggers.Add(triggerYear);
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void QueryDatabase()
        {
            calendar.CalendarDataSource    = from entry in m_DB.Agendas
                                             where ((entry.Day >= calendar.StartDate) && (entry.Day <= calendar.EndDate) && FilterBoxList().Contains(entry.SectionId))
                                             orderby entry.Day, entry.SectionId
                                             select new { Text = entry.Value, Date = entry.Day, Style = FormatStyle(entry.Section.Facade.Font.Name, entry.Section.Facade.Size, entry.Section.Facade.Spacing, entry.Section.Facade.Color, entry.Section.Facade.IsBold, entry.Section.Facade.IsItalic, entry.Section.Facade.IsUnderline) } as Object;

            calendar.SectionTypeDataSource = from entry in m_DB.Sections
                                             where (entry.UserId == UserId)
                                             orderby entry.Name
                                             select new { Id = entry.SectionId, Text = entry.Name } as Object;

            calendar.MainContentDataSource = from entry in m_DB.Agendas
                                             where ((entry.Day == calendar.SelectedEditDate) && (entry.SectionId == calendar.SelectedSectionType))
                                             select new { Text = entry.Value } as Object;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private String FormatStyle(String font, Int32 size, Int32 spacing, String color, Boolean isBold, Boolean isItalic, Boolean isUnderline)
        {
            String style = String.Empty;

            style += "font-family: " + font + "; ";
            style += "font-size: " + size.ToString() + "px; ";
            style += "letter-spacing: " + spacing.ToString() + "px; ";
            style += "color: #" + color + "; ";

            if(isBold)
                style += "font-weight: bold; ";
            else
                style += "font-weight: normal; ";

            if(isItalic)
                style += "font-style: italic; ";
            else
                style += "font-style: normal; ";

            if(isUnderline)
                style += "text-decoration: underline; ";
            else
                style += "text-decoration: none; ";

            return style;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void BuildFilterUI()
        {
            var sections = from section in m_DB.Sections
                           where section.UserId == UserId
                           select new { section.SectionId, section.Name };

            foreach(var section in sections)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "Filter_" + section.SectionId.ToString();
                checkbox.Checked = true;
                checkbox.AutoPostBack = true;
                checkbox.EnableViewState = true;

                Label label = new Label();
                label.ID = "LabelFilter_" + section.SectionId.ToString();
                label.Text = section.Name;
                Section sec = m_DB.Sections.SingleOrDefault(item => item.SectionId == section.SectionId);
                if(sec != null)
                {
                    label.Attributes.Add("style", FormatStyle(sec.Facade.Font.Name, sec.Facade.Size, sec.Facade.Spacing, sec.Facade.Color, sec.Facade.IsBold, sec.Facade.IsItalic, sec.Facade.IsUnderline));
                }

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "filterbox");
                div.Controls.Add(checkbox);
                div.Controls.Add(label);

                panelFilter.Controls.Add(div);

                AsyncPostBackTrigger triggerFilter = new AsyncPostBackTrigger();
                triggerFilter.ControlID = checkbox.UniqueID;
                updatepanel.Triggers.Add(triggerFilter);
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private List<Int32> FilterBoxList()
        {
            List<Int32> list = new List<Int32>();

            foreach(HtmlGenericControl div in panelFilter.Controls)
                foreach(Control c in div.Controls)
                    if(c is CheckBox)
                        if(((CheckBox)c).Checked)
                            list.Add(c.ID.Substring(7).ToInt32());

            return list;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void SaveData(String text, DateTime date, Int32 id)
        {
            Agenda agenda = m_DB.Agendas.SingleOrDefault(a => (a.Section.UserId == UserId) && (a.Day == date) && (a.SectionId == id));

            if(agenda != null)
            {
                agenda.Value = text;
            }
            else
            {
                agenda = new Agenda();
                agenda.Day = date;
                agenda.SectionId = id;
                agenda.Value = text;

                m_DB.Agendas.InsertOnSubmit(agenda);
            }

            m_DB.SubmitChanges();
        }
    }
}
