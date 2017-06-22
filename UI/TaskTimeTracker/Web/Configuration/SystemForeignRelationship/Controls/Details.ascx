<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Configuration.SystemForeignRelationship.Controls.Details" %>
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
                <asp:Label ID="lblSystemForeignRelationshipIdText" runat="server"
                   >SystemForeignRelationshipId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemForeignRelationshipId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynSystemForeignRelationshipId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblPrimaryDatabaseText" runat="server">PrimaryDatabase :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPrimaryDatabase" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblPrimaryEntityIdText" runat="server">PrimaryEntityId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPrimaryEntityId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblForeignDatabaseText" runat="server">ForeignDatabase :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblForeignDatabase" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblForeignEntityIdText" runat="server">ForeignEntityId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblForeignEntityId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFieldNameText" runat="server">FieldName :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFieldName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSourceText" runat="server">Source :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSource" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSystemForeignRelationshipTypeText" runat="server">SystemForeignRelationshipType :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemForeignRelationshipType" runat="server"></asp:Label>
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
