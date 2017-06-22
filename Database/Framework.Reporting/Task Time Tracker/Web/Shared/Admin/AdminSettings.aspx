<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true"
    CodeBehind="AdminSettings.aspx.cs" Inherits="Shared.UI.Web.Admin.AdminSettings" %>

<%@ Register TagPrefix="srx" Namespace="Shared.UI.WebFramework" Assembly="Shared.UI.WebFramework" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <style>
        label
        {
            margin-left: 5px;
            vertical-align: middle;
        }
    </style>
    <table border="0" style="font-weight:bold;">
         <tr>
            <td colspan="2" align="left" style="font-size:large"><b>Admin Settings</b></td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td>Update Info Style
            </td>
            <td align="left">
                <asp:DropDownList ID="drpUpdateInfoStyle" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Value="UpdateInfoStyle1">UpdateInfoStyle1</asp:ListItem>
                    <asp:ListItem Value="UpdateInfoStyle2">UpdateInfoStyle2</asp:ListItem>
                    <asp:ListItem Value="UpdateInfoStyle3">UpdateInfoStyle3</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td>Sort Arrow Style
            </td>
            <td align="left">
                <asp:DropDownList ID="drpSortArrowStyle" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Value="SortArrowStyle1">SortArrowStyle1</asp:ListItem>
                    <asp:ListItem Value="SortArrowStyle2">SortArrowStyle2</asp:ListItem>
                    <asp:ListItem Value="SortArrowStyle3">SortArrowStyle3</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td>Date Range Style
            </td>
            <td align="left">
                <asp:DropDownList ID="drpDateRangeStyle" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Value="Horizontal">Horizontal</asp:ListItem>
                    <asp:ListItem Value="Vertical">Vertical</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td>Date Control Layout 
            </td>
            <td align="left">
                <asp:DropDownList ID="drpDateControlLayout" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Value="H">Horizontal</asp:ListItem>
                    <asp:ListItem Value="V">Vertical</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td>Vertical Tab Header Background Color
            </td>
            <td align="left">
                <asp:TextBox ID="txtTabHeaderBackgroundColor" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td>Grid Description Column Default Character Count
            </td>
            <td align="left">
                <asp:TextBox ID="txtDescCount" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td></td>
            <td align="left">
                <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick"
                    CausesValidation="true" />
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_OnClick" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
