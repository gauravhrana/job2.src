<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolioGroupXProjectPortfolio.Controls.Details" %>
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
                <asp:Label ID="lblProjectPortfolioGroupXProjectPortfolioIdText" runat="server">ProjectPortfolioGroupXProjectPortfolioId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProjectPortfolioGroupXProjectPortfolioId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynProjectPortfolioGroupXProjectPortfolioId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblProjectPortfolioGroupIdText" runat="server">ProjectPortfolioGroupId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProjectPortfolioGroupId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynProjectPortfolioGroupId" runat="server" />
            </td>
        </tr>

        <tr>
            <td class="ralabel">
                <asp:Label ID="lblProjectPortfolioIdText" runat="server">ProjectPortfolioId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProjectPortfolioId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynProjectPortfolioId" runat="server" />
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblDescriptionText" runat="server">Description:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
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
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedDateText" runat="server">CreatedDate:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblModifiedDateText" runat="server">ModifiedDate:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedByAuditIdText" runat="server">CreatedByAuditId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedByAuditId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynCreatedByAuditId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblModifiedByAuditIdText" runat="server">ModifiedByAuditId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedByAuditId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynModifiedByAuditId" runat="server" />
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
