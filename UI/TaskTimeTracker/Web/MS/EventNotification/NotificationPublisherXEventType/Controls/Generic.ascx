<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtCreatedDateId.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });
    });
</script>
<div id="borderdiv" runat="server">
    <table width="400">
        <tr>
            <td>
                <table width="95%">
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblNotificationPublisherXEventTypeId" Text="NotificationPublisherXEventTypeId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotificationPublisherXEventTypeId" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:PlaceHolder ID="dynNotificationPublisherXEventTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">NotificationPublisherId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpNotificationPublisherList" runat="server" OnSelectedIndexChanged="drpNotificationPublisherList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotificationPublisherId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynNotificationPublisherId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">NotificationEventTypeId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpNotificationEventTypeList" runat="server" OnSelectedIndexChanged="drpNotificationEventTypeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotificationEventTypeId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynNotificationEventTypeId" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td class="ralabel">CreatedDateId:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreatedDateId" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            <asp:Label ID="lblUserDateTimeFormat" runat="server" Text=""></asp:Label>
                            <asp:PlaceHolder ID="dynCreatedDateId" runat="server" />
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
