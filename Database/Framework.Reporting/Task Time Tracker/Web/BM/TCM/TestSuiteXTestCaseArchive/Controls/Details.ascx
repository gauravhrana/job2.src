<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.TCM.TestSuiteXTestCaseArchive.Controls.Details" %>

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
                <asp:Label ID="lblTestSuiteXTestCaseArchiveIdText" runat="server">TestSuiteXTestCaseArchiveId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestSuiteXTestCaseArchiveId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynTestSuiteXTestCaseArchiveId" runat="server" />
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
                <asp:Label ID="lblTestSuiteText" runat="server">TestSuite :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestSuite" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTestCaseText" runat="server">TestCase :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestCase" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTestCaseStatusText" runat="server">TestCaseStatus :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestCaseStatus" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTestCasePriorityText" runat="server">TestCasePriority :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestCasePriority" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTestSuiteXTestCaseIdText" runat="server">TestSuiteXTestCaseId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestSuiteXTestCaseId" runat="server"></asp:Label>
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

