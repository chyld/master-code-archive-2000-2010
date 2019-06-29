namespace Chyld.Elysium.WebUI
{
    using System;
    using System.Web.UI;
    using System.Linq;

    using Chyld.Elysium.Data;
    using Chyld.Elysium.Extension;
    using Chyld.Elysium.Security;

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public partial class Admin : System.Web.UI.Page
    {
        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private ElysiumDB m_DB;
        private Boolean m_BindSection;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public Int32 UserId
        {
            get { return ((Authentication)Session["Authentication"]).UserId; }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void Page_Init(Object o, EventArgs e)
        {
            m_DB = new ElysiumDB();
            m_BindSection = false;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void Page_PreRender(Object o, EventArgs e)
        {
            textboxSection.Text = String.Empty;
            textboxSection.Focus();

            if(!IsPostBack || m_BindSection)
                BindSection();

            BindStyle();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonAdd_Click(Object o, EventArgs e)
        {
            if(textboxSection.Text != String.Empty)
            {
                Facade style = new Facade();
                style.FontId = m_DB.Fonts.FirstOrDefault().FontId;
                style.Size = 12;
                style.Spacing = 0;
                style.Color = "000000";
                style.IsBold = false;
                style.IsItalic = false;
                style.IsUnderline = false;
                m_DB.Facades.InsertOnSubmit(style);
                m_DB.SubmitChanges();

                Section section = new Section();
                section.Name = textboxSection.Text;
                section.UserId = UserId;
                section.StyleId = style.StyleId;
                m_DB.Sections.InsertOnSubmit(section);
                m_DB.SubmitChanges();

                m_BindSection = true;
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonUpdate_Click(Object o, EventArgs e)
        {
            if(dropdownlistSection.Items.Count > 0)
            {
                Section section = m_DB.Sections.SingleOrDefault(s => s.SectionId == dropdownlistSection.SelectedValue.ToInt32());

                section.Facade.FontId = dropdownlistFont.SelectedValue.ToInt32();
                section.Facade.Size = textboxSize.Text.ToInt32();
                section.Facade.Spacing = textboxSpacing.Text.ToInt32();
                section.Facade.Color = textboxColor.Text;
                section.Facade.IsBold = checkboxBold.Checked;
                section.Facade.IsItalic = checkboxItalic.Checked;
                section.Facade.IsUnderline = checkboxUnderline.Checked;

                m_DB.SubmitChanges();
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonExit_Click(Object o, EventArgs e)
        {
            ((Authentication)Session["Authentication"]).IsValidUser = false;
            Response.Redirect("~/Admin.aspx");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonNotes_Click(Object o, EventArgs e)
        {
            Response.Redirect("~/Notes.aspx");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonCalendar_Click(Object o, EventArgs e)
        {
            Response.Redirect("~/Main.aspx");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void BindSection()
        {
            dropdownlistSection.DataSource = from section in m_DB.Sections
                                             where section.UserId == UserId
                                             orderby section.Name
                                             select new { Text = section.Name, Id = section.SectionId };

            dropdownlistSection.DataTextField = "Text";
            dropdownlistSection.DataValueField = "Id";
            dropdownlistSection.DataBind();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void BindStyle()
        {
            if(dropdownlistSection.Items.Count > 0)
            {
                Section section = m_DB.Sections.SingleOrDefault(s => s.SectionId == dropdownlistSection.SelectedValue.ToInt32());

                dropdownlistFont.DataSource = from font in m_DB.Fonts
                                              orderby font.Name
                                              select new { Text = font.Name, Id = font.FontId };

                dropdownlistFont.DataTextField = "Text";
                dropdownlistFont.DataValueField = "Id";
                dropdownlistFont.DataBind();

                dropdownlistFont.SelectedValue = section.Facade.FontId.ToString();
                textboxSize.Text = section.Facade.Size.ToString();
                textboxSpacing.Text = section.Facade.Spacing.ToString();
                textboxColor.Text = section.Facade.Color;
                checkboxBold.Checked = section.Facade.IsBold;
                checkboxItalic.Checked = section.Facade.IsItalic;
                checkboxUnderline.Checked = section.Facade.IsUnderline;

                dropdownlistFont.Enabled = true;
                textboxSize.Enabled = true;
                textboxSpacing.Enabled = true;
                textboxColor.Enabled = true;
                checkboxBold.Enabled = true;
                checkboxItalic.Enabled = true;
                checkboxUnderline.Enabled = true;
                imagebuttonUpdate.Enabled = true;
            }
            else
            {
                dropdownlistFont.Enabled = false;
                textboxSize.Enabled = false;
                textboxSpacing.Enabled = false;
                textboxColor.Enabled = false;
                checkboxBold.Enabled = false;
                checkboxItalic.Enabled = false;
                checkboxUnderline.Enabled = false;
                imagebuttonUpdate.Enabled = false;
            }
        }
    }
}
