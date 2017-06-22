<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.TasksAndWorkflow.TaskSchedule.Controls.Details" %>
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
               <asp:Label ID="lblTaskScheduleIdText" runat="server" >TaskScheduleId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskScheduleId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynTaskScheduleId" runat="server" />
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
                 <asp:Label ID="lblTaskScheduleTypeText" runat="server" >TaskScheduleType :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskScheduleType" runat="server"></asp:Label>
            </td>
            <td>
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
