<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Admin.ConnectionString.Controls.Details" %>
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
                <asp:Label ID="lblConnectionStringIdText" runat="server"><span>ConnectionStringId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblConnectionStringId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynConnectionStringId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNameText" runat="server"><span>Name: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDescriptionText" runat="server"><span>Description: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDataSourceText" runat="server"><span>Data Source: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDataSource" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="ralabel">
                <asp:Label ID="lblInitialCatalogText" runat="server"><span>Initial Catalog: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblInitialCatalog" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblUserNameText" runat="server"><span>User Name: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="ralabel">
                <asp:Label ID="lblPasswordText" runat="server"><span>Password: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPassword" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="ralabel">
                <asp:Label ID="lblProviderNameText" runat="server"><span>Provider Name: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProviderName" runat="server"></asp:Label>
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
