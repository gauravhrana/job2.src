<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.FeatureXFeatureRule.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblFeatureXFeatureRuleId" Text="FeatureXFeatureRuleId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFeatureXFeatureRuleId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFeatureXFeatureRuleId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            FeatureId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFeatureList" runat="server" OnSelectedIndexChanged="drpFeatureList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFeatureId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFeatureId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            FeatureRuleStatusId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFeatureRuleStatusList" runat="server" OnSelectedIndexChanged="drpFeatureRuleStatusList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFeatureRuleStatusId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFeatureRuleStatusId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            FeatureRuleId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFeatureRuleList" runat="server" OnSelectedIndexChanged="drpFeatureRuleList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFeatureRuleId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFeatureRuleId" runat="server" />
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
