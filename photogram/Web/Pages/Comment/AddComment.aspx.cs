using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Comment
{
    public partial class AddComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void bComment_Click(object sender, EventArgs e)
        {
            string valor = Request.QueryString["imageId"];
            long id = (long)Convert.ToDouble(valor);
            SessionManager.CreateComment(Context, id, tbComment.Text);
            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/HomePage.aspx?index=0"));
        }
    }
}