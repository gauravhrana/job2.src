<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Configuration.ThemeDetail.Controls.Details" %>
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
                <asp:Label ID="lblThemeDetailIdText" runat="server"><span>ThemeDetailId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblThemeDetailId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynThemeDetailId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationIdText" runat="server"><span>ApplicationId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblThemeKeyIdText" runat="server"><span>ThemeKeyId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblThemeKeyId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblValueText" runat="server"><span>Value: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValue" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblThemeIdText" runat="server"><span>ThemeId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblThemeId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblThemeCategoryIdText" runat="server"><span>ThemeCategoryId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblThemeCategoryId" runat="server"></asp:Label>
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
                                <asp:Label ID="lblHistory" runat="server" Text="" Visible="false"><b>Record History</b></asp:Label>
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