<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.ApplicationManagement.Development.TestData.BrokenHistoryControls.SearchFilter" %>
<asp:Table runat="server" ID="tblMain" CssClass="searchfilter" >
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">Search</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Type Of Issue:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionTypeOfIssue" runat="server" AppendDataBoundItems="true" 
             OnSelectedIndexChanged="drpSearchConditionTypeOfIssue_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
                <%--<asp:ListItem Value="InvalidEntityType">Invalid System Entity Type</asp:ListItem>
                <asp:ListItem Value="InvalidApplicationUser">Invalid Application User</asp:ListItem>
                <asp:ListItem Value="InvalidEntityKey">Invalid Entity Key</asp:ListItem>--%>
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="rowEntityType" Visible="false">
        <asp:TableCell CssClass="ralabel">SystemEntityType:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionSystemEntityType" runat="server" AppendDataBoundItems="true" 
             OnSelectedIndexChanged="drpSearchConditionSystemEntityType_SelectedIndexChanged" AutoPostBack="false">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:TextBox runat="server" ID="txtSearchConditionSystemEntityType" />--%>
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
