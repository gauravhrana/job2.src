<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.ApplicationManagement.Development.TestData.Controls.SearchFilter" %>
<asp:Table runat="server" CellPadding="5" ID="tblMain" CssClass="searchfilter" Width="600">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">Search</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Application:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionAplication" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:TextBox runat="server" ID="txtSearchConditionTaskEntity"/> --%>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
