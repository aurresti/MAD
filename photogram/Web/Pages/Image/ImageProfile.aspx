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
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/HomePage.aspx?index=0" meta:resourcekey="lnkHome" />
             <div>
                 <center>
                     <asp:Image ID="Image1" runat="server" Width="85px" />
                 </center>
             </div>
            <center>
                <asp:Label ID="lTitle" runat="server" meta:resourcekey="lTitle"></asp:Label>
                <asp:Label ID="lTitleContent" runat="server" Text="0"></asp:Label>
                <asp:Label ID="lCategory" runat="server" meta:resourcekey="lCategory"></asp:Label>
                <asp:Label ID="lCategoryContent" runat="server" Text="0"></asp:Label>
                <asp:Label ID="lUser" runat="server" meta:resourcekey="lUser"></asp:Label>
                <asp:HyperLink ID="hlUser" runat="server" NavigateUrl="~/Pages/HomePage.aspx" Text ="User"></asp:HyperLink>
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkMyProfile" runat="server" NavigateUrl="~/Pages/ProfilePage.aspx" meta:resourcekey="lnkMyProfile" />
            <center>
                <asp:Label ID="lISO" runat="server" meta:resourcekey="lISO"></asp:Label>
                <asp:Label ID="lISOContent" runat="server" Text="0"></asp:Label>
                <asp:Label ID="lDiaphragm" runat="server" meta:resourcekey="lDiaphragm"></asp:Label>
                <asp:Label ID="lDiaphragmContent" runat="server" Text="0"></asp:Label>
                <asp:Label ID="lExposition" runat="server" meta:resourcekey="lExposition"></asp:Label>
                <asp:Label ID="lExpositionContent" runat="server" Text="0"></asp:Label>
                <asp:Label ID="lWhite" runat="server" meta:resourcekey="lWhite"></asp:Label>
                <asp:Label ID="lWhiteContent" runat="server" Text="0"></asp:Label>
            </center>
        </div>
        <div>
            <asp:HyperLink ID="lnkExit" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" meta:resourcekey="lnkExit" />
            <center>
                <asp:Label ID="lDescription" runat="server" meta:resourcekey="lDescription"></asp:Label>
                <asp:Label ID="lDescriptionContent" runat="server" Text="0"></asp:Label>
            </center>
        </div>
        <div>                
            <center>
                <asp:Button ID="bLike" runat="server" meta:resourcekey="bLike" OnClick="bLike_Click" />
                <asp:Label ID="lLike" runat="server" Text="0"></asp:Label>
                <asp:Button ID="bComment" runat="server" meta:resourcekey="bComment" OnClick="bComment_Click" />
                <asp:Button ID="bSeeComment" runat="server" meta:resourcekey="bSeeComment" OnClick="bSeeComment_Click" />
            </center>
        </div>
     </form>
</asp:Content>
