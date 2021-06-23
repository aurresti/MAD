using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Photogram.Web.HTTP.View.ApplicationObjects
{
    /// <summary>
    /// Summary description for Countries
    /// </summary>
    public class Category
    {

        /* 
         * In a more realistic application, these values could be read from a 
         * database in the "static" constructor.
         */
        private static readonly ArrayList categories_es = new ArrayList();
        private static readonly ArrayList categories_en = new ArrayList();
        private static readonly ArrayList categories_gl = new ArrayList();
        private static readonly ArrayList categoriesCodes = new ArrayList();
        private static readonly Hashtable categories = new Hashtable();


        /* Access modifiers are not allowed on static constructors
         * so if we want to prevent that anybody creates instances 
         * of this class we must do the following ...
         */
        private Category() { }

        static Category()
        {
            #region set the countries

            //categories_es.Add(new ListItem("-", "no"));
            categories_es.Add(new ListItem("Fauna", "an"));
            categories_es.Add(new ListItem("Paisaje", "la"));
            categories_es.Add(new ListItem("Moda", "fa"));

            //categories_en.Add(new ListItem("-", "no"));
            categories_en.Add(new ListItem("Animals", "an"));
            categories_en.Add(new ListItem("Fashion", "fa"));
            categories_en.Add(new ListItem("Landscape", "la"));
            
            //categories_gl.Add(new ListItem("-", "no"));
            categories_gl.Add(new ListItem("Fauna", "an"));
            categories_gl.Add(new ListItem("Paisaxe", "la"));
            categories_gl.Add(new ListItem("Moda", "fa"));

            //categoriesCodes.Add("no");
            categoriesCodes.Add("an");
            categoriesCodes.Add("la");
            categoriesCodes.Add("fa");

            categories.Add("es", categories_es);
            categories.Add("en", categories_en);
            categories.Add("gl", categories_gl);


            #endregion

        }

        public static ICollection GetCategoryCodes()
        {
            return categoriesCodes;
        }

        public static ArrayList GetCategories(String languageCode)
        {
            ArrayList lang = (ArrayList)categories[languageCode];

            if (lang != null)
            {
                return lang;
            }
            else
            {
                return (ArrayList)categories["en"];
            }
        }
    }
}