<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="SearchImage.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Image.SearchImage" 
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <center>
                <asp:Label ID="lSearch" runat="server" meta:resourcekey="lSearch"></asp:Label>
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server"  meta:resourcekey="btnSearch" OnClick="btnSearch_Click" />
            </center>
        </div>
        <div>
            <center>
                 <asp:CheckBox ID="cbCategory" runat="server"  meta:resourcekey="cbCategory"/>
                <asp:DropDownList ID="categoryU" runat="server" Width="100px" OnSelectedIndexChanged="comboCategorySelectedIndexChanged">                       
                    </asp:DropDownList>
            </center>
        </div>
        <center>
            <asp:Button ID="btnUpload" runat="server" meta:resourcekey="btnUpload" OnClick="btnUpload_Click" />
        </center>
    </form>
</asp:Content>
