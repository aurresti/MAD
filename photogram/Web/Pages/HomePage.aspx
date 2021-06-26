<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true"
    Codebehind="HomePage.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.HomePage"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
    <asp:Label ID="lTitleHome" runat="server" meta:resourcekey="lTitleHome"></asp:Label>
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
                <asp:Label ID="lSearch" runat="server" meta:resourcekey="lSearch"></asp:Label>
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server"  meta:resourcekey="btnSearch" OnClick="btnSearch_Click" />
            </center>
        </div>
        <div>
            <center>
                <asp:CheckBox ID="cbCategory" runat="server"  meta:resourcekey="cbCategory" OnCheckedChanged="cbCategory_CheckedChanged"/>
                <asp:DropDownList ID="comboCategory" runat="server" AutoPostBack="True"
                            Width="100px" meta:resourcekey="comboCategoryResource1"
                            OnSelectedIndexChanged="comboCategorySelectedIndexChanged">
                </asp:DropDownList>
            </center>
        </div>
        <div>
            <center>
                <asp:GridView ID="gridMembersList" 
AutoGenerateColumns="False" 
            onrowdatabound="gridMembersList_RowDataBound"
            runat="server"  
            allowpaging="true"
            OnPageIndexChanging ="gridMembersList_PageIndexChanging"
            onrowcommand="gridMembersList_RowCommand" OnSelectedIndexChanged="gridMembersList_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="ImageId" HeaderText="ImageId"/>
                    <asp:ImageField DataImageUrlField="Url" DataImageUrlFormatString="~/img/{0}" 
                        HeaderText ="Imagen"> 
                    </asp:ImageField>
                    <asp:BoundField DataField="Title" HeaderText="Titulo"/>
                    <asp:BoundField DataField="ExifInfo" HeaderText="Exif"/>
                    <asp:BoundField DataField="CategoryName" HeaderText="Categoria"/>
                    <asp:BoundField DataField="Date" HeaderText="Fecha"/>
                    <asp:BoundField DataField="Description" HeaderText="Descripcion"/>
                    <asp:HyperLinkField DataNavigateUrlFields="userId" DataNavigateUrlFormatString="OtherProfilePage.aspx?userId={0}"
                    DataTextField="Name" NavigateUrl="OtherProfilePage.aspx" HeaderText="Autor"/>
                    <asp:HyperLinkField DataNavigateUrlFields="imageId" DataNavigateUrlFormatString="~/Pages/Comment/AddComment.aspx?imageId={0}"
                    Text="Comentar" NavigateUrl="OtherProfilePage.aspx" HeaderText="Añadir Comentarios"/>
                    <asp:HyperLinkField DataNavigateUrlFields="imageId" DataNavigateUrlFormatString="~/Pages/Comment/SeeComments.aspx?imageId={0}"
                    Text="Ver Comentarios" NavigateUrl="OtherProfilePage.aspx" HeaderText="Ver Comentarios"/>
                    <asp:BoundField DataField="numLikes" HeaderText="Likes"/>
        
        <asp:TemplateField HeaderText="Dar Like">
        <ItemTemplate>
            <asp:Button ID="btnViewmore"  
            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            " CommandName="More" runat="server" Text="Like" />
        </ItemTemplate>
        </asp:TemplateField> 
        </Columns>
        </asp:GridView>  
                <asp:Label ID="lblNotFound" runat="server" meta:resourcekey="lblNotFound"></asp:Label>
            </center>
            <center>
                <asp:Button ID="btnUpload" runat="server" meta:resourcekey="btnUpload" OnClick="btnUpload_Click" />
            </center>
        </div>
    </form>
    
</asp:Content>
