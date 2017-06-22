<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs"
    Inherits="Shared.UI.Web.Admin.Audit.AuditHistory.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>

<div id="borderdiv" runat="server">
    <table>
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblAuditHistoryIdText" runat="server"><span>AuditHistoryId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAuditHistoryId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynAuditHistoryId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSystemEntityIdText" runat="server"><span>SystemEntity: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynSystemEntity" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEntityKeyText" runat="server"><span>EntityKey: </span></asp:Label>

            </td>
            <td>
                <asp:Label ID="lblEntityKey" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynEntityKey" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblAuditActionIdText" runat="server"><span>AuditAction: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAuditActionId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynAuditActionId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedDateText" runat="server"><span>CreatedDate: </span></asp:Label>

            </td>
            <td>
                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynCreatedDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedByPersonIdText" runat="server"><span>CreatedByPerson: </span></asp:Label>                
            </td>
            <td>
                <asp:Label ID="lblCreatedByPersonId" runat="server"></asp:Label>
            </td>
             <td>
                <asp:PlaceHolder ID="dynCreatedByPerson" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
            </td>
        </tr>
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
