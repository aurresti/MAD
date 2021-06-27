using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.Properties;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class ShowImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            string text = Request.Params.Get("text");
            string category = Request.Params.Get("category");
            bool filter = "True".Equals(Request.Params.Get("filter"));
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.Photogram_defaultCound;
            }

            ImageBlock result = SessionManager.FindImageProfileDetails(text,
                        category, filter, startIndex, count);
            List<ImageInfo> list = result.Images;

            if (list.Count == 0) 
            {
                lblNotFound.Visible = true;
                return;
            }

            gridMembersList.DataSource = list;
            gridMembersList.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = String.Format("/Pages/Image/ShowImages.aspx?text={0}&category={1}&filter={2}&startIndex={3}&count={4}",
                    text, category, filter, (startIndex - count), count);
                
                //url = Settings.Default.Photogram_applicationURL + url;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (result.existMoreImages)
            {
                   String url = String.Format("/Pages/Image/ShowImages.aspx?text={0}&category={1}&filter={2}&startIndex={3}&count={4}",
                    text, category, filter, (startIndex + count), count);

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }
    }
}