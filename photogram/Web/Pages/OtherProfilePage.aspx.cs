using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.CommentService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class OtherProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string valor = Request.QueryString["userId"];
                long id = (long)Convert.ToDouble(valor);
                UserProfile user = SessionManager.FindUserProfileDetailsUser(id);
                lUser.Text = user.FirstName;
                if (SessionManager.ExistFollow(Context, user.FirstName))
                {
                    lIsFollow.Visible = true;
                }
                else
                {
                    lIsFollow.Visible = false;
                }
            }
        }

        protected void bFollowed_Click(object sender, EventArgs e)
        {
            string valor = Request.QueryString["userId"];
            long id = (long)Convert.ToDouble(valor);
            SessionManager.SeeFolloweds(id);
        }

        protected void bFollowers_Click(object sender, EventArgs e)
        {
            string valor = Request.QueryString["userId"];
            long id = (long)Convert.ToDouble(valor);
            var followed = SessionManager.SeeFollowers(id);
        }

        protected void bFollow_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string valor = Request.QueryString["userId"];
                    long id = (long)Convert.ToDouble(valor);
                    UserProfile user = SessionManager.FindUserProfileDetailsUser(id);
                    if (SessionManager.ExistFollow(Context, user.FirstName))
                    {
                        SessionManager.UnFollowUser(Context, id);
                        lIsFollow.Visible = false;
                    }
                    else
                    {
                        SessionManager.FollowUser(Context, id);
                        lIsFollow.Visible = true;
                    }

                    //Response.Redirect(Response.
                        //ApplyAppPathModifier("~/Pages/OtherProfilePage.aspx"+"?userId="+id));
                }
                catch (DuplicateInstanceException)
                {
                    lIsFollow.Visible = true;
                }
            }
        }

        protected void bImage1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string valor = Request.QueryString["userId"];
                long id = (long)Convert.ToDouble(valor);

                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/ImageProfile.aspx"));
            }
        }

        protected void bImage2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string valor = Request.QueryString["userId"];
                long id = (long)Convert.ToDouble(valor);

                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/ImageProfile.aspx"));
            }
        }

        protected void bImage3_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string valor = Request.QueryString["userId"];
                long id = (long)Convert.ToDouble(valor);

                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/ImageProfile.aspx"));
            }
        }
    }
}