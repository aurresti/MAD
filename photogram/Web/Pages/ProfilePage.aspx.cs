using System;

using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.Photogram.Model;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //gvFollowed.Visible = false;
        }

        protected void bChangePassword_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/ChangePassword.aspx"));
            }
        }

        protected void bUpgrade_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/User/UpdateUserProfile.aspx"));
            }
        }

        protected void bFollowed_Click(object sender, EventArgs e)
        {
            
            var followed = SessionManager.SeeFollowedsUser(Context);
            List<String> name = new List<String>();
            foreach (UserAccount aux in followed) {
                name.Add(aux.firstName);
            }
            /*foreach (UserAccount aux in followed) {
                HyperLink hyp = new HyperLink();
                hyp.ID = "hyp" + aux.loginName;
                hyp.NavigateUrl = "~/Pages/OtherProfilePage.aspx?userId=" + aux.userId;
                name.Add(hyp);
            }*/
            gvFollowed.DataSource = name;
            gvFollowed.DataBind();
            if (gvFollowed.Visible) {
                gvFollowed.Visible = false;
            } else {
                gvFollowed.Visible = true;
            }
        }

        protected void bFollowers_Click(object sender, EventArgs e)
        {
            var follower = SessionManager.SeeFollowersUser(Context);
            List<String> name = new List<String>();
            foreach (UserAccount aux in follower)
            {
                name.Add(aux.loginName);
            }
            gvFollowed.DataSource = name;
            gvFollowed.DataBind();
            if (gvFollower.Visible)
            {
                gvFollower.Visible = false;
            }
            else
            {
                gvFollowed.Visible = true;
            }
        }

        protected void bImage1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ImageProfile.aspx?imageId=15"));
            }
        }

        protected void bImage2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/Image/ImageProfile.aspx"));
            }
        }

        protected void bImage3_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/Image/ImageProfile.aspx"));
            }
        }
    }
}