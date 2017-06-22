<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Admin.ApplicationRelation.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table>
        <tr>
            <td>
                <table width="400">
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblApplicationRelationId" runat="server" Text="ApplicationRelationId:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationRelationId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationRelationId" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td width="300" class="ralabel" align="left">PublisherApplication :
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPublisherApplication" runat="server" 
                                OnSelectedIndexChanged="drpPublisherApplication_SelectedIndexChanged"
                                AppendDataBoundItems="true">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="txtPublisherApplicationId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPublisherApplicationId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">SubscriberApplication:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSubscriberApplication" runat="server" 
                                OnSelectedIndexChanged="drpSubscriberApplication_SelectedIndexChanged"
                                AppendDataBoundItems="true">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="txtSubscriberApplicationId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSubscriberApplicationId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">SystemEntityType:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSystemEntityType" runat="server" 
                                OnSelectedIndexChanged="drpSystemEntityType_SelectedIndexChanged"
                                AppendDataBoundItems="true">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemEntityTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">SubscriberApplicationRole:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSubscriberApplicationRole" runat="server" 
                                OnSelectedIndexChanged="drpSubscriberApplicationRole_SelectedIndexChanged"
                                AppendDataBoundItems="true">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="txtSubscriberApplicationRoleId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSubscriberApplicationRoleId" runat="server" />
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
