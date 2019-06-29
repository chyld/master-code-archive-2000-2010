using System;
using System.Collections.Generic;
using Chyld.Apocrypha.Alias;

namespace Chyld.Apocrypha.Fiction
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

        protected void Page_Load(Object o, EventArgs e)
        {
            textboxA.Focus();
            CheckAuthentication();
        }

        protected void Run(Object o, EventArgs e)
        {
            if (IsAuthenticated)
            {
                Manager m = new Manager();

                Dictionary<String, String> input = new Dictionary<String, String>();
                input["A"] = textboxA.Text;
                input["B"] = textboxB.Text;
                input["C"] = textboxC.Text;
                input["D"] = textboxD.Text;
                input["E"] = textboxE.Text;
                input["F"] = textboxF.Text;
                input["G"] = textboxG.Text;
                input["H"] = textboxH.Text;
                input["I"] = textboxI.Text;
                input["J"] = textboxJ.Text;
                input["K"] = textboxK.Text;
                input["L"] = textboxL.Text;
                input["M"] = textboxM.Text;
                input["N"] = textboxN.Text;
                input["O"] = textboxO.Text;
                input["P"] = textboxP.Text;
                input["Q"] = textboxQ.Text;
                input["R"] = textboxR.Text;
                input["S"] = textboxS.Text;
                input["T"] = textboxT.Text;

                Dictionary<String, String> output = m.Compute(input);
                textboxA.Text = output["A"];
                textboxB.Text = output["B"];
                textboxC.Text = output["C"];
                textboxD.Text = output["D"];
                textboxE.Text = output["E"];
                textboxF.Text = output["F"];
                textboxG.Text = output["G"];
                textboxH.Text = output["H"];
                textboxI.Text = output["I"];
                textboxJ.Text = output["J"];
                textboxK.Text = output["K"];
                textboxL.Text = output["L"];
                textboxM.Text = output["M"];
                textboxN.Text = output["N"];
                textboxO.Text = output["O"];
                textboxP.Text = output["P"];
                textboxQ.Text = output["Q"];
                textboxR.Text = output["R"];
                textboxS.Text = output["S"];
                textboxT.Text = output["T"];
            }
            else
            {
                textboxA.Text = String.Empty;
                textboxB.Text = String.Empty;
                textboxC.Text = String.Empty;
                textboxD.Text = String.Empty;
                textboxE.Text = String.Empty;
                textboxF.Text = String.Empty;
                textboxG.Text = String.Empty;
                textboxH.Text = String.Empty;
                textboxI.Text = String.Empty;
                textboxJ.Text = String.Empty;
                textboxK.Text = String.Empty;
                textboxL.Text = String.Empty;
                textboxM.Text = String.Empty;
                textboxN.Text = String.Empty;
                textboxO.Text = String.Empty;
                textboxP.Text = String.Empty;
                textboxQ.Text = String.Empty;
                textboxR.Text = String.Empty;
                textboxS.Text = String.Empty;
                textboxT.Text = String.Empty;
            }
        }

        private void CheckAuthentication()
        {
            switch (textboxD.Text)
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
