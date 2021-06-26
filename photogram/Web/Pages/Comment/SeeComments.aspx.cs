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
using Es.Udc.DotNet.Photogram.Model;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Comment
{

    public partial class SeeComments : System.Web.UI.Page
    {
        protected struct Info{
            public String Login { get; set; }
            public String Description { get; set; }
            public DateTime Date { get; set; }
            public long UserId { get; set; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string valor = Request.QueryString["imageId"];
            long id = (long)Convert.ToDouble(valor);
            List<Castle.Core.Pair<Model.Comment, UserAccount>>  comments = SessionManager.SeeComment(id);
            Info aux = new Info();
            List<Info> auxList = new List<Info>();
            foreach (Castle.Core.Pair<Model.Comment, UserAccount> i in comments)
            {
                aux.Date = i.First.date;
                aux.Description = i.First.comment1;
                aux.Login = i.Second.loginName;
                aux.UserId = i.Second.userId;
                auxList.Add(aux);
            }
            gvComment.DataSource = auxList;
            gvComment.DataBind();
        }

        protected void bComment_Click(object sender, EventArgs e)
        {
            string valor = Request.QueryString["imageId"];
            long id = (long)Convert.ToDouble(valor);
            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Comment/AddComment.aspx?imageId=" + id));
        }

        protected void gvImage_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}