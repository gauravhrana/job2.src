<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.SystemIntegrity.SuperKey.Controls.SearchFilter" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
<asp:Table runat="server" ID="tblMain" CssClass="searchfilter" >
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">SystemEntityType:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionSystemEntityType" runat="server" AppendDataBoundItems="true"
                OnSelectedIndexChanged="drpSearchConditionSystemEntityType_SelectedIndexChanged"
                AutoPostBack="false">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionSystemEntityType" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Name:</asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionName" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
            <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
