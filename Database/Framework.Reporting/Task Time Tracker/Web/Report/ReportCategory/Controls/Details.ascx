<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.ReportCategory.Controls.Details" %>
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
                <asp:Label ID="lblReportCategoryIdText" runat="server">ReportCategoryId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReportCategoryId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynReportCategoryId" runat="server" />
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationText" runat="server">Application :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplication" runat="server"></asp:Label>
            </td>
        </tr>   
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblNameText" runat="server">Name :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDescriptionText" runat="server">Description :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSortOrderText" runat="server">Sort Order: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSortOrder" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedByAuditIdText" runat="server"><span >Created By AuditId:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedByAuditId" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblModifiedByAuditIdText" runat="server"><span >Modified By AuditId:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedByAuditId" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedDateText" runat="server"><span >Date Created:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblModifiedDateText" runat="server"><span >Date Modified:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModifiedDate" runat="server"></asp:Label>
            </td>
            <td></td>
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
