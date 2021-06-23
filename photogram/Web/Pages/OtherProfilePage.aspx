<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="OtherProfilePage.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.OtherProfilePage" meta:resourcekey="Page" %>

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
        <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/User/HomePage.aspx" meta:resourcekey="lnkHome" />
        
            <center>
            <asp:Label ID="lProfile" runat="server" meta:resourcekey="lProfile"></asp:Label>
            
            <asp:Button ID="bFollowed" runat="server" meta:resourcekey="bFollowed" OnClick="bFollowed_Click" />
            <asp:Button ID="bFollowers" runat="server" meta:resourcekey="bFollowers" OnClick="bFollowers_Click" />
            </center>
    </div>
    <div>
        <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/User/ProfilePage.aspx" meta:resourcekey="lnkMyProfile" />
        <center>
            <asp:Button ID="bFollow" runat="server" meta:resourcekey="bFollow" OnClick="bFollow_Click"  />
            <asp:Label ID="lIsFollow" runat="server" meta:resourcekey="lIsFollow"></asp:Label>
        </center>
    </div>
    <div>
        <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" meta:resourcekey="lnkExit" />
        <center>
            <asp:Image ID="Image1" runat="server" Height="16px" />
            <asp:Image ID="Image2" runat="server" />
            <asp:Image ID="Image3" runat="server" />
        </center>
    </div>
    <center>
    <div id="form">
        
            
        <asp:Button ID="bImage1" runat="server" meta:resourcekey="bImage" OnClick="bImage1_Click"  />
        <asp:Button ID="bImage2" runat="server" meta:resourcekey="bImage" OnClick="bImage2_Click"  />
        <asp:Button ID="bImage3" runat="server" meta:resourcekey="bImage" OnClick="bImage3_Click"  />
            
    </div>
    <div>
    </div>
    <div>
    </div>
    </center>
    </form>
</asp:Content>

