<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase.Controls.Details" %>
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
            <td width="100">
                <asp:Label ID="lblTestSuiteXTestCaseIdText" runat="server"><span>TestSuiteXTestCaseId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestSuiteXTestCaseId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynTestSuiteXTestCaseId" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTestSuiteText" runat="server"><span>TestSuite: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestSuiteId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTestCaseText" runat="server"><span>TestCase: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestCaseId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        

        <tr>
            <td>
                <asp:Label ID="lblTestCaseStatusText" runat="server">TestCaseStatus :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestCaseStatusId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTestCasePriorityText" runat="server">TestCasePriority :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTestCasePriorityId" runat="server"></asp:Label>
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