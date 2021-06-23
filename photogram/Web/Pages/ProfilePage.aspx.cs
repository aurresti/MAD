using System;

using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bChangePassword_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/ChangePassword.aspx"));
            }
        }

        protected void bUpgrade_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/UpdateUserProfile.aspx"));
            }
        }

        protected void bFollowed_Click(object sender, EventArgs e)
        {

        }

        protected void bFollowers_Click(object sender, EventArgs e)
        {

        }

        protected void bImage1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/ImageProfile.aspx"));
            }
        }

        protected void bImage2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/ImageProfile.aspx"));
            }
        }

        protected void bImage3_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/ImageProfile.aspx"));
            }
        }
    }
}