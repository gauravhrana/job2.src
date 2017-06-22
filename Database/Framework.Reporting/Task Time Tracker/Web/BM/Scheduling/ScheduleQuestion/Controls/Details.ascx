<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion.Controls.Details" %>
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
                <asp:Label ID="lblScheduleQuestionIdText" runat="server"><span>ScheduleQuestionId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblScheduleQuestionId" runat="server"></asp:Label>
            </td>
             <td>
                <asp:PlaceHolder ID="dynScheduleQuestionId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
               <asp:Label ID="lblScheduleIdText" runat="server"><span>ScheduleId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblScheduleId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblQuestionText" runat="server"><span>Question: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblQuestionId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblAnswerText" runat="server"><span>Answer: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAnswer" runat="server"></asp:Label>
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