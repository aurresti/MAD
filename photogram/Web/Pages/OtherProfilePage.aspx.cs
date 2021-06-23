using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class OtherProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lIsFollow.Visible = false;
        }

        protected void bFollowed_Click(object sender, EventArgs e)
        {

        }

        protected void bFollowers_Click(object sender, EventArgs e)
        {

        }

        protected void bFollow_Click(object sender, EventArgs e)
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