<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.SearchKeyDetail.Controls.Details" %>
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
                <b>
                    <asp:Label ID="lblSearchKeyDetailIdText" runat="server" Text="SearchKeyDetail Id"></asp:Label></b>
            </td>
            <td align="left">
                <asp:Label ID="lblSearchKeyDetailId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynSearchKeyDetailId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">                
                <asp:Label ID="lblSearchKeyText" runat="server">SearchKey:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSearchKeyName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                
                <asp:Label ID="lblSearchParameterText" runat="server">SearchParameter:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSearchParameter" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
            <asp:Label ID="lblSortOrderText" runat="server">SortOrder:</asp:Label>
                
            </td>
            <td>
                <asp:Label ID="lblSortOrder" runat="server"></asp:Label>
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
