<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs"
    Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleHistory.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>

<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblScheduleHistoryIdText" runat="server"
                   >ScheduleHistoryId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblScheduleHistoryId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynScheduleHistoryId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblRecordDateText" runat="server">Record :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRecordDate" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>        
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblWorkDateText" runat="server">WorkDate :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblWorkDate" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblStartTimeText" runat="server">StartTime :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStartTime" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEndTimeText" runat="server">EndTime :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEndTime" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTotalHoursWorkedText" runat="server">TotalHoursWorked :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTotalHoursWorked" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNextWorkDateText" runat="server">NextWorkDate :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNextWorkDate" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNextWorkTimeText" runat="server">NextWorkTime :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNextWorkTime" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblScheduleIdText" runat="server">ScheduleId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblScheduleId" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblPersonText" runat="server">Person :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPerson" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblScheduleStateNameText" runat="server">ScheduleStateName :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblScheduleStateName" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblKnowledgeDateText" runat="server">KnowledgeDate :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblKnowledgeDate" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblAcknowledgedByIdText" runat="server">AcknowledgedById :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAcknowledgedById" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblAcknowledgedByText" runat="server">AcknowledgedById :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAcknowledgedBy" runat="server"></asp:Label>
            </td>
            <td></td>
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
