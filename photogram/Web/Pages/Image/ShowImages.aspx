<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="ShowImages.aspx.cs"
    Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Image.ShowImages" meta:resourcekey="Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/HomePage.aspx?index=0" meta:resourcekey="lnkHome" />
    <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/ProfilePage.aspx" meta:resourcekey="lnkMyProfile" />
            <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" meta:resourcekey="lnkExit" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form runat="server">  
        <div>
                <center>
                    <asp:GridView ID="gridImageList" 
    AutoGenerateColumns="False" 
                runat="server"  
                allowpaging="true"
                onrowcommand="gridImageList_RowCommand"
                AutoPostBack="True"
               >
            <Columns>
                <asp:BoundField DataField="ImageId" HeaderText="ImageId">
                    <HeaderStyle CssClass="hidden-field" />
                    <ItemStyle CssClass="hidden-field"/>
                    </asp:BoundField>
                        <asp:ImageField DataImageUrlField="url"  DataImageUrlFormatString="{0}"  
                            HeaderText ="Imagen" ControlStyle-Width="200px" ControlStyle-Height = "250px">
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
                    " CommandName="More" runat="server" Text="like" AutoPostBack="True"/>
                </ItemTemplate>
            </asp:TemplateField> 
            </Columns>
            </asp:GridView>  
                    <asp:Label ID="lblNotFound" runat="server" meta:resourcekey="lblNotFound"></asp:Label>
                </center>
            </div>
        </form>
        <br />
        <!-- "Previous" and "Next" links. -->
        <div class="previousNextLinks">
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                    Visible="False"></asp:HyperLink>
            </span><span class="nextLink">
                <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
            </span>
        </div>
</asp:Content>
