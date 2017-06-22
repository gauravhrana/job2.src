<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<br />
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityEntityStatusIdText" runat="server">FunctionalityEntityStatusId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityEntityStatusId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynFunctionalityEntityStatusId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSystemEntityTypeText" runat="server">SystemEntityType :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityTypeId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityText" runat="server">Functionality :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityStatusText" runat="server">FunctionalityStatus :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityStatusId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityPriorityText" runat="server">FunctionalityPriority :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityPriorityId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblAssignedToText" runat="server">AssignedTo :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAssignedTo" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblMemoText" runat="server">Memo :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMemo" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTargetDateText" runat="server"><span>TargetDate: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTargetDate" runat="server"></asp:Label>
            </td>
        </tr> 
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblStartDateText" runat="server"><span>StartDate: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
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
