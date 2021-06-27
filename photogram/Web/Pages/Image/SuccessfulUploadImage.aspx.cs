using System;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class SuccessfulUploadImage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string imgUrl = Request.Params.Get("imgUrl");

            Image1.ImageUrl = imgUrl;

            Image1.Visible = true;

        }
    }
}