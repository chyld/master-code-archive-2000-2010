#region assembly references
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Chyld.Scribe.Common;
using Chyld.Scribe.BLL;
#endregion

namespace Chyld.Scribe.Scribe
{
    public partial class DreamPage : System.Web.UI.Page
    {
        #region fields
        private Manager m_Manager;
        #endregion

        #region events
        #region event :: protected void Page_Load(object p_Sender, EventArgs p_EventArgs)
        protected void Page_Load(object p_Sender, EventArgs p_EventArgs)
        {
            try
            {
                m_Manager = new Manager(String.Empty, String.Empty);
                textboxPasswordA.Focus();
            }
            catch { }
        }
        #endregion

        #region event :: protected void buttonPasswordAuthenticate_Click(object p_Sender, EventArgs p_EventArgs)
        protected void buttonPasswordAuthenticate_Click(object p_Sender, EventArgs p_EventArgs)
        {
            try
            {
                if(m_Manager.IsPasswordAuthenticated(textboxPasswordA.Text, textboxPasswordB.Text, textboxPasswordC.Text))
                    Session["authenticated"] = true;
                else
                    Session["authenticated"] = false;

                Response.Redirect("Default.aspx");
            }
            catch { }
        }
        #endregion
        #endregion
    }
}
