<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="AddComment.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Comment.AddComment" meta:resourcekey="Page"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
    <asp:Label ID="lTitleProfile" runat="server" meta:resourcekey="lTitleProfile"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
         <div>
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/HomePage.aspx?index=0" meta:resourcekey="lnkHome" />
            
            <center>
                
                <asp:Label ID="lBeginComment" runat="server" meta:resourcekey="lBeginComment"></asp:Label>
                
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/ProfilePage.aspx" meta:resourcekey="lnkMyProfile" />
            <center>
                
                <asp:TextBox ID="tbComment" runat="server"></asp:TextBox>
                
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" meta:resourcekey="lnkExit" />

        </div>
        <div>                
            <center>
                <asp:Button ID="bComment" runat="server" meta:resourcekey="bComment" OnClick="bComment_Click" />
            </center>
        </div>
     </form>
</asp:Content>
