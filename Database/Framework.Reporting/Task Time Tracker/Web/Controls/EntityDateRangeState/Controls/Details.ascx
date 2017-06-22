<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.EntityDateRangeState.Controls.Details" %>

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
                <asp:Label ID="lblEntityDateRangeStateIdText" runat="server">EntityDateRangeStateId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEntityDateRangeStateId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynEntityDateRangeStateId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblStartDateText" runat="server">StartDate:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEndDateText" runat="server">EndDate:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSystemEntityIdText" runat="server">SystemEntityId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblKeyIdText" runat="server">KeyId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblKeyId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEntityDateRangeStateTypeText" runat="server">EntityDateRangeStateType:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEntityDateRangeStateType" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNotesText" runat="server">Notes:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNotes" runat="server"></asp:Label>
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

