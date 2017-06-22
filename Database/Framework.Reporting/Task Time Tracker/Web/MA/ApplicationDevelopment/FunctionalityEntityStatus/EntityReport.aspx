<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="EntityReport.aspx.cs"
    Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.EntityReport" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<%@ Register Src="~/Shared/Controls/ReadOnlyBucket.ascx" TagName="ReadOnlyBucket"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="sm" TagName="SubMenu" Src="~/Shared/Controls/SubMenu/SubMenu.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ContentPlaceHolderID="HeadContent" ID="HeadContent" runat="server">
    <link href="../../../Styles/Tabs.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table cellpadding="0" cellspacing="0" class="searchfilter" runat="server" id="tblMainEntityReport">
        <tr>
            <td class="searchFilterHeaderContainer" >
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynInputTabs" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" Text="Generate Report" ID="btnSelect" OnClick="btnSelect_Click" />
            </td>
        </tr>
    </table>
    <div style="width: 1000px; overflow: auto;">
        <asp:PlaceHolder ID="TableReportContent" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>
