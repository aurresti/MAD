<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true"
    Codebehind="UploadImage.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Image.UploadImage"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
    <asp:Label ID="lTitleUpload" runat="server" meta:resourcekey="lTitleUpload"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
     <form id="form1" runat="server">
         <div>
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/HomePage.aspx?index=0" meta:resourcekey="lnkHome" />
             
            <center>
                <asp:Image ID="Image1" Visible = "false" runat="server" Height="137px" Width="251px" />
            </center>
            <div>
                <center>
                    <asp:Label ID="lTitle" runat="server" meta:resourcekey="lTitle"></asp:Label>
                    <asp:TextBox ID="tbTitle" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server"
                    ControlToValidate="tbTitle" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                    <asp:Label ID="lCategory" runat="server" meta:resourcekey="lCategory"></asp:Label>
                    <asp:DropDownList ID="categoryU" runat="server" Width="100px"
                        OnSelectedIndexChanged="comboCategorySelectedIndexChanged">                       
                    </asp:DropDownList>

                </center>
            </div>
        </div>
        <div>
            <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/ProfilePage.aspx" meta:resourcekey="lnkMyProfile" />
            <center>
                <asp:Label ID="lISO" runat="server" meta:resourcekey="lISO"></asp:Label>
                <asp:TextBox ID="tbSensibility" runat="server"></asp:TextBox>
                <asp:Label ID="lDiaphragm" runat="server" meta:resourcekey="lDiaphragm"></asp:Label>
                <asp:TextBox ID="tbDiaphragm" runat="server"></asp:TextBox>
                <asp:Label ID="lExposition" runat="server" meta:resourcekey="lExposition"></asp:Label>
                <asp:TextBox ID="tbExposition" runat="server"></asp:TextBox>
                <asp:Label ID="lWhite" runat="server" meta:resourcekey="lWhite"></asp:Label>
                <asp:TextBox ID="tbDistance" runat="server"></asp:TextBox>
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" meta:resourcekey="lnkExit" />
            <center>
                <asp:Label ID="lDescription" runat="server" meta:resourcekey="lDescription"></asp:Label>
                <asp:TextBox ID="tbDescription" runat="server"></asp:TextBox>
                <asp:Label ID="lImageView" runat="server" meta:resourcekey="lImageView"></asp:Label>
                <asp:FileUpload ID="FileUpload1" runat="server"/>

                
                
            </center>
        </div>
        <div>            
            <center>
                <asp:Button ID="btnUpload" runat="server" meta:resourcekey="btnUpload" OnClick="btnUpload_Click" />
            </center>
        </div>
     </form>
    
</asp:Content>