<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblTestSuiteXTestCaseId" Text="TestSuiteXTestCaseId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTestSuiteXTestCaseId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTestSuiteXTestCaseId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            TestSuiteId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTestSuiteList" runat="server" OnSelectedIndexChanged="drpTestSuiteList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTestSuiteId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTestSuiteId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            TestCaseId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTestCaseList" runat="server" OnSelectedIndexChanged="drpTestCaseList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTestCaseId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTestCaseId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            TestCaseStatus:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTestCaseStatusList" runat="server" OnSelectedIndexChanged="drpTestCaseStatusList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTestCaseStatusId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTestCaseStatusId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            TestCasePriority:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTestCasePriorityList" runat="server" OnSelectedIndexChanged="drpTestCasePriorityList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTestCasePriorityId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTestCasePriorityId" runat="server" />
                        </td>
                    </tr>
            </table>
            <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
            <table>
                    <tr>
                        <td colspan="4">
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
            </td>
        </tr>
    </table>
</div>

