<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true"
    Codebehind="HomePage.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.HomePage"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
    <asp:Label ID="lTitleHome" runat="server" meta:resourcekey="lTitleHome"></asp:Label>
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
                <asp:Label ID="lSearch" runat="server" meta:resourcekey="lSearch"></asp:Label>
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server"  meta:resourcekey="btnSearch" OnClick="btnSearch_Click" />
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/ProfilePage.aspx" meta:resourcekey="lnkMyProfile" />
            <center>
                <asp:CheckBox ID="cbCategory" runat="server"  meta:resourcekey="cbCategory" OnCheckedChanged="cbCategory_CheckedChanged"/>
                <asp:DropDownList ID="comboCategory" runat="server" AutoPostBack="True"
                            Width="100px" meta:resourcekey="comboCategoryResource1"
                            OnSelectedIndexChanged="comboCategorySelectedIndexChanged">
                </asp:DropDownList>
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" meta:resourcekey="lnkExit" />
            <center>
                <asp:Label ID="lblNotFound" runat="server" meta:resourcekey="lblNotFound"></asp:Label>
            </center>
            <center>
                <asp:Button ID="btnUpload" runat="server" meta:resourcekey="btnUpload" OnClick="btnUpload_Click" />
            </center>
        </div>
    </form>
    
</asp:Content>
