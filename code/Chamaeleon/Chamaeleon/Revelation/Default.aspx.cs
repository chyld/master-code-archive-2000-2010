using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chyld.Chamaeleon.Enigma;

namespace Chyld.Chamaeleon.Revelation
{
    public partial class _Default : System.Web.UI.Page
    {
        private const String m_start = "one";
        private const String m_stop = "zero";

        public Boolean IsAuthenticated
        {
            get
            {
                if (Session["IsAuthenticated"] == null) Session["IsAuthenticated"] = false;
                return (Boolean)Session["IsAuthenticated"];
            }
            set
            {
                Session["IsAuthenticated"] = value;
            }
        }

        public Boolean IsDataA
        {
            get { return !String.IsNullOrEmpty(textboxA.Text); }
        }
        
        public Boolean IsDataB
        {
            get { return !String.IsNullOrEmpty(textboxB.Text); }
        }

        public Boolean IsDataC
        {
            get { return !String.IsNullOrEmpty(textboxC.Text); }
        }

        protected void Page_Load(Object o, EventArgs e)
        {
            textboxA.Focus();
            CheckAuthentication();
        }

        protected void Run(Object o, EventArgs e)
        {
            if (IsAuthenticated)
            {
                if (IsDataA && !IsDataB && !IsDataC)
                {
                    textboxB.Text = textboxA.Text.GetKey();
                    textboxA.Text = String.Empty;
                    textboxC.Text = String.Empty;
                    return;
                }

                if (!IsDataA && IsDataB && !IsDataC)
                {
                    textboxA.Text = textboxB.Text.GetUsr();
                    textboxC.Text = textboxB.Text.GetPwd();
                    textboxB.Text = String.Empty;
                    return;
                }
            }

            textboxA.Text = String.Empty;
            textboxB.Text = String.Empty;
            textboxC.Text = String.Empty;
        }

        private void CheckAuthentication()
        {
            switch (textboxC.Text)
            {
                case m_start:
                    IsAuthenticated = true;
                    break;
                case m_stop:
                    IsAuthenticated = false;
                    break;
            }
        }
    }
}
