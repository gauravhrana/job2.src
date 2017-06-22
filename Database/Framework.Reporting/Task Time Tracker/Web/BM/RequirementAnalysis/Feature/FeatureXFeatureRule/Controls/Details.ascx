<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.FeatureXFeatureRule.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %> 
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
                <asp:Label ID="lblFeatureXFeatureRuleIdText" runat="server" >FeatureXFeatureRuleId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFeatureXFeatureRuleId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynFeatureXFeatureRuleId" runat="server" />
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
                <asp:Label ID="lblFeatureRuleStatusText" runat="server">FeatureRuleStatus:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFeatureRuleStatus" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel"> 
                <asp:Label ID="lblFeatureRuleText" runat="server">FeatureRule:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFeatureRule" runat="server"></asp:Label>
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
