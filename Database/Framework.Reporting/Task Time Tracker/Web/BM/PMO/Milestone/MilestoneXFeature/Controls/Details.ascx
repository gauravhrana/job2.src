<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.MilestoneXFeature.Controls.Details" %>
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
                <asp:Label ID="lblMilestoneXFeatureIdText" runat="server">MilestoneXFeatureId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMilestoneXFeatureId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynMilestoneXFeatureId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblMilestoneText" runat="server">Milestone:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMilestone" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblMilestoneFeatureStateText" runat="server">MilestoneFeatureState:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMilestoneFeatureState" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFeatureText" runat="server">Feature:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFeature" runat="server"></asp:Label>
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
