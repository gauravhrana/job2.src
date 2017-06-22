<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblFieldConfigurationModeXApplicationRoleId" Text="FieldConfigurationModeXApplicationRoleId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFieldConfigurationModeXApplicationRoleId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFieldConfigurationModeXApplicationRoleId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            FieldConfigurationModeId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFieldConfigurationModeList" runat="server" OnSelectedIndexChanged="drpFieldConfigurationModeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFieldConfigurationModeId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFieldConfigurationModeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            ApplicationRoleId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplicationRoleList" runat="server" OnSelectedIndexChanged="drpApplicationRoleList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationRoleId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationRoleId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            FieldConfigurationModeAccessModeId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFieldConfigurationModeAccessModeList" runat="server" OnSelectedIndexChanged="drpFieldConfigurationModeAccessModeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFieldConfigurationModeAccessModeId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFieldConfigurationModeAccessModeId" runat="server" />
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
