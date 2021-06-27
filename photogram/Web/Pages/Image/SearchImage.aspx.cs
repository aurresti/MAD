using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.Photogram.Model.CategoryService;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class SearchImage : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    /* Get the Service */
        //    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
        //    ICategoryService categoryService = iocManager.Resolve<ICategoryService>();

        //    /* Get Accounts Info */
        //    List<Model.Category> list =
        //        categoryService.FindCategories();
        //    this.categoryU.DataSource = list;
        //    this.categoryU.DataTextField = "name";
        //    this.categoryU.DataValueField = "name";
        //    this.categoryU.DataBind();

        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /* Get current language and country from browser */
                String defaultLanguage =
                    GetLanguageFromBrowserPreferences();
                String defaultCountry =
                    GetCountryFromBrowserPreferences();

                /* Combo box initialization */
                UpdateComboCategory(defaultLanguage, "Fauna");
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
            this.categoryU.DataValueField = "name";
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

        protected void btnSearch_Click(object sender, EventArgs e) 
        {
            if (Page.IsValid)
            {

                String url = String.Format("./ShowImages.aspx?text={0}&category={1}&filter={2}", tbSearch.Text, this.categoryU.SelectedValue, cbCategory.Checked);
                Response.Redirect(Response.ApplyAppPathModifier(url));
                
            }
        }

    }
}