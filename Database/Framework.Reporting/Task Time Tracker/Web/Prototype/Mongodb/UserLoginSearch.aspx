<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="UserLoginSearch.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Mongodb.UserLoginSearch" %>

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
            <asp:TableCell CssClass="ralabel"> User Name: </asp:TableCell>
            <asp:TableCell>

                <asp:TextBox runat="server" ID="txtUserName" />

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
                <asp:GridView ID="gridUserLogin" AutoGenerateColumns="true" runat="server"></asp:GridView>               
            </td>
        </tr>
    </table>

</asp:Content>



