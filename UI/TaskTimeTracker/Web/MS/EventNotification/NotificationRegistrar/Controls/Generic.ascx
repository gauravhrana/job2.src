<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.EventNotification.NotificationRegistrar.Controls.Generic" %>


<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblNotificationRegistrarId" Text="NotificationRegistrarId:"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotificationRegistrarId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynNotificationRegistrarId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            NotificationEventType:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpNotificationEventTypeList" runat="server" OnSelectedIndexChanged="drpNotificationEventTypeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotificationEventType" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynNotificationEventTypeId" runat="server" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="ralabel">
                            NotificationPublisher:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpNotificationPublisherList" runat="server" OnSelectedIndexChanged="drpNotificationPublisherList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotificationPublisher" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynNotificationPublisherId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Message:
                        </td>
                        <td>
                            <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynMessage" runat="server" />
                        </td>
                    </tr>
                     
                   
                </table>
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
                <table>
                    <tr>
                        <td colspan="4">
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
            </td>
        </tr>
    </table>
</div>


