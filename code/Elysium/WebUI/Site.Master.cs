namespace Chyld.Elysium.WebUI
{
    using System;
    using System.Web.UI;

    using Chyld.Elysium.Data;
    using Chyld.Elysium.Extension;
    using Chyld.Elysium.Security;

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public partial class Site : System.Web.UI.MasterPage
    {
        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private readonly String m_LoginPage = "~/Login.aspx";

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void Page_Init(Object o, EventArgs e)
        {
            if(Session["Authentication"] == null)
                Session["Authentication"] = new Authentication(String.Empty, String.Empty);

            if((!((Authentication)Session["Authentication"]).IsValidUser) && (Page.AppRelativeVirtualPath != m_LoginPage))
                Response.Redirect(m_LoginPage);
        }
    }
}
