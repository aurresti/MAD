<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="SeeComments.aspx.cs" 
    Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Comment.SeeComments" meta:resourcekey="Page" %>
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
            
            <center>
                
                <asp:Label ID="lSeeComment" runat="server" meta:resourcekey="lSeeComment"></asp:Label>
                
            </center>
        </div>
        <div>
            <center>
                <asp:gridview id="gvComment" 
                autogeneratecolumns="false"
                allowpaging="true"
                onrowcommand="gvComment_RowCommand"
                OnPageIndexChanging ="gvComment_PageIndexChanging"
                runat="server" OnSelectedIndexChanged="gvComment_SelectedIndexChanged1">
                    
                <Columns>
                    <asp:BoundField DataField="commentId" HeaderText="commentId">
                        <HeaderStyle CssClass="hidden-field" />
                        <ItemStyle CssClass="hidden-field"/>
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="userId" DataNavigateUrlFormatString="~/Pages/OtherProfilePage.aspx?userId={0}"
                    DataTextField="Login" NavigateUrl="~/Pages/OtherProfilePage.aspx" HeaderText="Autor"/>
                    <asp:BoundField DataField="Date" HeaderText="Fecha"/>
                    <asp:BoundField DataField="Description" HeaderText="Descripcion"/>
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:Button ID="btnWantEdit"  
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
                            " CommandName="WantEdit" runat="server" Text="Edit" AutoPostBack="True"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:Button ID="btnWantDel"  
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
                            " CommandName="WantDel" runat="server" Text="Del" AutoPostBack="True"/>
                        </ItemTemplate>
                    </asp:TemplateField>
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
