<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.WBS.TaskRiskRewardRankingXPerson.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblTaskRiskRewardRankingXPersonId" Text="TaskRiskRewardRankingXPersonId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskRiskRewardRankingXPersonId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTaskRiskRewardRankingXPersonId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            TaskId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTaskList" runat="server" OnSelectedIndexChanged="drpTaskList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTaskId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            RiskId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpRiskList" runat="server" OnSelectedIndexChanged="drpRiskList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRiskId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynRiskId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            RewardId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpRewardList" runat="server" OnSelectedIndexChanged="drpRewardList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRewardId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynRewardId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            PersonId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPersonList" runat="server" OnSelectedIndexChanged="drpPersonList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPersonId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPersonId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Ranking:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRanking" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynRanking" runat="server" />
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
