namespace Chyld.Elysium.WebUI
{
    using System;
    using System.Web.UI;

    using Chyld.Elysium.Data;
    using Chyld.Elysium.Extension;
    using Chyld.Elysium.Security;

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public partial class Login : System.Web.UI.Page
    {
        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void Page_Load(Object o, EventArgs e)
        {
            textboxUsername.Focus();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        protected void imagebuttonLogin_Click(Object o, EventArgs e)
        {
            Session["Authentication"] = new Authentication(textboxUsername.Text, textboxPassword.Text);
            Response.Redirect("~/Main.aspx");
        }
    }
}
