using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Web.Security;

namespace Es.Udc.DotNet.Photogram.Web.Pages.User
{
    public partial class Authentication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPasswordError.Visible = false;
            lblLoginError.Visible = false;
        }

        protected void BtnLoginClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    SessionManager.Login(Context, txtLogin.Text,
                        txtPassword.Text, checkRememberPassword.Checked);

                    FormsAuthentication.
                        RedirectFromLoginPage(txtLogin.Text,
                            checkRememberPassword.Checked);
                }
                catch (InstanceNotFoundException)
                {
                    lblLoginError.Visible = true;
                }
                catch (IncorrectPasswordException)
                {
                    lblPasswordError.Visible = true;
                }
            }
        }
    }
}