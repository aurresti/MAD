//using System;
//using System.Collections;
//using System.Web.UI.WebControls;

//namespace Es.Udc.DotNet.Photogram.Web.HTTP.View.ApplicationObjects
//{
//    /// <summary>
//    /// Summary description for Countries
//    /// </summary>
//    public class Category
//    {

//        /* 
//         * In a more realistic application, these values could be read from a 
//         * database in the "static" constructor.
//         */
//        private static readonly ArrayList categories_es = new ArrayList();
//        private static readonly ArrayList categories_en = new ArrayList();
//        private static readonly ArrayList categories_gl = new ArrayList();
//        private static readonly ArrayList categoriesCodes = new ArrayList();
//        private static readonly Hashtable categories = new Hashtable();


//        /* Access modifiers are not allowed on static constructors
//         * so if we want to prevent that anybody creates instances 
//         * of this class we must do the following ...
//         */
//        private Category() { }

//        static Category()
//        {
//            #region set the countries

//            //categories_es.Add(new ListItem("-", "no"));
//            categories_es.Add(new ListItem("Fauna", "Fauna"));
//            categories_es.Add(new ListItem("Paisaje", "Paisaje"));
//            categories_es.Add(new ListItem("Moda", "Moda"));

//            //categories_en.Add(new ListItem("-", "no"));
//            categories_en.Add(new ListItem("Animals", "Fauna"));
//            categories_en.Add(new ListItem("Fashion", "Paisaje"));
//            categories_en.Add(new ListItem("Landscape", "Moda"));
            
//            //categories_gl.Add(new ListItem("-", "no"));
//            categories_gl.Add(new ListItem("Fauna", "Fauna"));
//            categories_gl.Add(new ListItem("Paisaxe", "Paisaje"));
//            categories_gl.Add(new ListItem("Moda", "Moda"));

//            //categoriesCodes.Add("no");
//            categoriesCodes.Add("Fauna");
//            categoriesCodes.Add("Paisaje");
//            categoriesCodes.Add("Moda");

//            categories.Add("es", categories_es);
//            categories.Add("en", categories_en);
//            categories.Add("gl", categories_gl);


//            #endregion

//        }

//        public static ICollection GetCategoryCodes()
//        {
//            return categoriesCodes;
//        }

//        public static ArrayList GetCategories(String languageCode)
//        {
//            ArrayList lang = (ArrayList)categories[languageCode];

//            if (lang != null)
//            {
//                return lang;
//            }
//            else
//            {
//                return (ArrayList)categories["en"];
//            }
//        }
//    }
//}