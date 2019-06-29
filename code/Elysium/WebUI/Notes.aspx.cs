namespace Chyld.Elysium.WebUI
{
    using System;
    using System.Web.UI;
    using System.Linq;

    using Chyld.Elysium.Data;
    using Chyld.Elysium.Extension;
    using Chyld.Elysium.Security;

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public partial class Notes : System.Web.UI.Page
    {
        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private ElysiumDB m_DB = new ElysiumDB();
        private Boolean m_ShouldBind;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public Int32 UserId
        {
            get { return ((Authentication)Session["Authentication"]).UserId; }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void Page_Init(Object o, EventArgs e)
        {
            AddAsyncPostBackTriggers();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void Page_Load(Object o, EventArgs e)
        {
            textboxNotebookData.Enabled = true;
            m_ShouldBind = false;
            textboxNotebookData.Focus();
            AddAsyncPostBackTriggers();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void Page_PreRender(Object o, EventArgs e)
        {
            textboxNotebookName.Text = String.Empty;
            Bind();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonExit_Click(Object o, EventArgs e)
        {
            ((Authentication)Session["Authentication"]).IsValidUser = false;
            Response.Redirect("~/Main.aspx");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonCalendar_Click(Object o, EventArgs e)
        {
            Response.Redirect("~/Main.aspx");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonAdmin_Click(Object o, EventArgs e)
        {
            Response.Redirect("~/Admin.aspx");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonSave_Click(Object o, EventArgs e)
        {
            Notebook notebook = m_DB.Notebooks.SingleOrDefault(nb => nb.NotebookId == dropdownlistNotebookName.SelectedValue.ToInt32());

            if(notebook != null)
            {
                notebook.Value = textboxNotebookData.Text;
                m_DB.SubmitChanges();
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonAddNotebook_Click(Object o, EventArgs e)
        {
            if((textboxNotebookName.Text != String.Empty) && (m_DB.Notebooks.SingleOrDefault(nb => (nb.UserId == UserId) && (nb.Name == textboxNotebookName.Text)) == null))
            {
                Notebook notebook = new Notebook();
                notebook.UserId = UserId;
                notebook.Name = textboxNotebookName.Text;
                notebook.Value = String.Empty;

                m_DB.Notebooks.InsertOnSubmit(notebook);
                m_DB.SubmitChanges();

                m_ShouldBind = true;
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void AddAsyncPostBackTriggers()
        {
            AsyncPostBackTrigger triggerSave = new AsyncPostBackTrigger();
            triggerSave.ControlID = imagebuttonSave.UniqueID;
            updatepanel.Triggers.Add(triggerSave);
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private void Bind()
        {
            if((m_ShouldBind) || (!IsPostBack))
            {
                var query = from notebook in m_DB.Notebooks
                            where notebook.UserId == UserId
                            orderby notebook.Name ascending
                            select new { Text = notebook.Name, Value = notebook.NotebookId };

                dropdownlistNotebookName.DataSource = query;
                dropdownlistNotebookName.DataTextField = "Text";
                dropdownlistNotebookName.DataValueField = "Value";
                dropdownlistNotebookName.DataBind();
            }

            if(dropdownlistNotebookName.Items.Count > 0)
                textboxNotebookData.Text = m_DB.Notebooks.SingleOrDefault(nb => nb.NotebookId == dropdownlistNotebookName.SelectedValue.ToInt32()).Value;
            else
                textboxNotebookData.Enabled = false;
        }
    }
}
