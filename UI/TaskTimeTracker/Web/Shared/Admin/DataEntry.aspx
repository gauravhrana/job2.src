<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataEntry.aspx.cs" MasterPageFile="~/MasterPages/Site.Master" Inherits="Shared.UI.Web.Admin.DataEntry" %>


<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   <table style="font-weight: bold; color: Black" width="600" border="0">
        <tr>
            <td>Component
            </td>
            <td align="left">
                <asp:DropDownList ID="drpDBComponent" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Value="DBName">DBName</asp:ListItem>
                    <asp:ListItem Value="DBComponentName">DBComponentName</asp:ListItem>
                    <asp:ListItem Value="DBProjectName">DBProjectName</asp:ListItem>
                    <asp:ListItem Value="EntityDetails">EntityDetails</asp:ListItem>
                </asp:DropDownList>
            </td>=
        </tr>
        <tr>
            <td></td>
            </tr>
       <tr>
            <td></td>
            </tr>
        <tr>
            <td></td>
            <td align="left">
                <asp:Button ID="btnSubmit" Text="Create Entries" runat="server" OnClick="btnSubmit_OnClick"
                    CausesValidation="true" />
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_OnClick" />
            </td>
        </tr>
    </table>
</asp:Content>

