<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.MilestoneXFeature.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblMilestoneXFeatureId" Text="MilestoneXFeatureId:"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMilestoneXFeatureId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynMilestoneXFeatureId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            MilestoneId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpMilestoneList" runat="server" OnSelectedIndexChanged="drpMilestoneList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMilestoneId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynMilestoneId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            MilestoneFeatureStateId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpMilestoneFeatureStateList" runat="server" OnSelectedIndexChanged="drpMilestoneFeatureStateList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMilestoneFeatureStateId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynMilestoneFeatureStateId" runat="server" />
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
                            <asp:Label ID="lblMemo" Text="Memo:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMemo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynMemo" runat="server" />
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
