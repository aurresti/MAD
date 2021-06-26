<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="SeeComments.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Comment.SeeComments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
    <asp:Label ID="lTitleProfile" runat="server" meta:resourcekey="lTitleProfile"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
    <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/HomePage.aspx?index=0" meta:resourcekey="lnkHome" />
            <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/ProfilePage.aspx" meta:resourcekey="lnkMyProfile" />
            <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" meta:resourcekey="lnkExit" />

        </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
         <div>
            
            <center>
                
                <asp:Label ID="lSeeComment" runat="server" meta:resourcekey="lSeeComment"></asp:Label>
                
            </center>
        </div>
        <div>
            <center>
                <asp:gridview id="gvComment" 
                autogeneratecolumns="false"
                allowpaging="true"
                OnPageIndexChanging ="gvComment_PageIndexChanging"
                runat="server" OnSelectedIndexChanged="gvComment_SelectedIndexChanged1">
                    
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="userId" DataNavigateUrlFormatString="~/Pages/OtherProfilePage.aspx?userId={0}"
                    DataTextField="Login" NavigateUrl="~/Pages/OtherProfilePage.aspx" HeaderText="Autor"/>
                    <asp:BoundField DataField="Date" HeaderText="Fecha"/>
                    <asp:BoundField DataField="Description" HeaderText="Descripcion"/>
                </Columns>
            </asp:gridview>
            </center>
        </div>
        <div>

        </div>
        <div>                
            <center>
                <asp:Button ID="bComment" runat="server" meta:resourcekey="bComment" OnClick="bComment_Click" />
            </center>
        </div>
     </form>
</asp:Content>
