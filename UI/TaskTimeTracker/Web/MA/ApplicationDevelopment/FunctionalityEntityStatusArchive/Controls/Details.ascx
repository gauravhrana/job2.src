<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatusArchive.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  >
        <%--<tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityEntityStatusArchiveIdText" runat="server">FunctionalityEntityStatusArchiveId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityEntityStatusArchiveId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynFunctionalityEntityStatusArchiveId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblRecordDateText" runat="server">Record :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRecordDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSystemEntityTypeText" runat="server">SystemEntityType :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityType" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityText" runat="server">Functionality :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionality" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityStatusText" runat="server">FunctionalityStatus :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityStatus" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityPriorityText" runat="server">FunctionalityPriority :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityPriority" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityEntityStatusIdText" runat="server">FunctionalityPriority :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityEntityStatusId" runat="server"></asp:Label>
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
                <asp:Label ID="lblKnowledgeDateText" runat="server">KnowledgeDate :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblKnowledgeDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblAcknowledgedByIdText" runat="server">AcknowledgedById :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAcknowledgedById" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblAcknowledgedByText" runat="server">AcknowledgedById :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAcknowledgedBy" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTargetDateText" runat="server">TargetDate :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTargetDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblStartDateText" runat="server">StartDate :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
            </td>
            <td>
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
