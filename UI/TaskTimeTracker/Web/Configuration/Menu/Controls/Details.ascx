<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Configuration.Menu.Controls.Details" %>
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
                <b>
                    <asp:Label ID="lblTextMenuId" runat="server" Text="MenuId"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lblMenuId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynMenuId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
               <b>
                    <asp:Label ID="lblNameText" runat="server" Text="Name"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <b>
                    <asp:Label ID="lblParentMeuText" runat="server" Text="ParentMenu"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lblParentMenuId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
               <b>
                    <asp:Label ID="lblApplicationModuleText" runat="server" Text="ApplicationModule"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lblApplicationModule" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
               <b>
                    <asp:Label ID="lblPrimaryDeveloperText" runat="server" Text="PrimaryDeveloper"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lblPrimaryDeveloper" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDescriptionText" runat="server" Text="Description"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSortOrderText" runat="server" Text="SortOrder"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSortOrder" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblIsVisibleText" runat="server" Text="IsVisible"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIsVisible" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblIsCheckedText" runat="server" Text="IsChecked"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIsChecked" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNavigateURLText" runat="server" Text="NavigateUrl"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNavigateURL" runat="server"></asp:Label>
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
