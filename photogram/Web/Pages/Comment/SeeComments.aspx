<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="SeeComments.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Comment.SeeComments" %>
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
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/HomePage.aspx" meta:resourcekey="lnkHome" />
            
            <center>
                
                <asp:Label ID="lSeeComment" runat="server" meta:resourcekey="lBeginComment"></asp:Label>
                
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/ProfilePage.aspx" meta:resourcekey="lnkMyProfile" />
            <center>
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" meta:resourcekey="lnkExit" />

        </div>
        <div>                
            <center>
                <asp:Button ID="bComment" runat="server" meta:resourcekey="bComment" OnClick="bComment_Click" />
                <asp:Button ID="bTurn" runat="server" meta:resourcekey="bTurn" OnClick="bTurn_Click" />
            </center>
        </div>
     </form>
</asp:Content>
