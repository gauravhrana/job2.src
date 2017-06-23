<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="TestDateRangeNewCtrl.aspx.cs" Inherits="ApplicationContainer.UI.Web.TestDateRangeNewCtrl" %>

<%@ Register Src="~/Shared/Controls/DateRangeAdvancedSearch.ascx" TagPrefix="uc1" TagName="DateRange" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="MainContent">
    <uc1:DateRange id="oDateRange" runat="server" />
    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
</asp:Content>
