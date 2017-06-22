<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Admin.SystemEntityType.Controls.Details" %>
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
                <asp:Label ID="lblSystemEntityTypeIdText" runat="server"><span>SystemEntityTypeId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityTypeId" runat="server"></asp:Label>
            </td>
             <td>
                <asp:PlaceHolder ID="dynSystemEntityTypeId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEntityNameText" runat="server"><span>EntityName: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEntityName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEntityDescriptionText" runat="server"><span>EntityDescription: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEntityDescription" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNextValueText" runat="server"><span>NextValue: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNextValue" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="ralabel">
                <asp:Label ID="lblPrimaryDatabaseText" runat="server"><span>Primary Database: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPrimaryDatabase" runat="server"></asp:Label>
            </td>
        </tr>

         <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedDateText" runat="server"><span>Created Date: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
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
