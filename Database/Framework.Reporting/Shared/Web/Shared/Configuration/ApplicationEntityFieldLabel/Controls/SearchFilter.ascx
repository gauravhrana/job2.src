<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Controls.SearchFilter" %>
<asp:Table runat="server" CellPadding="5" ID="tblMain" CssClass="searchfilter" Width="600">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">Search</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">SystemEntityType:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionSystemEntityType" runat="server" AutoPostBack="false"
                AppendDataBoundItems="true" OnSelectedIndexChanged="drpSearchConditionSystemEntityType_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
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
