using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class SuccessfulUploadImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string imgUrl = Request.Params.Get("imgUrl");

            Image1.ImageUrl = imgUrl;

            Image1.Visible = true;

        }
    }
}