<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.Schedule.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>
<div id="borderdiv" runat="server">
    <table class="table table-striped" >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblScheduleIdText" runat="server">ScheduleId: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblScheduleId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynScheduleId" runat="server" />
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblPersonText" runat="server">Person: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPersonId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblScheduleStateText" runat="server">Schedule State: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblScheduleState" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblWorkDateText" runat="server" >Work Date: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblWorkDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblStartTimeText" runat="server">Start Time: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStartTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblEndTimeText" runat="server" >End Time: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEndTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblTotalHoursWorkedText" runat="server">Total Hours Worked: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTotalHoursWorked" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblNextWorkDateText" runat="server">Next Work Date: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNextWorkDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblNextWorkTimeText" runat="server">Next Work Time: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNextWorkTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblPlannedHoursText" runat="server">Planned Hours: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPlannedHours" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblCreatedByAuditIdText" runat="server"><span >Created By AuditId:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedByAuditId" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblModifiedByAuditIdText" runat="server"><span >Modified By AuditId:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedByAuditId" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblCreatedDateText" runat="server"><span >Date Created:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblModifiedDateText" runat="server"><span >Date Modified:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedDate" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3">
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="qaGridView" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Question" HeaderText="Question" />
                        <asp:BoundField DataField="Answer" HeaderText="Answer" />
                    </Columns>
                </asp:GridView>
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
