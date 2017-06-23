<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlVisibilityManager.ascx.cs"
    Inherits="Shared.UI.Web.Controls.ControlVisibilityManager" %>
<asp:Menu ID="managerMenu" runat="server" CssClass="menu" EnableViewState="true" IncludeStyleBlock="false" Orientation="Horizontal"  OnMenuItemClick="managerMenu_MenuItemClick">
    <Items>
        <asp:MenuItem Text="*"></asp:MenuItem>
    </Items>
</asp:Menu>
