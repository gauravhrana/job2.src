<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ActivityXDeliverableArtifact.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblActivityXDeliverableArtifactId" Text="ActivityXDeliverableArtifactId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtActivityXDeliverableArtifactId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynActivityXDeliverableArtifactId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            ActivityId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpActivityList" runat="server" OnSelectedIndexChanged="drpActivityList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtActivityId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynActivityId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            DeliverableArtifactsStatusId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDeliverableArtifactsStatusList" runat="server"
                                OnSelectedIndexChanged="drpDeliverableArtifactsStatusList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeliverableArtifactsStatusId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDeliverableArtifactsStatusId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            DeliverableArtifactsId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDeliverableArtifactsList" runat="server" OnSelectedIndexChanged="drpDeliverableArtifactsList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeliverableArtifactsId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDeliverableArtifactsId" runat="server" />
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
