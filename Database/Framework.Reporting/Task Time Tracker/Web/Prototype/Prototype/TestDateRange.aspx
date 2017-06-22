<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="TestDateRange.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestDateRange" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<%@ Register TagName="DateRangeControl" TagPrefix="dr" Src="~/Shared/ApplicationManagement/Development/DateRangeSample.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchControlItem" runat="server">

    <asp:Table runat="server" CellSpacing="0" CellPadding="0" ID="tblMain" CssClass="searchfilter">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
       <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
            <dr:DateRangeControl ID="oDate" runat="server" />
               
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell> </asp:TableCell><asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
            </asp:TableCell></asp:TableRow></asp:Table></asp:Content><asp:Content ID="Content4" ContentPlaceHolderID="ListControlItem" runat="server">
    <table >
     
        <tr>
            <td colspan="3">
                <asp:GridView ID="LoginHistoryGrid" runat="server" >
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
