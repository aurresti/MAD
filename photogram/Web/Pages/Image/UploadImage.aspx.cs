using Es.Udc.DotNet.Photogram.Model.CategoryService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Web;
using System.Collections.Generic;
using System.IO;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class UploadImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                /* Get current language and country from browser */
                String defaultLanguage =
                    GetLanguageFromBrowserPreferences();

                /* Combo box initialization */
                UpdateComboCategory(defaultLanguage, "an");

            }
        }

        private String GetLanguageFromBrowserPreferences()
        {
            String language;
            CultureInfo cultureInfo =
                CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
            language = cultureInfo.TwoLetterISOLanguageName;
            LogManager.RecordMessage("Preferred language of user" +
                                     " (based on browser preferences): " + language);
            return language;
        }

        private String GetCountryFromBrowserPreferences()
        {
            String country;
            CultureInfo cultureInfo =
                CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

            if (cultureInfo.IsNeutralCulture)
            {
                country = "";
            }
            else
            {
                // cultureInfoName is something like en-US
                String cultureInfoName = cultureInfo.Name;
                // Gets the last two caracters of cultureInfoname
                country = cultureInfoName.Substring(cultureInfoName.Length - 2);

                LogManager.RecordMessage("Preferred region/country of user " +
                                         "(based on browser preferences): " + country);
            }

            return country;
        }

        private void UpdateComboCategory(String selectedLanguage, String selectedCategory)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICategoryService categoryService = iocManager.Resolve<ICategoryService>();

            /* Get Accounts Info */
            List<Model.Category> list =
                categoryService.FindCategories();
            this.categoryU.DataSource = list;
            this.categoryU.DataTextField = "name";
            this.categoryU.DataValueField = "categoryId";
            this.categoryU.DataBind();
            this.categoryU.SelectedValue = selectedCategory;
        }

        protected void comboCategorySelectedIndexChanged(object sender, EventArgs e)
        {
            //Cambiar defaultLanguage por donde se almacena la informacion del idioma
            String defaultLanguage =
                    GetLanguageFromBrowserPreferences();
            this.UpdateComboCategory(defaultLanguage, categoryU.SelectedValue);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    //Save image

                    string folderPath = Server.MapPath("~/img/");

                    FileInfo fi = new FileInfo(FileUpload1.FileName);

                    //Check whether Directory (Folder) exists.
                    if (!Directory.Exists(folderPath))
                    {
                        //If Directory (Folder) does not exists Create it.
                        Directory.CreateDirectory(folderPath);
                    }

                    String url = "~/img/" + Path.GetFileName(tbTitle.Text + fi.Extension);

                    String exif = tbSensibility.Text + "/" + tbDiaphragm.Text + "/" + tbExposition.Text + "/" + tbDistance.Text;

                    SessionManager.RegisterImage(Context, tbTitle.Text, tbDescription.Text,
                        exif, Convert.ToInt32(categoryU.SelectedValue), url);

                    //Save the File to the Directory (Folder).
                    
                    FileUpload1.SaveAs(folderPath + Path.GetFileName(tbTitle.Text + fi.Extension));

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/SuccessfulUploadImage.aspx?imgUrl=" + url));

                }
                catch (DuplicateInstanceException)
                {
                    //lblLoginError.Visible = true;
                }
            }
        }
            
    }
}