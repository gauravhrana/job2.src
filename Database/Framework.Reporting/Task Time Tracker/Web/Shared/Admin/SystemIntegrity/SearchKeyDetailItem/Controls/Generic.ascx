<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.SearchKeyDetailItem.Controls.Generic" %>
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
                                <asp:Label ID="lblSearchKeyDetailItemId" runat="server" Text="SearchKeyDetailItemId:"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchKeyDetailItemId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSearchKeyDetailItemId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <b>
                                <asp:Label ID="lblSearchKeyDetailId" runat="server" Text="SearchKeyDetail:"></asp:Label></b>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSearchKeyDetailId" runat="server" OnSelectedIndexChanged="drpSearchKeyDetail_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchKeyDetailId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSearchKeyDetailId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Value:
                        </td>
                        <td>
                            <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynValue" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            SortOrder:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
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
