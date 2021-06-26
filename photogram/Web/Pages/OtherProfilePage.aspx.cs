using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.CommentService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.Photogram.Model;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class OtherProfilePage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string valor = Request.QueryString["userId"];
                long id = (long)Convert.ToDouble(valor);
                var images = SessionManager.FindImageProfileDetailsByUserIdUser(id);
                UserProfile user = SessionManager.FindUserProfileDetailsUser(id);
                lUser.Text = user.FirstName;
                if (SessionManager.IsUserAuthenticated(Context))
                {
                    if (SessionManager.ExistFollow(Context, user.FirstName))
                    {
                        lIsFollow.Visible = true;
                    }
                    else
                    {
                        lIsFollow.Visible = false;
                    }
                }
                else
                {
                    lIsFollow.Visible = false;
                }
                
                if (images.Count == 1)
                {
                    Image1.ImageUrl = images[0].imageView;
                    Image2.ImageUrl = "";
                    Image3.ImageUrl = "";
                    bImage1.Visible = true;
                    bImage2.Visible = false;
                    bImage3.Visible = false;
                    Image1.Visible = true;
                    Image2.Visible = false;
                    Image3.Visible = false;
                }
                else if (images.Count == 2)
                {
                    Image1.ImageUrl = images[0].imageView;
                    Image2.ImageUrl = images[1].imageView;
                    Image3.ImageUrl = "";
                    bImage1.Visible = true;
                    bImage2.Visible = true;
                    bImage3.Visible = false;
                    Image1.Visible = true;
                    Image2.Visible = true;
                    Image3.Visible = false;
                }
                else if (images.Count >= 3)
                {
                    Image1.ImageUrl = images[0].imageView;
                    Image2.ImageUrl = images[1].imageView;
                    Image3.ImageUrl = images[2].imageView;
                    bImage1.Visible = true;
                    bImage2.Visible = true;
                    bImage3.Visible = true;
                    Image1.Visible = true;
                    Image2.Visible = true;
                    Image3.Visible = true;
                }
                else {
                    Image1.ImageUrl = "";
                    Image2.ImageUrl = "";
                    Image3.ImageUrl = "";
                    bImage1.Visible = false;
                    bImage2.Visible = false;
                    bImage3.Visible = false;
                    Image1.Visible = false;
                    Image2.Visible = false;
                    Image3.Visible = false;
                }
            }
        }

        protected void bFollowed_Click(object sender, EventArgs e)
        {
            string valor = Request.QueryString["userId"];
            long id = (long)Convert.ToDouble(valor);
            var followed = SessionManager.SeeFollowers(id);
            gvFollowed.DataSource = followed;
            gvFollowed.DataBind();
            if (gvFollowed.Visible)
            {
                gvFollowed.Visible = false;
            }
            else
            {
                gvFollowed.Visible = true;
            }
        }

        protected void bFollowers_Click(object sender, EventArgs e)
        {
            string valor = Request.QueryString["userId"];
            long id = (long)Convert.ToDouble(valor);
            var follower = SessionManager.SeeFolloweds(id);
            gvFollower.DataSource = follower;
            gvFollower.DataBind();
            if (gvFollower.Visible)
            {
                gvFollower.Visible = false;
            }
            else
            {
                gvFollower.Visible = true;
            }
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
                    if (SessionManager.IsUserAuthenticated(Context))
                    {
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
                    }
                    else {
                        Response.Redirect(Response.
                            ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
                    }

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
                var images = SessionManager.FindImageProfileDetailsByUserIdUser(id);
                if (images.Count<1)
                {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Error/InternalError.aspx"));
                }
                else
                {
                    Response.Redirect(Response.
                            ApplyAppPathModifier("~/Pages/Image/ImageProfile.aspx?imageId=" + images[0].imageId));
                }
            }
        }

        protected void bImage2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string valor = Request.QueryString["userId"];
                long id = (long)Convert.ToDouble(valor);
                var images = SessionManager.FindImageProfileDetailsByUserIdUser(id);
                if (images.Count<2)
                {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Error/InternalError.aspx"));
                }
                else
                {
                    Response.Redirect(Response.
                            ApplyAppPathModifier("~/Pages/Image/ImageProfile.aspx?imageId=" + images[1].imageId));
                }
            }
        }

        protected void bImage3_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string valor = Request.QueryString["userId"];
                long id = (long)Convert.ToDouble(valor);
                var images = SessionManager.FindImageProfileDetailsByUserIdUser(id);
                if (images.Count < 3)
                {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Error/InternalError.aspx"));
                }
                else
                {
                    Response.Redirect(Response.
                            ApplyAppPathModifier("~/Pages/Image/ImageProfile.aspx?imageId=" + images[2].imageId));
                }
            }
        }
    }
}