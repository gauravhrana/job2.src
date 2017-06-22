<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonNav.ascx.cs" Inherits="ApplicationContainer.UI.Web.MasterPages.CommonNav" %>

<div class="row" style="background-color: #4b6c9e;">

    <div class="col-sm-12">

        <asp:Menu ID="NavigationMenu" runat="server"
            CssClass="app-main-menu"
            EnableViewState="false"
            IncludeStyleBlock="false" Orientation="Horizontal"
            RenderingMode="List">

            <StaticMenuItemStyle CssClass="stdMenuItem" />
            <DynamicMenuItemStyle CssClass="stdMenuItem" />

            <StaticSelectedStyle CssClass="MenuDefaultSelectedStyle" />
            <DynamicSelectedStyle CssClass="MenuDefaultSelectedStyle" />

            <StaticHoverStyle CssClass="MenuDefaultHoverStyle" />
            <DynamicHoverStyle CssClass="MenuDefaultHoverStyle" />

        </asp:Menu>

    </div>

</div>
