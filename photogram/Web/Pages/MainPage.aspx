<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.MainPage" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="MainForm" method="POST" runat="server">
    <div>
        <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="lnkHome" />
        
            <center>
            <asp:Label ID="lProfile" runat="server" meta:resourcekey="lProfile"></asp:Label>
            
            <asp:Button ID="bFollowed" runat="server" meta:resourcekey="bFollowed" />
            <asp:Button ID="bFollowers" runat="server" meta:resourcekey="bFollowers" />
            </center>
    </div>
    <div>
        <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="lnkMyProfile" />
        <center>
            <asp:Button ID="bUpgrade" runat="server" meta:resourcekey="bUpgrade"  />
            <asp:Button ID="bChangePassword" runat="server" meta:resourcekey="bChangePassword" OnClick="bChangePassword_Click"  />
        </center>
    </div>
    <div>
        <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="lnkExit" />
        <center>
            <asp:Image ID="Image1" runat="server" Height="16px" />
            <asp:Image ID="Image2" runat="server" />
            <asp:Image ID="Image3" runat="server" />
        </center>
    </div>
    <center>
    <div id="form">
        
            
        <asp:HyperLink ID="hlImage1" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="hlImage" />
        <asp:HyperLink ID="hlImage2" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="hlImage" />
        <asp:HyperLink ID="hlImage3" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="hlImage" />
            
    </div>
    <div>
    </div>
    <div>
    </div>
    </center>
    </form>
</asp:Content>
