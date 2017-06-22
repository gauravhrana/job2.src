<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" 
    Inherits="Shared.UI.Web.SystemIntegrity.RenumberMigration.Controls.Details" %>


<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblRenumberMigrationIdText" runat="server"><span>RenumberMigrationId:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRenumberMigrationId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynRenumberMigrationId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationIdText" runat="server"><span>ApplicationId:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationId" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSystemEntityTypeText" runat="server"><span>SystemEntityType:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityType" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblOriginalKeyText" runat="server"><span>Original Key:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblOriginalKey" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblMigratedKeyText" runat="server"><span>Migrated Key:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMigratedKey" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>        
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblRecordDateText" runat="server"><span>Record Date:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRecordDate" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>            
    </table>
</div>
