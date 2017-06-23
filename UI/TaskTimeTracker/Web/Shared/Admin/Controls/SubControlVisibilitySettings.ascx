<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubControlVisibilitySettings.ascx.cs"
    Inherits="Shared.UI.Web.Admin.Controls.SubControlVisibilitySettings" %>
<table   >
    <tr>
        <td width="300">
            Button Panel Visible
        </td>
        <td align="left">
            <asp:DropDownList ID="drpButtonPanelVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="300">
            Advanced Button panel Visible
        </td>
        <td align="left">
            <asp:DropDownList ID="drpAdvancedButtonPanelVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="300">
            Search Filter Visible
        </td>
        <td align="left">
            <asp:DropDownList ID="drpSearchFilterVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="300">
            Checked Box Column Visible
        </td>
        <td align="left">
            <asp:DropDownList ID="drpCheckedColumnVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="300">
            Font Panel Visible
        </td>
        <td align="left">
            <asp:DropDownList ID="drpFontPanelVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="300">
            Paging Panel Visible
        </td>
        <td align="left">
            <asp:DropDownList ID="drpPagingPanelVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="300">
            Sorting Panel Visible
        </td>
        <td align="left">
            <asp:DropDownList ID="drpSortingPanelVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="orientationContainer" runat="server" visible="false">
        <td width="300">
            Tab Orientation
        </td>
        <td align="left">
            <asp:DropDownList ID="drpTabOrientation" runat="server">
                <asp:ListItem Value="Horizontal">Horizontal</asp:ListItem>
                <asp:ListItem Value="Vertical">Vertical</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="300">
            Only Bind Active
        </td>
        <td align="left">
            <asp:DropDownList ID="drpOnlyBindActive" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td align="center">
            <asp:Button ID="btnSubmit" Text="Update" runat="server" OnClick="btnSubmit_OnClick"
                CausesValidation="true" />
            <asp:Button ID="btnUpdateReturn" Text="Update And Return" runat="server" OnClick="btnUpdateReturn_OnClick"
                CausesValidation="true" />
            <asp:Button ID="btnCancel" Text="Return" runat="server" OnClick="btnCancel_OnClick" />
        </td>
    </tr>
</table>
