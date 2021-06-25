<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.ProfilePage" meta:resourcekey="Page" %>

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
        <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/HomePage.aspx?index=0" meta:resourcekey="lnkHome" />
        
            <center>
            <asp:Label ID="lProfile" runat="server" meta:resourcekey="lProfile"></asp:Label>
            
            <asp:Button ID="bFollowed" runat="server" meta:resourcekey="bFollowed" OnClick="bFollowed_Click" />
            <asp:Button ID="bFollowers" runat="server" meta:resourcekey="bFollowers" OnClick="bFollowers_Click" />
            </center>
    </div>
    <div>
        <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/ProfilePage.aspx" meta:resourcekey="lnkMyProfile" />
        <center>
            <asp:gridview id="gvFollowed" 
                autogeneratecolumns="false"
                allowpaging="true"
                runat="server">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="userId" DataNavigateUrlFormatString="OtherProfilePage.aspx?userId={0}"
                    DataTextField="firstName" NavigateUrl="OtherProfilePage.aspx" HeaderText="Usuarios"/>
                </Columns>
            </asp:gridview>
            <asp:gridview id="gvFollower" 
                autogeneratecolumns="false"
                allowpaging="true"
                runat="server">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="userId" DataNavigateUrlFormatString="OtherProfilePage.aspx?userId={0}"
                    DataTextField="firstName" NavigateUrl="OtherProfilePage.aspx" HeaderText="Usuarios"/>
                </Columns>
            </asp:gridview>
            <asp:Button ID="bUpgrade" runat="server" meta:resourcekey="bUpgrade" OnClick="bUpgrade_Click"  />
            <asp:Button ID="bChangePassword" runat="server" meta:resourcekey="bChangePassword" OnClick="bChangePassword_Click"  />
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
