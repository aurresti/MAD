using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class ImageProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try {
                    string valor = Request.QueryString["imageId"];
                    long id = (long)Convert.ToDouble(valor);
                    var imageShow = SessionManager.FindImageProfileDetailsById(id);
                    UserProfile user = SessionManager.FindUserProfileDetailsUser((long)imageShow.User);
                    hlUser.Text = user.FirstName;
                    UserSession userSession =
                        (UserSession)Context.Session["userSession"];
                    //UserSession userSession = SessionManager.GetUserSession(Context);
                    if (SessionManager.IsUserAuthenticated(Context))
                    {
                        if ((long)imageShow.User == userSession.UserProfileId)
                        {
                            hlUser.NavigateUrl = "~/Pages/ProfilePage.aspx";
                        }
                        else
                        {
                            hlUser.NavigateUrl = "~/Pages/OtherProfilePage.aspx?userId=" + (long)imageShow.User;
                        }
                    }
                    else {
                        hlUser.NavigateUrl = "~/Pages/OtherProfilePage.aspx?userId=" + (long)imageShow.User;
                    }
                    String exif = imageShow.Exif;
                    String[] info = exif.Split('/');
                    lTitleContent.Text = imageShow.Title;
                    lCategoryContent.Text = SessionManager.FindCategoryName(imageShow.Category);
                    lDescriptionContent.Text = imageShow.Descripction;
                    lISOContent.Text = info[0];
                    lDiaphragmContent.Text = info[1];
                    lExpositionContent.Text = info[2];
                    lWhiteContent.Text = info[3];
                    Image1.ImageUrl = imageShow.File;
                } catch (InstanceNotFoundException) {
                    Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/Error/InternalError.aspx"));
                }
            }
        }

        protected void bDelete_Click(object sender, EventArgs e)
        {
            
        }
    }
}