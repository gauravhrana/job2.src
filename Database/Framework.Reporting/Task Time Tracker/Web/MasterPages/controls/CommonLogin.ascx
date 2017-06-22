<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonLogin.ascx.cs" Inherits="ApplicationContainer.UI.Web.MasterPages.CommonLogin" %>

<div class="loginDisplay" style="display: none">
    <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
        <AnonymousTemplate>
            [ <a href="~/Account/Login.aspx" id="HeadLoginStatus" runat="server">Log In</a>]
        </AnonymousTemplate>
        <LoggedInTemplate>
            Welcome <span class="bold">
                <asp:LoginName ID="HeadLoginName" runat="server" />
            </span>!    [
                        <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out" LogoutPageUrl="~/" />
            ]
        </LoggedInTemplate>
    </asp:LoginView>
</div>
