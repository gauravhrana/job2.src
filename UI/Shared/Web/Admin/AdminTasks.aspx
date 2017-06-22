<%@ Page Title="Admin Tasks" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdminTasks.aspx.cs" Inherits="Shared.UI.Web.Admin.AdminTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    Admin Tasks
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table cellpadding="5" style="font-weight: bold; color: Black" width="600" border="0">
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
            </td>
        </tr>
        <tr valign="top">
            <td width="200">
                Date:
            </td>
            <td align="left">
                <asp:DropDownList ID="drpCalendar" runat="server">
                    <asp:ListItem Value="0">Today</asp:ListItem>
                    <asp:ListItem Value="7">Last Week</asp:ListItem>
                    <asp:ListItem Value="31">Last Month</asp:ListItem>
                    <asp:ListItem Value="120">Last Quarter</asp:ListItem>
                    <asp:ListItem Value="365">Last Year</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="200">
                Expired SuperKey Records
            </td>
            <td align="left">
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                <asp:Button ID="btnDeleteSuperKey" runat="server" Text="Delete" OnClick="btnDeleteSuperKey_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:GridView ID="dgvSuperKey" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="SuperKeyId" HeaderText="Id" ControlStyle-Width="100" />
                        <asp:BoundField DataField="Name" HeaderText="Name" ControlStyle-Width="200" />
                        <asp:BoundField DataField="SystemEntityType" HeaderText="SystemEntityType" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="SortOrder" HeaderText="SortOrder" />
                        <asp:BoundField DataField="ExpirationDate" HeaderText="ExpirationDate" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
