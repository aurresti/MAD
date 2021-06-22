<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true"
    Codebehind="ImageProfile.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Image.ImageProfile"
    meta:resourcekey="Page" %>

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
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="lnkHome" />
            <center>
                <asp:Label ID="lTitle" runat="server" meta:resourcekey="lTitle"></asp:Label>
                <asp:TextBox ID="tbTitle" runat="server"></asp:TextBox>
                <asp:Label ID="lCategory" runat="server" meta:resourcekey="lCategory"></asp:Label>
                <asp:TextBox ID="tbCategory" runat="server"></asp:TextBox>
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="lnkMyProfile" />
            <center>
                <asp:Label ID="lSensibility" runat="server" meta:resourcekey="lSensibility"></asp:Label>
                <asp:TextBox ID="tbSensibility" runat="server"></asp:TextBox>
                <asp:Label ID="lDiaphragm" runat="server" meta:resourcekey="lDiaphragm"></asp:Label>
                <asp:TextBox ID="tbDiaphragm" runat="server"></asp:TextBox>
                <asp:Label ID="lExposition" runat="server" meta:resourcekey="lExposition"></asp:Label>
                <asp:TextBox ID="tbExposition" runat="server"></asp:TextBox>
                <asp:Label ID="lDistance" runat="server" meta:resourcekey="lDistance"></asp:Label>
                <asp:TextBox ID="tbDistance" runat="server"></asp:TextBox>
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="lnkExit" />
            <center>
                <asp:Label ID="lDescription" runat="server" meta:resourcekey="lDescription"></asp:Label>
                <asp:TextBox ID="tbDescription" runat="server"></asp:TextBox>
            </center>
        </div>
     </form>
</asp:Content>
