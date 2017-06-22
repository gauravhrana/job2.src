<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode.Controls.SearchFilter" %>
<asp:Table runat="server" CellPadding="5" ID="tblMain" CssClass="searchfilter" Width="600">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">Search</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Name:</asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionName" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
