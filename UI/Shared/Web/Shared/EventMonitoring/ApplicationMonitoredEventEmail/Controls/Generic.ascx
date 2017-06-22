<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.ApplicationMonitoredEventEmail.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblApplicationMonitoredEventEmailId" Text="ApplicationMonitoredEventEmailId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationMonitoredEventEmailId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationMonitoredEventEmailId" runat="server" />
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
                            <asp:TextBox ID="txtApplicationMonitoredEventSourceId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationMonitoredEventSourceId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            UserId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpUserList" runat="server" OnSelectedIndexChanged="drpUserList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynUserId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            CorrespondenceLevel:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCorrespondenceLevel" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCorrespondenceLevel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Active:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpActive" runat="server">
                                <asp:ListItem Value="true">Yes</asp:ListItem>
                                <asp:ListItem Value="false">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynActive" runat="server" />
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
