<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="QuickSearchPage.aspx.cs" Inherits="ApplicationContainer.UI.Web.QuickSearchPage" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>   
<%@ Register TagName="QuickSearchControl" TagPrefix="qs" Src="~/Shared/Controls/QuickSearch/QuickSearch.ascx" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SearchControlItem" runat="server">   

       <asp:Table runat="server" CellSpacing="0" CellPadding="0" ID="tblMain" CssClass="searchfilter">
       <asp:TableRow>
        <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
            <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
        </asp:TableCell>
    </asp:TableRow>
        <asp:TableRow>
         <asp:TableCell>
         <qs:QuickSearchControl id="QuickSearchControlId" runat="server"  /></asp:TableCell></asp:TableRow></asp:Table></asp:Content>
        