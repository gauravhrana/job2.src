<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.WBS.TaskPriorityXApplicationUser.Controls.Details" %>
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
                <asp:Label ID="lblTaskPriorityXApplicationUserIdText" runat="server"><span>TaskPriorityXApplicationUserId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskPriorityXApplicationUserId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynTaskPriorityXApplicationUserId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblTaskText" runat="server">Task :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTask" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblTaskPriorityTypeText" runat="server">TaskPriorityType :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTaskPriorityType" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationUserText" runat="server">ApplicationUser :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUser" runat="server"></asp:Label>
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