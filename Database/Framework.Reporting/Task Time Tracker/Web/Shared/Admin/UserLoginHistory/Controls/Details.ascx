<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Admin.UserLoginHistory.Controls.Details" %>
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
                <asp:Label ID="lblUserLoginHistoryIdText" runat="server">UserLoginHistoryId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUserLoginHistoryId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynUserLoginHistoryId" runat="server" />
            </td>
        </tr>
        
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblUserIdText" runat="server">User Id :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUserId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblUserNameText" runat="server">User Name :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lbURLText" runat="server">URL :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblURL" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDateVisitedText" runat="server">Date Visited :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDateVisited" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblServerNameText" runat="server">Server :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblServerName" runat="server"></asp:Label>
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

