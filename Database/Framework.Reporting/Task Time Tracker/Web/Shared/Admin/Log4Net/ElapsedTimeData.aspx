<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true"
    CodeBehind="ElapsedTimeData.aspx.cs" Inherits="Shared.UI.Web.Admin.Log4Net.ElapsedTimeData" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="ContentControlItem" ContentPlaceHolderID="SearchControlItem" runat="server">
    <asp:Table runat="server" CellSpacing="0" CellPadding="0" ID="tblMain" CssClass="searchfilter">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Computer Name: </asp:TableCell>
            <asp:TableCell>

                <asp:TextBox runat="server" ID="txtComputerName" />

            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Connection Key: </asp:TableCell><asp:TableCell>

                <asp:TextBox runat="server" ID="txtConnectionKey" />

            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Application User: </asp:TableCell><asp:TableCell>

                <asp:TextBox ID="txtApplicationUserList" runat="server" CssClass="form-control"></asp:TextBox>

            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Group By: </asp:TableCell><asp:TableCell>
                <asp:DropDownList runat="server" ID="drpGroupBy"
                    AppendDataBoundItems="true">
                    <asp:ListItem Value="None" Selected="True">None</asp:ListItem>
                    <asp:ListItem Value="ApplicationUser">ApplicationUser</asp:ListItem>
                    <asp:ListItem Value="Computer">Computer</asp:ListItem>
                    <asp:ListItem Value="ConnectionKey">ConnectionKey</asp:ListItem>

                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ListControlItem" runat="server">
    <table>
        <tr>
            <td colspan="3">
                <asp:Panel ID="pnlGroupListContainer" runat="server" />
                <asp:Panel ID="pnlGroupListContainer1" runat="server" />
            </td>
        </tr>
    </table>

</asp:Content>



