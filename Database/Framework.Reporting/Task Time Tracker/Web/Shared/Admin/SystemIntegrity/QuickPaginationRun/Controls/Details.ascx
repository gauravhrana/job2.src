<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.QuickPaginationRun.Controls.Details" %>
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
                <b>
                    <asp:Label ID="lblTextQuickPaginationRunId" runat="server" Text="QuickPaginationRunId"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lblQuickPaginationRunId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynQuickPaginationRunId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                SystemEntityType
            </td>
            <td>
                <asp:Label ID="lblSystemEntityTypeId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                ApplicationUser
            </td>
            <td>
                <asp:Label ID="lblApplicationUserId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                SortClause
            </td>
            <td>
                <asp:Label ID="lblSortClause" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                WhereClause
            </td>
            <td>
                <asp:Label ID="lblWhereClause" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                ExpirationTime
            </td>
            <td>
                <asp:Label ID="lblExpirationTime" runat="server"></asp:Label>
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
