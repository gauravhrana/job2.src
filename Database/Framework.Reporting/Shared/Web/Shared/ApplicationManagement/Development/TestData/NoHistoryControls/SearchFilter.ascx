<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.ApplicationManagement.Development.TestData.NoHistoryControls.SearchFilter" %>
<asp:Table runat="server" CellPadding="5" ID="tblMain" CssClass="searchfilter" Width="600">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">Search</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Domain:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionDomain" runat="server" AppendDataBoundItems="true"
            AutoPostBack="true" OnSelectedIndexChanged="drpSearchConditionDomain_SelectedIndexChanged">
                <%--<asp:ListItem Selected="True" Value="-1">All</asp:ListItem>--%>
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">System Entity:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionSystemEntityType" runat="server" AppendDataBoundItems="true"
            AutoPostBack="false" OnSelectedIndexChanged="drpSearchConditionSystemEntityType_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell><asp:CheckBox ID="chkSearchOff" runat="server" Text="Auto Search Off" Checked="true" AutoPostBack="true"
        OnCheckedChanged="chkSearchOff_CheckedChanged">
        </asp:CheckBox></asp:TableCell>
        <asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
