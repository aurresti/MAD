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
using System.Collections.Generic;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNotFound.Visible = false;
            if (!IsPostBack)
            {
                btnAfter.Visible = false;
                btnBefore.Visible = false;
                /* Get current language and country from browser */
                String defaultLanguage =
                    GetLanguageFromBrowserPreferences();

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
                        ApplyAppPathModifier("~/Pages/Image/UploadImage.aspx"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string valor = Request.QueryString["index"];
                    int id = (int)Convert.ToDouble(valor);
                    ImageBlock result = SessionManager.FindImageProfileDetails(tbSearch.Text, 
                        comboCategory.SelectedValue, cbCategory.Checked, id);
                    /*List<ImageProfile> images = new List<ImageProfile>();
                    for (int i = 0; i < result.Count; i++)
                    {
                        images.Add(SessionManager.FindImageProfileDetailsById(result[i].imageId));
                    }*/
                    if (result.Images.Count >= 1)
                    {
                        /*gvImage.Visible = true;
                        gvImage.DataSource = result.Images;
                        gvImage.DataBind();*/
                        gridMembersList.DataSource = result.Images;
                        gridMembersList.DataBind();
                    }
                    else {
                        //gvImage.Visible = false;
                        lblNotFound.Visible = true;
                    }

                    if (result.existMoreImages)
                    {
                        btnAfter.Visible = true;
                    }
                    else
                    {
                        btnAfter.Visible = false;
                    }

                    if (result.Images.Count <= 5)
                    {
                        btnAfter.Visible = false;
                        btnBefore.Visible = false;
                    }
                    else
                    {
                        btnBefore.Visible = true;
                    }

                }
                catch (InstanceNotFoundException)
                {
                    lblNotFound.Visible = true;
                }
            }
        }

        void gvImage_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            if (e.CommandName == "Like")
            {
                //first find the button that got clicked
                var clickedButton = e.CommandSource as Button;
                //find the row of the button
                var clickedRow = clickedButton.NamingContainer as GridViewRow;
                //now as the UserName is in the BoundField, access it using the cell index.
                var clickedUserName = clickedRow.Cells[0].Text;
                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/UploadImage.aspx"));
            } 
        }

        protected void gridMembersList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "More")
            {
                if (SessionManager.IsUserAuthenticated(Context)) {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gridMembersList.Rows[index];
                    TableCell cell = row.Cells[0];
                    long imageId = (long)Convert.ToDouble(cell.Text);
                    if (!SessionManager.ExistsLike(Context, imageId)) {
                        SessionManager.CreateLike(Context, imageId);
                        row.Cells[9].Text = ((int)Convert.ToDouble(row.Cells[9].Text) + 1).ToString();
                    } else {
                        SessionManager.DeleteLike(Context, imageId);
                        row.Cells[9].Text = ((int)Convert.ToDouble(row.Cells[9].Text) - 1).ToString();
                    }
                } else {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
                }
            }
        }

        protected void cbCategory_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void gvImage_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void btnBefore_Click(object sender, EventArgs e)
        {

        }

        protected void btnAfter_Click(object sender, EventArgs e)
        {

        }

        protected void gridMembersList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}