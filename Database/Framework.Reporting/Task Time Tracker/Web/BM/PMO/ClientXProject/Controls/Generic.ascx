<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ClientXProject.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table width="400">
        <tr>
            <td>
                <table width="95%">
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblClientXProjectId" Text="ClientXProjectId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClientXProjectId" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:PlaceHolder ID="dynClientXProjectId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">ClientId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpClientList" runat="server" OnSelectedIndexChanged="drpClientList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClientId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynClientId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">ProjectId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpProjectList" runat="server" OnSelectedIndexChanged="drpProjectList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProjectId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynProjectId" runat="server" />
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
