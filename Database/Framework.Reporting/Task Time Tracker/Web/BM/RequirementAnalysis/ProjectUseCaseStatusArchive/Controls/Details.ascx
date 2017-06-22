<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.ProjectUseCaseStatusArchive.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblProjectUseCaseStatusArchiveIdText" runat="server"
                   >ProjectUseCaseStatusArchiveId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProjectUseCaseStatusArchiveId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynProjectUseCaseStatusArchiveId" runat="server" />
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
                <asp:Label ID="lblUseCaseText" runat="server">UseCase :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUseCase" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblProjectText" runat="server">Project :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProject" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblProjectUseCaseStatusText" runat="server">ProjectUseCaseStatus :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProjectUseCaseStatus" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblProjectUseCaseStatusIdText" runat="server">ProjectUseCaseStatusId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProjectUseCaseStatusId" runat="server"></asp:Label>
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
