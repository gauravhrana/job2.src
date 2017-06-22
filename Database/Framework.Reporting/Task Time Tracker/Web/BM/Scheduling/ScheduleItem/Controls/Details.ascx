<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleItem.Controls.Details" %>
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
                <asp:Label ID="lblScheduleItemText" runat="server"><span>ScheduleItemId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblScheduleItemId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynScheduleItemId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblScheduleIdText" runat="server"><span>ScheduleId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblScheduleId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTaskFormulationIdText" runat="server"><span>TaskFormulationId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskFormulationId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTotalTimeSpentText" runat="server"><span>TotalTimeSpent: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTotalTimeSpent" runat="server"></asp:Label>
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