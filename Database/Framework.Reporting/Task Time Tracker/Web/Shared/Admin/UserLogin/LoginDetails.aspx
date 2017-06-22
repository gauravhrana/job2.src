<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="LoginDetails.aspx.cs"
    Inherits="Shared.UI.Web.Admin.UserLogin.LoginDetails" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchControlItem" runat="server">
    <asp:Table runat="server" CellSpacing="0" CellPadding="0" ID="tblMain" Width="50px"
        CssClass="searchfilter">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel">
            From Date:
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="txtSearchConditionFromDate" />
                Format:
                <asp:Label ID="lblFromDateFormat" runat="server" Text=""></asp:Label>
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel">To Date:</asp:TableCell><asp:TableCell>
                <asp:TextBox runat="server" ID="txtSearchConditionToDate" />
                Format:
                <asp:Label ID="lblToDateFormat" runat="server" Text=""></asp:Label>
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> </asp:TableCell><asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
            </asp:TableCell></asp:TableRow>
    </asp:Table>
    <asp:GridView ID="LoginDetailsGrid" runat="server" Width="50%">
    </asp:GridView>
</asp:Content>
