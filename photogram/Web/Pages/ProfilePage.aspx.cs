using System;

using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.Photogram.Model;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class ProfilePage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //gvFollowed.Visible = false;
            var images = SessionManager.FindImageProfileDetailsByUserId(Context);
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
            else
            {
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
            
            var followed = SessionManager.SeeFollowersUser(Context);
            gvFollowed.DataSource = followed;
            gvFollowed.DataBind();
            if (gvFollowed.Visible) {
                gvFollowed.Visible = false;
            } else {
                gvFollowed.Visible = true;
            }
        }

        protected void bFollowers_Click(object sender, EventArgs e)
        {
            var follower = SessionManager.SeeFollowedsUser(Context);
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

        protected void bImage1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var images = SessionManager.FindImageProfileDetailsByUserId(Context);
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
                var images = SessionManager.FindImageProfileDetailsByUserId(Context);
                if (images.Count<2) {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Error/InternalError.aspx"));
                } else {
                    Response.Redirect(Response.
                            ApplyAppPathModifier("~/Pages/Image/ImageProfile.aspx?imageId=" + images[1].imageId));
                }
            }
        }

        protected void bImage3_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var images = SessionManager.FindImageProfileDetailsByUserId(Context);
                if (images.Count<3)
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

        void gvFollowed_RowDataBound(Object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";

            }

        }
    }
}