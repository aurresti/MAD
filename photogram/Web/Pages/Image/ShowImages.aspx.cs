using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class ShowImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string text = Request.Params.Get("text");
            string category = Request.Params.Get("category");
            bool filter = "True".Equals(Request.Params.Get("filter"));

            ImageBlock result = SessionManager.FindImageProfileDetails(text,
                        category, filter, 0);
            List<ImageInfo> list = result.Images;
            if (list.Count == 0) 
            {
                lblNotFound.Visible = true;
                return;
            }

            gridMembersList.DataSource = list;
            gridMembersList.DataBind();
        }
    }
}