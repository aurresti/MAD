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
                <asp:gridview id="gvImage" 
                autogeneratecolumns="false"
                allowpaging="true"
                runat="server">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Titulo"/>
                    <asp:HyperLinkField DataNavigateUrlFields="User" DataNavigateUrlFormatString="OtherProfilePage.aspx?userId={0}"
                    DataTextField="User" NavigateUrl="OtherProfilePage.aspx" HeaderText="Autor"/>
                    <asp:BoundField DataField="Date" HeaderText="Fecha"/>
                    <asp:BoundField DataField="Descripction" HeaderText="Descripcion"/>
                    <asp:BoundField DataField="Exif" HeaderText="Exif"/>
                    <asp:BoundField DataField="Likes" HeaderText="Likes"/>
                </Columns>
            </asp:gridview>
                <asp:Label ID="lblNotFound" runat="server" meta:resourcekey="lblNotFound"></asp:Label>
            </center>
            <center>
                <asp:Button ID="btnUpload" runat="server" meta:resourcekey="btnUpload" OnClick="btnUpload_Click" />
            </center>
        </div>
    </form>
    
</asp:Content>
