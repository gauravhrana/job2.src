<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.VacationPlan.Controls.Details" %>
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
                <asp:Label ID="lblVacationPlanIdText" runat="server"><span>VacationPlanId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblVacationPlanId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynVacationPlanId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblNameText" runat="server"><span>Name: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDescriptionText" runat="server"><span>Description: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationUserIdText" runat="server"><span>ApplicationUserId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUserId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationUserId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblStartDateText" runat="server"><span>StartDate: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynStartDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEndDateText" runat="server"><span>EndDate: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynEndDate" runat="server" />
            </td>
        </tr>
                <tr>
            <td class="ralabel">
                <asp:Label ID="lblSortOrderText" runat="server"><span>SortOrder: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSortOrder" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedDateText" runat="server">CreatedDate:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblModifiedDateText" runat="server">ModifiedDate:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedByAuditIdText" runat="server">CreatedByAuditId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedByAuditId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynCreatedByAuditId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblModifiedByAuditIdText" runat="server">ModifiedByAuditId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedByAuditId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynModifiedByAuditId" runat="server" />
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
