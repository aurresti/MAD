using System;

using Es.Udc.DotNet.Photogram.Web.HTTP.Session;

namespace Es.Udc.DotNet.Photogram.Web
{
    public partial class Photogram : System.Web.UI.MasterPage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
                if (lnkProfile != null)
                    lnkProfile.Visible = false;
                if (lnkUploadImage != null)
                    lnkUploadImage.Visible = false;
                if (lnkExit != null)
                    lnkExit.Visible = false;
                if (lnkHome != null)
                    lnkHome.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
            }
            else
            {
                if (lblWelcome != null)
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lnkAuth != null)
                    lnkAuth.Visible = false;
                if (lnkRegister != null)
                    lnkRegister.Visible = false;
                if (lnkUploadImage != null)
                    lnkUploadImage.Visible = false;
            }
        }
    }
}