<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.SuperKeyDetail.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <b>
                                <asp:Label ID="lblSuperKeyDetailId" runat="server" Text="SuperKeyDetailId:"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSuperKeyDetailId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSuperKeyDetailId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            SuperKey:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSuperKey" runat="server" OnSelectedIndexChanged="drpSuperKey_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSuperKeyId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSuperKeyId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Entity Key:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityKey" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityKey" runat="server" />
                        </td>
                    </tr>
                    </table>
                <ui:UpdateInfo ID="UpdateInfo" runat="server" />
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
