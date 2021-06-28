using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Comment
{
    public partial class UpdateComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string valor = Request.QueryString["commentId"];
                long commentId = (long)Convert.ToDouble(valor);

                Model.Comment comment = SessionManager.FindComment(commentId);

                tbComment2.Text = comment.comment1;
            }
        }

        protected void bComment2_Click(object sender, EventArgs e)
        {
            string valor = Request.QueryString["imageId"];
            long imageId = (long)Convert.ToDouble(valor);

            string valor2 = Request.QueryString["commentId"];
            long commentId = (long)Convert.ToDouble(valor2);

            SessionManager.UpdateComment(Context, imageId, commentId, tbComment2.Text);
            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/SearchImage.aspx"));
        }
    }
}