<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.TaskPackageXOwnerXTask.Controls.Details" %>
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
            <td width="100">
                <asp:Label ID="lblTaskPackageXOwnerXTaskIdText" runat="server"><span>TaskPackageXOwnerXTaskId:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskPackageXOwnerXTaskId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynTaskPackageXOwnerXTaskId" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTaskPackageText" runat="server"><span> TaskPackage:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskPackage" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblApplicationUserText" runat="server"><span> ApplicationUser:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUser" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTaskText" runat="server">Task :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskId" runat="server"></asp:Label>
            </td>
            <td>
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
