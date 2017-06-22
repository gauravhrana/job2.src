<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
 <%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFieldConfigurationModeXApplicationRoleIdText" runat="server">FieldConfigurationModeXApplicationRoleId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFieldConfigurationModeXApplicationRoleId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynFieldConfigurationModeXApplicationRoleId" runat="server" />
            </td>
        </tr>       
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFieldConfigurationModeText" runat="server">FieldConfigurationMode:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFieldConfigurationMode" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationRoleText" runat="server">ApplicationRole:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationRole" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>      
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFieldConfigurationModeAccessModeText" runat="server">FieldConfigurationModeAccessMode:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFieldConfigurationModeAccessMode" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
    <table>
        <tr>
            <td colspan="2">
                <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dc:List ID="oHistoryList" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</div>
