<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.TasksAndWorkflow.TaskRun.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
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
               <asp:Label ID="lblTaskRunIdText" runat="server" >TaskRunId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskRunId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynTaskRunId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
               <asp:Label ID="lblApplicationIdText" runat="server" >ApplicationId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTaskEntityText" runat="server">TaskEntity :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskEntity" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTaskScheduleIdText" runat="server">TaskScheduleId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskScheduleId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblBusinessDateText" runat="server">BusinessDate :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBusinessDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblStartTimeText" runat="server">StartTime :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStartTime" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
       <tr>
            <td class="ralabel">
                <asp:Label ID="lblEndTimeText" runat="server">EndTime :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEndTime" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblRunByText" runat="server">RunBy :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRunBy" runat="server"></asp:Label>
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
