<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
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
                <asp:Label ID="lblNotificationPublisherXEventTypeIdText" runat="server">NotificationPublisherXEventTypeId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNotificationPublisherXEventTypeId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynNotificationPublisherXEventTypeId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNotificationPublisherText" runat="server">NotificationPublisher:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNotificationPublisher" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNotificationEventTypeText" runat="server">NotificationEventType:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNotificationEventType" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedDateIdText" runat="server">CreatedDateId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedDateId" runat="server"></asp:Label>
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
