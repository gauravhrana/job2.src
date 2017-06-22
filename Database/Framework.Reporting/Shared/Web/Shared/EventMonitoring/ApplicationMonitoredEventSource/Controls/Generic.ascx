<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.ApplicationMonitoredEventSource.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblApplicationMonitoredEventSourceId" Text="ApplicationMonitoredEventSourceId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationMonitoredEventSourceId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationMonitoredEventSourceId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Code:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Description:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
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
