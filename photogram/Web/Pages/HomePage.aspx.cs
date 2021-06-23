using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using System.Web.Security;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNotFound.Visible = false;
            if (!IsPostBack)
            {
                /* Get current language and country from browser */
                String defaultLanguage =
                    GetLanguageFromBrowserPreferences();

                /* Combo box initialization */
                UpdateComboCategory(defaultLanguage, "no");
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
            this.comboCategory.DataSource = Category.GetCategories(selectedLanguage);
            this.comboCategory.DataTextField = "text";
            this.comboCategory.DataValueField = "value";
            this.comboCategory.DataBind();
            this.comboCategory.SelectedValue = selectedCategory;
        }

        protected void comboCategorySelectedIndexChanged(object sender, EventArgs e)
        {
            //Cambiar defaultLanguage por donde se almacena la informacion del idioma
            String defaultLanguage =
                    GetLanguageFromBrowserPreferences();
            this.UpdateComboCategory(defaultLanguage, comboCategory.SelectedValue);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/UploadImage.aspx"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    /*SessionManager.Login(Context, txtLogin.Text,
                        txtPassword.Text, cbCategory.Checked);*/

                }
                catch (InstanceNotFoundException)
                {
                    lblNotFound.Visible = true;
                }
            }
        }

        protected void cbCategory_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}