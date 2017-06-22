<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.ApplicationMonitoredEvent.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblApplicationMonitoredEventId" Text="ApplicationMonitoredEventId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationMonitoredEventId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationMonitoredEventId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                         <td class="ralabel">
                            ApplicationMonitoredEventSourceId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplicationMonitoredEventSourceList" runat="server"
                                OnSelectedIndexChanged="drpApplicationMonitoredEventSourceList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationMonitoredEventSourceId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationMonitoredEventSourceId" runat="server" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            ApplicationMonitoredEventProcessingStateId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplicationMonitoredEventProcessingStateList"
                                runat="server" OnSelectedIndexChanged="drpApplicationMonitoredEventProcessingStateList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationMonitoredEventProcessingStateId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationMonitoredEventProcessingStateId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                         <td class="ralabel">
                            ReferenceId:
                        </td>
                        <td>
                            <asp:TextBox ID="txtReferenceId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReferenceId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                         <td class="ralabel">
                            ReferenceCode:
                        </td>
                        <td>
                            <asp:TextBox ID="txtReferenceCode" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReferenceCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                         <td class="ralabel">
                            Category:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCategory" runat="server" />
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
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynMessage" runat="server" />
                        </td>
                    </tr>
                    <tr>
                         <td class="ralabel">
                            IsDuplicate:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpIsDuplicate" runat="server">
                                <asp:ListItem Value="true">Yes</asp:ListItem>
                                <asp:ListItem Value="false">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynIsDuplicate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                         <td class="ralabel">
                            LastModifiedBy:
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastModifiedBy" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynLastModifiedBy" runat="server" />
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
