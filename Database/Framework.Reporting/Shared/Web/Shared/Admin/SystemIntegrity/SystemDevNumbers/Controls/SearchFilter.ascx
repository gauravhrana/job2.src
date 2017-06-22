<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.SystemDevNumbers.Controls.SearchFilter" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
<asp:Table runat="server" ID="tblMain" CssClass="searchfilter" >
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Person:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionPerson" runat="server" AppendDataBoundItems="true"
                AutoPostBack="false" OnSelectedIndexChanged="drpSearchConditionPerson_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionPerson" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
            <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
